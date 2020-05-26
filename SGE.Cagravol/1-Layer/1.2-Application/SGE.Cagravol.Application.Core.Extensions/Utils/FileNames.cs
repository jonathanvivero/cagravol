using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Extensions.Utils
{
	public static class FileNames
	{
		public static string GetExcelNameToExport(string moduleName, string sectionName, string extra = "")
		{
			return GetNameToExport(moduleName, sectionName, extra, "xls");
		}

		public static string GetZipNameToExport(string moduleName, string sectionName, string extra = "")
		{
			return GetNameToExport(moduleName, sectionName, extra, "zip");
		}

		public static string GetPdfNameToExport(string moduleName, string sectionName, string extra = "")
		{
			return GetNameToExport(moduleName, sectionName, extra, "pdf");
		}

		private static string GetNameToExport(string moduleName, string sectionName, string extra, string extension)
		{
			var result = moduleName + "_" + sectionName;
			if (!string.IsNullOrEmpty(extra))
			{
				result += "_" + extra;
			}

			result += "_EI_SATELLITE_" + DateTime.UtcNow.ToString("dd-MM-yyyyTHH-mm-ss") + "." + extension;

			return result;
		}
	}
}
