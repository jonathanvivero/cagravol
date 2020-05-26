using SGE.Cagravol.Application.Core.Extensions.AuthorizationRules;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.Filters.AuthorizationRules
{
	public abstract class AuthorizeByRulesAttribute : ActionFilterAttribute
	{
		private readonly List<Tuple<IAuthorizationRule, RuleConcatenationModeEnum>> rules;
		private readonly Dictionary<RuleConcatenationModeEnum, Func<bool?, bool, bool?>> indexedRules;

		public AuthorizeByRulesAttribute()
		{
			this.rules = new List<Tuple<IAuthorizationRule, RuleConcatenationModeEnum>>();
			this.indexedRules = new Dictionary<RuleConcatenationModeEnum, Func<bool?, bool, bool?>>();

			this.indexedRules.Add(RuleConcatenationModeEnum.And, this.And);
			this.indexedRules.Add(RuleConcatenationModeEnum.Or, this.Or);
			this.indexedRules.Add(RuleConcatenationModeEnum.OrNot, this.OrNot);
			this.indexedRules.Add(RuleConcatenationModeEnum.None, this.None);
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			bool? lastResult = null;
			RuleConcatenationModeEnum lastRule = RuleConcatenationModeEnum.None;

			foreach (var rule in this.rules)
			{
				var currentResult = rule.Item1.IsValid(filterContext);

				lastResult = lastResult.HasValue ?
					this.indexedRules[lastRule].Invoke(lastResult, currentResult) : currentResult;

				lastRule = rule.Item2;
			}

			if (!lastResult.Value)
			{
				this.SetUnAuthorizedRequest(filterContext);
			}

			this.rules.Clear();
		}

		protected void InitializeRules(params RuleDefinition[] ruleDefinitions)
		{
			foreach (var ruleDefinition in ruleDefinitions)
			{
				if (ruleDefinition.Rule == null)
				{
					throw new InvalidOperationException();
				}
				this.rules.Add(new Tuple<IAuthorizationRule, RuleConcatenationModeEnum>(ruleDefinition.Rule, ruleDefinition.Mode));
			}
		}

		protected void InitializeRules(params IAuthorizationRule[] authorizationRules)
		{
			foreach (var authorizationRule in authorizationRules)
			{
				if (authorizationRule == null)
				{
					throw new InvalidOperationException();
				}
				this.rules.Add(new Tuple<IAuthorizationRule, RuleConcatenationModeEnum>(authorizationRule, RuleConcatenationModeEnum.None));
			}
		}

		private void SetUnAuthorizedRequest(ActionExecutingContext filterContext)
		{
			filterContext.Result = new JsonResult
			{
				Data = new { Message = "Unauthorize" },
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
			filterContext.HttpContext.Response.StatusCode = 403;
			filterContext.HttpContext.Response.StatusDescription = CommonResources.NotPermissions;
		}

		private bool? And(bool? lastResult, bool currentResult)
		{
			var result = lastResult;
			if (currentResult == false)
			{
				result = false;
			}

			return result;
		}

		private bool? Or(bool? lastResult, bool currentResult)
		{
			return !(lastResult.Value == false && currentResult == false);
		}

		private bool? OrNot(bool? lastResult, bool currentResult)
		{
			return Or(lastResult, !currentResult);
		}

		private bool? None(bool? lastResult, bool currentResult)
		{
			return currentResult;
		}
	}
}
