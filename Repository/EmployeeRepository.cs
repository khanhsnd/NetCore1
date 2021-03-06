using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
  public  class EmployeeRepository: RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
                
        }
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
        {
            var result = FindByCondition(x => x.CompanyId.Equals(companyId), trackChanges);
            return result;
        }
    }
}
