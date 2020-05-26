using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.History;
using SGE.Cagravol.Infrastructure.Data;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Workflows
{
    public class WFFileStateNoteRepository
        : BaseRepository<WFFileStateNote>, IWFFileStateNoteRepository
    {
        public WFFileStateNoteRepository(ISGEContext context)
			: base(context)
		{
		}

        public IResultServiceModel<IEnumerable<WFFileStateNote>> GetListByFileId(long id)
        {
            IResultServiceModel<IEnumerable<WFFileStateNote>> rsm = new ResultServiceModel<IEnumerable<WFFileStateNote>>();

            return rsm;
        }

        public IResultServiceModel<IEnumerable<WFFileStateNote>> GetListByFileHistoryItemId(long id)
        {
            IResultServiceModel<IEnumerable<WFFileStateNote>> rsm = new ResultServiceModel<IEnumerable<WFFileStateNote>>();

            var item = this.context
                .WFFileStateNotes
                .Where(w => w.WFEntityState.EntityId == id);

            if (item != null)
            {
                rsm.OnSuccess(item);
            }
            else
            {
                rsm.OnError("", "");
            }

            return rsm;
        }

    }
}
