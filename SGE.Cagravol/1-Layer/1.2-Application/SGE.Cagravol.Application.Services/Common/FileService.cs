using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace SGE.Cagravol.Application.Services.Common
{
    public sealed class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly string _baseTempPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService" /> class.
        /// </summary>
        public FileService()
        {
            _basePath = "~/" + ConfigurationManager.AppSettings["FileService.UploadFolder"]; ;
            _baseTempPath = "~/" + ConfigurationManager.AppSettings["FileService.TempUploadFolder"]; ;

            if (!_basePath.EndsWith("/")) _basePath += "/";
            if (!_baseTempPath.EndsWith("/")) _baseTempPath += "/";
        }

        /// <summary>
        /// Ensures the directory exists.
        /// </summary>
        /// <param name="logicalPath">The logical path.</param>
        public void EnsureDirectoryExists(UploadFoldersEnum logicalPathEnum)
        {
            string tmpDir = GetPhysicalPath(logicalPathEnum.ToString());
            if (!Directory.Exists(tmpDir))
            {
                Directory.CreateDirectory(tmpDir);
            }
        }

        /// <summary>
        /// Ensures the directory exists.
        /// </summary>
        /// <param name="logicalPath">The logical path.</param>
        /// <param name="subFolders">array of subfolders completing the path.</param>
        public IResultModel EnsureDirectoryExists(UploadFoldersEnum logicalPathEnum, params string[] subFolders)
        {

            IResultModel rm = new ResultModel();
            string tmpDir = string.Empty;
            int x = 0;

            try
            {
                tmpDir = GetPhysicalPath(logicalPathEnum.ToString());

                if (!Directory.Exists(tmpDir))
                {
                    Directory.CreateDirectory(tmpDir);
                }

                for (x = 0; x < subFolders.Length; x++)
                {
                    tmpDir = GetPhysicalPath(Path.Combine(tmpDir, subFolders[x]));

                    if (!Directory.Exists(tmpDir))
                    {
                        Directory.CreateDirectory(tmpDir);
                    }
                }
                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        public IResultModel EnsureDirectoryExists(params string[] subFolders)
        {

            IResultModel rm = new ResultModel();
            string tmpDir = string.Empty;
            int x = 0;

            try
            {
                tmpDir = GetPhysicalPath(subFolders[0]);

                if (!Directory.Exists(tmpDir))
                {
                    Directory.CreateDirectory(tmpDir);
                }

                for (x = 1; x < subFolders.Length; x++)
                {
                    tmpDir = Path.Combine(tmpDir, subFolders[x]);

                    if (!Directory.Exists(tmpDir))
                    {
                        Directory.CreateDirectory(tmpDir);
                    }
                }
                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }


        /// <summary>
        /// Saves the specified logical path.
        /// </summary>
        /// <param name="logicalPath">The logical path.</param>
        /// <param name="inputStream">The input stream.</param>
        /// <returns></returns>
        public string Save(string logicalPath, Stream inputStream)
        {
            var normalizedLogicalPath = normalizePath(logicalPath);
            var localPath = GetPhysicalPath(normalizedLogicalPath);
            using (var file = new FileStream(localPath, FileMode.Create))
            {
                inputStream.CopyTo(file);
                file.Flush();
            }
            return getFullUrlFor(normalizedLogicalPath);
        }

        /// <summary>
        /// Saves the specified logical path.
        /// </summary>
        /// <param name="logicalPath">The logical path.</param>
        /// <param name="contents">The contents.</param>
        /// <returns></returns>
        public string Save(string logicalPath, byte[] contents)
        {
            using (var stream = new MemoryStream(contents))
            {
                return this.Save(logicalPath, stream);
            }
        }

        /// <summary>
        /// Moves the file.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <returns></returns>
        public string MoveFile(string sourceFilePath, UploadFoldersEnum targetFolderEnum)
        {
            var relativePath = sourceFilePath;
            string targetFolder = targetFolderEnum.ToString();
            if (!relativePath.Contains("~"))
            {
                relativePath = "~" + relativePath;
            }
            var localFilePath = GetPhysicalPath(relativePath);
            if (!targetFolder.EndsWith("/"))
                targetFolder += "/";

            var logicalTargetFile = normalizePath(targetFolder + Path.GetFileName(sourceFilePath));
            var destinationFilePath = GetPhysicalPath(logicalTargetFile);

            if (localFilePath.Contains("admin.png"))
            {
                File.Copy(localFilePath, destinationFilePath, true);
            }
            else
            {
                File.Move(localFilePath, destinationFilePath);
            }
            return getFullUrlFor(logicalTargetFile);
        }

        /// <summary>
        /// Copy the file.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <returns></returns>
        public string CopyFile(string sourceFilePath, string targetFolder)
        {
            var relativePath = sourceFilePath;
            if (!relativePath.Contains("~"))
            {
                relativePath = "~" + relativePath;
            }
            var localFilePath = GetPhysicalPath(relativePath);
            if (!targetFolder.EndsWith("/"))
                targetFolder += "/";

            var logicalTargetFile = normalizePath(targetFolder + Guid.NewGuid() + Path.GetExtension(sourceFilePath));
            var destinationFilePath = GetPhysicalPath(logicalTargetFile);

            File.Copy(localFilePath, destinationFilePath, true);

            return getFullUrlFor(logicalTargetFile);
        }

        /// <summary>
        /// Deletes the specified logical path.
        /// </summary>
        /// <param name="logicalPath">The logical path.</param>
        /// <returns></returns>
        public bool Delete(string logicalPath)
        {
            //var normalizedLogicalPath = normalizePath(logicalPath);
            //var localPath = GetPhysicalPath(normalizedLogicalPath);
            var localPath = logicalPath;
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes the specified local file.
        /// </summary>
        /// <param name="url">Local url where file is.</param>
        /// <returns>Returns true if file is deleted.</returns>
        public bool DeleteFromRelativeUrl(string url)
        {
            var localPath = this.GetPath(url);
            return this.Delete(localPath);
        }

        /// <summary>
        /// Deletes the specified file in the specified folder.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="folderEnum">The folder Enum.</param>
        /// <returns></returns>
        public bool Delete(string fileName, UploadFoldersEnum folderEnum)
        {
            string path = GetPath(fileName, folderEnum);
            return Delete(path);
        }

        /// <summary>
        /// Get the physical path for the specified logical path 
        /// </summary>
        public string GetPhysicalPath(string logicalPath)
        {
            return HttpContext.Current.Server.MapPath(normalizePath(logicalPath));
        }

        /// <summary>
        /// Get the physical path for the specified logical path 
        /// </summary>       

        public string GetPhysicalPathWithoutContext(string logicalPath)
        {
            string physicalPath = string.Empty;

            //var context = new HttpContext()

            return physicalPath;
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="newFolder">The new folder.</param>
        /// <returns></returns>
        public string CreateFolder(string path, string newFolder)
        {
            //var tmpDir = string.Format("{0}/{1}", path, newFolder);
            var tmpDir = Path.Combine(path, newFolder);
            //var logicalParth = GetPhysicalPath(tmpDir);
            if (!Directory.Exists(tmpDir))
            {
                Directory.CreateDirectory(tmpDir);
            }

            return tmpDir;
        }

        public string GetPath(string fileName)
        {
            return HostingEnvironment.MapPath(fileName);
        }

        public string GetRelativeFolderPath(UploadFoldersEnum folderEnum)
        {
            return string.Format(@"{0}/{1}", _basePath, folderEnum.ToString());
        }

        public string GetPath(string fileName, UploadFoldersEnum folderEnum)
        {
            return HostingEnvironment.MapPath(GetRelativePath(fileName, folderEnum.ToString()));
        }
        public string GetFolderPhysicalPath(params string[] folders)
        {
            var tmpDir = this._basePath + string.Join("/", folders);
            return HostingEnvironment.MapPath(tmpDir);
        }

        /// <summary>
        /// Get relative path.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="folder">The file folder.</param>
        /// <returns></returns>
        public string GetRelativePath(string fileName, string folder)
        {
            return string.Format(@"{0}/{1}/{2}", _basePath, folder, fileName);
        }

        public string GetRelativePath(string fileName, params string[] folders)
        {
            string dir = _basePath;

            for (var x = 0; x < folders.Length; x++)
            {
                dir = string.Format(@"{0}{1}/", dir, folders[x]);
            }

            dir = string.Format(@"{0}{1}", dir, fileName);

            return dir;
        }


        public IResultModel DeleteFolderPhysicalPath(params string[] folders)
        {
            IResultModel rm = new ResultModel();
            string tmpDir = string.Empty,
                physicalDir = string.Empty;

            try
            {
                tmpDir = this._basePath + string.Join("/", folders);
                physicalDir = HostingEnvironment.MapPath(tmpDir);

                if (Directory.Exists(physicalDir))
                {
                    return this.DeleteDirectory(physicalDir);                    
                }

                rm.OnError(ErrorResources.TemporaryFolderNotFoundForDeletion);
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;

        }


        #region Private members

        /// <summary>
        /// Gets the full URL for a logical Path. By default, the logical path is returned, but
        /// it is a chance to transform the URL before returning it to the client.
        /// </summary>
        private string getFullUrlFor(string logicalPath)
        {
            var result = logicalPath;

            return result;
        }

        /// <summary>
        /// Normalizes the logical path, removing protocol, host and port, and ensuring the path starts with basePath.
        /// </summary>
        private string normalizePath(string logicalPath)
        {
            if (string.IsNullOrWhiteSpace(logicalPath))
            {
                return _basePath;
            }

            if (logicalPath.StartsWith("http://") || logicalPath.StartsWith("https://"))
            {
                logicalPath = new Uri(logicalPath).PathAndQuery;
            }

            if (logicalPath.StartsWith(_basePath))
            {
                return logicalPath;
            }
            else
            {
                return _basePath + logicalPath.TrimStart('/');
            }
        }


        private IResultModel DeleteDirectory(string target_dir)
        {

            IResultModel rm = new ResultModel();
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            try
            {
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);

                rm.OnSuccess();
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
                
            }

            return rm;

        }
        #endregion
    }
}
