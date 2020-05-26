using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SGE.Cagravol.Application.ServiceModel;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Base
{
	/// <summary>
	/// Base class to implements services that access to the data layer.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BaseUserBasedRepository<T> : IBaseUserBasedRepository<T>
		where T : class, IEntityUserBasedIdentity, new()
	{
		/// <summary>
		/// The current context
		/// </summary>
        protected readonly ISGEContext context;

		#region Initialization
        public BaseUserBasedRepository(ISGEContext context)
		{
			this.context = context;
		}
		#endregion

		/// <summary>
		/// Creates an instance.
		/// </summary>
		/// <returns></returns>
        public virtual IResultServiceModel<T> Create()
        {
            IResultServiceModel<T> rv = new ResultServiceModel<T>();

            try
            {
                return rv.OnSuccess(
                    this.context
                        .Set<T>()
                        .Create()
                        );
            }
            catch (Exception ex)
            {
                return rv.OnException(ex);
            }
        }


		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
        public virtual IResultModel Delete(T entity)
        {
            try
            {
                this.context
                .Set<T>()
                .Remove(entity);

                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }


        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IResultServiceModel<IEnumerable<T>> GetAll()
        {
            IResultServiceModel<IEnumerable<T>> rv = new ResultServiceModel<IEnumerable<T>>();
            try
            {
                return rv.OnSuccess(
                    this.context
                    .Set<T>()
                    );
            }
            catch (Exception ex)
            {
                return rv.OnException(ex);
            }
        }

        /// <summary>
        /// Adds the specified entity to the current context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public IResultModel Add(T entity)
        {
            try
            {
                this.context
                .Set<T>()
                .Add(entity);

                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }

        /// <summary>
        /// Removes the specified entity from the current context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual IResultModel Remove(T entity)
        {
            try
            {
                this.context
                    .Set<T>()
                    .Remove(entity);
                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }

        /// <summary>
        /// Counts the number of the elements from this entity that are in the current context.
        /// </summary>
        public IResultServiceModel<int> Count()
        {
            IResultServiceModel<int> rv = new ResultServiceModel<int>();
            try
            {
                return rv.OnSuccess(
                    this.context
                    .Set<T>()
                    .Count()
                );
            }
            catch (Exception ex)
            {
                return rv.OnException(ex);
            }

        }


		/// <summary>
		/// Counts the number of the elements from this entity that are in the current context.
		/// </summary>
		/// <returns></returns>
        public async Task<IResultServiceModel<int>> CountAsync()
        {
            IResultServiceModel<int> rv = new ResultServiceModel<int>();

            try
            {
                return rv.OnSuccess(
                    await this.context
                    .Set<T>()
                    .CountAsync()
                );
            }
            catch (Exception ex)
            {
                return rv.OnException(ex);
            }
        }

		/// <summary>
		/// Finds an entity with the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
        public virtual IResultServiceModel<T> Find(string id)
        {
            IResultServiceModel<T> rv = new ResultServiceModel<T>();
            try
            {
                var entity = this.context
                    .Set<T>()
                    .Where(i => i.UserId == id)
                    .SingleOrDefault();

                return rv.OnSuccess(entity);
            }
            catch (Exception ex)
            {
                return rv.OnException(ex);
            }
        }

        public async Task<IResultServiceModel<T>> FindAsync(string id)
        {
            IResultServiceModel<T> rv = new ResultServiceModel<T>();
            try
            {
                var entity = await this.context
                    .Set<T>()
                    .Where(i => i.UserId == id)
                    .SingleOrDefaultAsync();

                return rv.OnSuccess(entity);
            }
            catch (Exception ex)
            {
                return rv.OnException(ex);
            }
        }
        public IResultModel Update(T entity)
        {
            try
            {
                this.context.SetState(entity, EntityState.Modified);
                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }

        public IResultModel SetForRemoval(T entity)
        {
            try
            {
                this.context.SetState(entity, EntityState.Deleted);
                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }
        public IResultModel Save()
        {
            try
            {
                this.context.SaveChanges();
                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }
        }

        public async Task<IResultModel> SaveAsync()
        {
            try
            {
                await this.context.SaveChangesAsync();
                return new ResultModel().OnSuccess();
            }
            catch (Exception ex)
            {
                return new ResultModel().OnException(ex);
            }


        }
	}
}
