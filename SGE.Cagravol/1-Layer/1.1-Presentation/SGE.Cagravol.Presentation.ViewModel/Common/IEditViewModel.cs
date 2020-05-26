using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Presentation.ViewModel.Common
{
    public interface IEditViewModel<T> : IReadableViewModel<T>
       where T : class
    {
        /// <summary>
        /// Copy the entity content to the current view model
        /// </summary>
        /// <param name="entity">The entity.</param>
        void CopyTo(T entity);

        ///// <summary>
        ///// Loads from the current view models to the entity
        ///// </summary>
        ///// <param name="entity">The entity.</param>
        //void LoadFrom(T entity);
    }
}
