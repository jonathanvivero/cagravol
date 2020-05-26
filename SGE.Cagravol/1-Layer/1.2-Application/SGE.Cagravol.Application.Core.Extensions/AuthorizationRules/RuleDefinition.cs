using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Extensions.AuthorizationRules
{
    public struct RuleDefinition
    {
        public IAuthorizationRule Rule;

        public RuleConcatenationModeEnum Mode;

        public RuleDefinition(IAuthorizationRule rule, RuleConcatenationModeEnum mode)
            : this()
        {
            this.Rule = rule;
            this.Mode = mode;
        }
    }
}
