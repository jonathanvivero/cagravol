using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.SeedData
{
    internal abstract class BaseSeed<T>
    {
        internal abstract T[] GenerateData();

    }
}
