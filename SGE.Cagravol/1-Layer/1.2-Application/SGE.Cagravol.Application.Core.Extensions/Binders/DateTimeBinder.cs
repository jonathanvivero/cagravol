using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.Binders
{
    public class DateTimeBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            string[] dateFormats = new string[]{
				CommonResources.DateTimeBinder_DateTimeFormat,
				CommonResources.DateTimeBinder_DateTimeDayMonthYearHourMinuteFormat,
				CommonResources.DateTimeBinder_DateTimeDayMonthYearHourMinuteSecondsFormat,
				CommonResources.DateTimeBinder_DateTimeMonthYearFormat
			};

            var date = DateTime.ParseExact(value.AttemptedValue, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);

            return date;
        }
    }
}
