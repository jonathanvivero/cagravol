using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using SGE.Cagravol.Presentation.Resources.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Presentation.Resources.Common;

namespace SGE.Cagravol.Infrastructure.Data.Repositories.Customers
{
    public class CustomerRepository
        : BaseRepository<Customer>, ICustomerRepository
    {        
        public CustomerRepository(ISGEContext context)
            : base(context)
        {

        }

        public IResultServiceModel<IEnumerable<Customer>> GetListByProjectId(long id)
        {
            IResultServiceModel<IEnumerable<Customer>> rsm = new ResultServiceModel<IEnumerable<Customer>>();

            try
            {
                var list = this.context.Customers.Where(w => w.ProjectId == id);

                rsm.OnSuccess(list);

            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<IEnumerable<string>> GetUserIdListByProjectId(long id)
        {
            IResultServiceModel<IEnumerable<string>> rsm = new ResultServiceModel<IEnumerable<string>>();

            try
            {
                var list = this.context
                    .Customers
                    .Where(w => w.ProjectId == id)
                    .Select(s=>s.UserId);
                    //.ToList();

                rsm.OnSuccess(list);
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<Customer> GetCustomerbyProjectCode(string signUpCode, bool mustBeFree = true)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            signUpCode = signUpCode.ToUpper();
            try
            {
                var customer = this.context.Customers
                    .Where(w => w.SignUpCode == signUpCode)
                    .FirstOrDefault();

                if (customer != null)
                {
                    if (mustBeFree)
                    {
                        if (string.IsNullOrWhiteSpace(customer.Email))
                        {
                            rsm.OnSuccess(customer);
                        }
                        else
                        {
                            rsm.OnError(CustomerResources.CustomerCodeHasBeenUsed);
                        }
                    }
                    else
                    {
                        rsm.OnSuccess(customer);
                    }
                }
                else
                {
                    rsm.OnError(CustomerResources.CustomerCodeDoesNotExist);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<Customer> FindByUserId(string userId)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                var item = this.context
                    .Customers
                    .Where(w => w.UserId == userId)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item);
                }
                else 
                {
                    rsm.OnError(CustomerResources.CustomerNotFoundForAnyActiveProject, EnumErrorCode.CUSTOMER_USER_NOT_FOUND_FOR_ANY_PROJECT);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<Customer> GetById(long id)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                var item = this.context
                    .Customers
                    .Where(w => w.Id == id)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item);
                }
                else
                {
                    rsm.OnError(CustomerResources.CustomerNotFoundForAnyActiveProject, EnumErrorCode.CUSTOMER_USER_NOT_FOUND_FOR_ANY_PROJECT);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultModel AddCustomerForProject(IEnumerable<Customer> customerList)
        {
            IResultModel rm = new ResultModel();

            try
            {
                this.context.Customers.AddRange(customerList);
                this.context.SaveChanges();

                return rm.OnSuccess();
            }
            catch (Exception ex)
            {
                return rm.OnException(ex);
            }
        }
        public IResultServiceModel<Customer> GetGeneralSpaceCustomerByProject(long projectId)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                Customer item = this.context
                    .Customers
                    .Where(w => w.ProjectId == projectId && w.SpaceNumber == 0)
                    .FirstOrDefault();

                if (item != null)
                {
                    rsm.OnSuccess(item);
                }
                else
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }
        public IResultServiceModel<Customer> FindByIdAndProject(long customerId, long projectId)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                var cuz = this.context
                    .Customers
                    .Where(w => w.Id == customerId && w.ProjectId == projectId)
                    .SingleOrDefault();

                if (cuz != null)
                {
                    rsm.OnSuccess(cuz);
                }
                else 
                {
                    rsm.OnError(ErrorResources.ItemDoesNotExist, EnumErrorCode.ITEM_DOES_NOT_EXIST);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
                
            }
            return rsm;
        }
    }
}
