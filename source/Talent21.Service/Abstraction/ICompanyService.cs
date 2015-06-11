namespace Talent21.Service.Abstraction
{
    public interface ICompanyService : IService
    {
        void CreateCompany(string name);
        CompanyProfileViewModel UpdateProfile(CompanyProfileViewModel profile);
       CompanyAddProfileViewModel AddProfile(CompanyAddProfileViewModel profile1); 


    }
}