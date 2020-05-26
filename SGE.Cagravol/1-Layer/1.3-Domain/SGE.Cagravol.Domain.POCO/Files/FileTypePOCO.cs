using SGE.Cagravol.Domain.Entities.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Files
{
    public class FileTypePOCO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string FileExtension { get; set; }

        public FileTypePOCO() { }
        public FileTypePOCO(FileType fileType) 
        {
            this.Id = fileType.Id;
            this.Name = fileType.Name;
            this.Notes = fileType.Notes;
            this.FileExtension = fileType.FileExtension;

        }
    }
}
