using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository 
    {
        public CompanyRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {

        }
        public IEnumerable<Company>GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(x => x.Name).ToList();
        }
        public Company GetCompany(Guid companyId, bool trackChanges)
        {
            var result = FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();
            return result;
        }
    }
}
