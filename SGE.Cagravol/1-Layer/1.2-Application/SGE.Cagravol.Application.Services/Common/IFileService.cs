using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
	public interface IFileService
	{
		/// <summary>
		/// Ensures the directory exists.
		/// </summary>
		/// <param name="logicalPath">The logical path.</param>
		void EnsureDirectoryExists(UploadFoldersEnum logicalPathEnum);

        /// <summary>
        /// Ensures the directory exists.
        /// </summary>
        /// <param name="logicalPath">The logical path.</param>
        /// <param name="subFolders">array of subfolders completing the path.</param>
        IResultModel EnsureDirectoryExists(UploadFoldersEnum logicalPathEnum, params string[] subFolders);
        IResultModel EnsureDirectoryExists(params string[] subFolders);

		/// <summary>
		/// Saves stream in the specified logical path.
		/// </summary>
		/// <param name="logicalPath">The logical path.</param>
		/// <param name="inputStream">The input stream.</param>
		/// <returns></returns>
		string Save(string logicalPath, Stream inputStream);

		/// <summary>
		/// Saves the stream in the specified logical path.
		/// </summary>
		/// <param name="logicalPath">The logical path.</param>
		/// <param name="contents">The contents.</param>
		/// <returns></returns>
		string Save(string logicalPath, byte[] contents);

		/// <summary>
		/// Moves the file.
		/// </summary>
		/// <param name="sourceFilePath">The source file path.</param>
		/// <param name="targetFolder">The target folder.</param>
		/// <returns></returns>
		string MoveFile(string sourceFilePath, UploadFoldersEnum targetFolder);

		/// <summary>
		/// Copy the file.
		/// </summary>
		/// <param name="sourceFilePath">The source file path.</param>
		/// <param name="targetFolder">The target folder.</param>
		/// <returns></returns>
		string CopyFile(string sourceFilePath, string targetFolder);

		/// <summary>
		/// Deletes the specified logical path.
		/// </summary>
		/// <param name="logicalPath">The logical path.</param>
		/// <returns></returns>
		bool Delete(string logicalPath);

		/// <summary>
		/// Deletes the specified file in the specified folder.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="folderEnum">The folder Enum.</param>
		/// <returns></returns>
		bool Delete(string fileName, UploadFoldersEnum folderEnum);

		/// <summary>
		/// Deletes the specified local file.
		/// </summary>
		/// <param name="url">Local url where file is.</param>
		/// <returns>Returns true if file is deleted.</returns>
		bool DeleteFromRelativeUrl(string url);

		/// <summary>
		/// Gets the physical path.
		/// </summary>
		/// <param name="logicalPath">The logical path.</param>
		/// <returns></returns>
		string GetPhysicalPath(string logicalPath);

		/// <summary>
		/// Gets the physical path without context.
		/// </summary>
		/// <param name="logicalPath">The logical path.</param>
		/// <returns></returns>
		string GetPhysicalPathWithoutContext(string logicalPath);

		/// <summary>
		/// Creates the folder.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="newFolder">The new folder.</param>
		/// <returns></returns>
		string CreateFolder(string path, string newFolder);

		/// <summary>
		/// Ger relative folder path
		/// </summary>
		/// <param name="folderEnum">The folder enum.</param>
		/// <returns></returns>
		string GetRelativeFolderPath(UploadFoldersEnum folderEnum);

		/// <summary>
		/// Get file path
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns></returns>
		string GetPath(string fileName);

		/// <summary>
		/// Get the file path
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="folderEnum">The file folder.</param>
		/// <returns></returns>
		string GetPath(string fileName, UploadFoldersEnum folderEnum);

		/// <summary>
		/// Get relative path.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="folder">The file folder.</param>
		/// <returns></returns>
		string GetRelativePath(string fileName, string folder);
        string GetRelativePath(string fileName, params string[] folders);
        string GetFolderPhysicalPath(params string[] folders);
        IResultModel DeleteFolderPhysicalPath(params string[] folders);
	}
}
