using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Base
{
	/// <summary>
	/// Provide methods to get elements by filter and paginated
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IFilteredRepository<T>
	{
		/// <summary>
		/// Gets the by filter.
		/// </summary>
		/// <param name="filterText">The filter text.</param>
		/// <returns>A IEnumerable collection of entities that match with the filter text</returns>
		IResultServiceModel<IEnumerable<T>> GetByFilter(string filterText);

		/// <summary>
		/// Gets the count of the results.
		/// </summary>
		/// <param name="filterText">The filter text.</param>
		/// <returns>The count of entities that match with the filter text</returns>
        IResultServiceModel<int> GetByFilterCount(string filterText);

		/// <summary>
		/// Gets the by filter and page.
		/// </summary>
		/// <param name="filterText">The filter text.</param>
		/// <param name="page">The page.</param>
		/// <returns>A IEnumerable collection of entities that match with the filter text</returns>
        IResultServiceModel<IEnumerable<T>> GetByFilterAndPage(string filterText, int page);
	}
}
