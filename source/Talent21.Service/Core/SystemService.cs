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

        public IndustryAddViewModel AddIndustry(IndustryAddViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public IndustryEditViewModel EditIndustry(IndustryEditViewModel model)
        {
            var industry = _industryRepository.ById(model.Id);
            industry.Title = model.Title;
            _industryRepository.Update(industry);
            _industryRepository.SaveChanges();
            return model;
        }

        public IndustryDeleteViewModel DeleteIndustry(IndustryDeleteViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public IndustryViewModel ViewIndustry(IndustryViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public SkillAddViewModel AddSkill(SkillAddViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public SkillEditViewModel EditSkill(SkillEditViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public SkillDeleteViewModel DeleteSkill(SkillDeleteViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public SkillViewModel ViewSkill(SkillViewModel model)
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