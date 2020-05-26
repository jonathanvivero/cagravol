using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Core.Enums.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Common
{
    public class AppConfiguration
        : IEntityIdentity
    {
        public long Id { get; set; }
        public string Key {get;set;}
        public string Value { get; set; }
        public EnumAppConfigurationFieldType FieldType { get; set; }
        public bool IsPublicViewable {get;set;}
        public bool IsPublicEnable {get; set;}
    }
}
