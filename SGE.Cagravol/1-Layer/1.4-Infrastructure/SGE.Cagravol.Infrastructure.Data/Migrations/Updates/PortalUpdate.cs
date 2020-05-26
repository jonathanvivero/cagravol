using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.Updates
{
    public abstract class PortalUpdate : IPortalUpdate
    {
        private string logPath = string.Empty;
        private int logCounter = 0;
        protected SGEContext context;
        protected StreamWriter wLog;
        
        public AppConfiguration keyVersion { get;set;}


        public PortalUpdate(SGEContext _context, AppConfiguration _keyVersion) 
        {
            this.context = _context;
            this.keyVersion = _keyVersion;
            
        }

        private string MapPath(string seedFile)
        {
            //if (HttpContext.Current != null)
            //    return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, "..\\.." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }

        protected void log (string note)
        {
            this.dumpAnything(note);            
        }
        protected void log(string where, string note)
        {            
            this.dumpAnything(string.Format("{0} => {1}", where, note));
        }


        public void DumpExeption(Exception ex)
        {

            this.dumpAnything(Newtonsoft.Json.JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented));                        
        }

        private void dumpAnything(string s)
        {
            var _logPath = string.Format("~/Dumps/Error_Log_{0:D4}.txt", this.logCounter++);

            _logPath = this.MapPath(_logPath);

            using (StreamWriter w = File.AppendText(_logPath))
            {
                w.Write(s);
            }
        }

        public abstract AppConfiguration DoUpdate();
    }
}
