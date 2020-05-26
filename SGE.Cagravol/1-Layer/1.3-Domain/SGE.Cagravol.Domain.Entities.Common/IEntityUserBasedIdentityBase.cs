using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Common
{
    public interface IEntityUserBasedIdentityBase<T>
    {
        T UserId { get; set; }
    }
}
