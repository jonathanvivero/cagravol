using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Customers
{
    public interface ICustomerRepository
        : IBaseRepository<Customer>
    {
        IResultServiceModel<IEnumerable<Customer>> GetListByProjectId(long id);
        IResultServiceModel<IEnumerable<string>> GetUserIdListByProjectId(long id);
        IResultServiceModel<Customer> GetCustomerbyProjectCode(string signUpCode, bool mustBeFree = true);
        IResultServiceModel<Customer> FindByUserId(string userId);
        IResultServiceModel<Customer> GetById(long id);
        IResultModel AddCustomerForProject(IEnumerable<Customer> customerList);
        IResultServiceModel<Customer> GetGeneralSpaceCustomerByProject(long projectId);
        IResultServiceModel<Customer> FindByIdAndProject(long customerId, long projectId);        

    }
}
