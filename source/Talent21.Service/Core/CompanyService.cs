using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;

namespace Talent21.Service.Core
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public void CreateCompany(string name)
        {
            _companyRepository.Create(new Company(){ Name = name });
        }

        public int SaveChanges()
        {
            return _companyRepository.SaveChanges();
        }
    }
}