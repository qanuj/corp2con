using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using Talent21.Service.Models.Core;

namespace Talent21.Service.Core
{
    public class SystemService : ISystemService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IIndustryRepository _industryRepository;

        public SystemService(ILocationRepository locationRepository,
            IIndustryRepository industryRepository
            )
        {
            _locationRepository = locationRepository;
            _industryRepository = industryRepository;
        }

        public int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public SystemAddIndustryModel AddIndustry(SystemAddIndustryModel model)
        {
            throw new System.NotImplementedException();
        }

        public SystemEditIndustryModel EditIndustry(SystemEditIndustryModel model)
        {
            var industry = _industryRepository.ById(model.Id);
            industry.Title = model.Title;
            _industryRepository.Update(industry);
            _industryRepository.SaveChanges();
            return model;
        }

        public SystemDeleteIndustryModel DeleteIndustry(SystemDeleteIndustryModel model)
        {
            throw new System.NotImplementedException();
        }

        public SystemViewIndustryModel ViewIndustry(SystemViewIndustryModel model)
        {
            throw new System.NotImplementedException();
        }

        public SystemAddSkillModel AddSkill(SystemAddSkillModel model)
        {
            throw new System.NotImplementedException();
        }

        public SystemEditSkillModel EditSkill(SystemEditSkillModel model)
        {
            throw new System.NotImplementedException();
        }

        public SystemDeleteSkillModel DeleteSkill(SystemDeleteSkillModel model)
        {
            throw new System.NotImplementedException();
        }

        public SystemViewSkillModel ViewSkill(SystemViewSkillModel model)
        {
            throw new System.NotImplementedException();
        }


        public LocationViewModel AddLocation(LocationCreateViewModel model)
        {
            var location = new Location
            {
                State = model.State, 
                Country = model.Country, 
                PinCode = model.PinCode
            };
            _locationRepository.Create(location);
            _locationRepository.SaveChanges();
            return new LocationViewModel
            {
                Id=location.Id,
                State = location.State,
                Country = location.Country,
                PinCode = location.PinCode
            };
        }

    }
}