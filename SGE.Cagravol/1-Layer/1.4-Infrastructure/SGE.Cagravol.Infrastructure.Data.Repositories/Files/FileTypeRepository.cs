using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.JSON.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Files;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Files
{
    public class FileTypeRepository
        : BaseRepository<FileType>, IFileTypeRepository
    {
        public FileTypeRepository(ISGEContext context)
			: base(context)
		{

		}


        public IResultServiceModel<IEnumerable<FileTypeJSON>> GetAllJSON() 
        {
            IResultServiceModel<IEnumerable<FileTypeJSON>> rsm = new ResultServiceModel<IEnumerable<FileTypeJSON>>();

            try
            {
                var list = this.context
                    .FileTypes
                    .Select(s => new FileTypeJSON() { FileExtension = s.FileExtension, Id = s.Id, Name = s.Name, Notes = s.Notes });
                
                rsm.OnSuccess(list);
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
                
            }

            return rsm;
        }
        
    }
}
