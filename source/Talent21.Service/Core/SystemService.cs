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
        private readonly ISkillRepository _skillRepository;

        public SystemService(ILocationRepository locationRepository,
            IIndustryRepository industryRepository, ISkillRepository skillRepository
                )
        {
            _locationRepository = locationRepository;
            _industryRepository = industryRepository;
            _skillRepository = skillRepository;
        }

        public int SaveChanges()
        {
            return _industryRepository.SaveChanges();
        }

        public IndustryAddViewModel AddIndustry(IndustryAddViewModel model)
        {
            var industry = new Industry

            {
                IndustryName = model.IndustryName
            };
            _industryRepository.Create(industry);
            _industryRepository.SaveChanges();
            return new IndustryAddViewModel
            {
                IndustryName = industry.IndustryName,
            };
        }

        private void Create(Industry model)
        {
            
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
            var entity = _industryRepository.ById(model.IndustryId);
            _industryRepository.Delete(entity);
            return model;
        }

        public IndustryViewModel ViewIndustry(IndustryViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public SkillAddViewModel AddSkill(SkillAddViewModel model)
        {
            var Skill = new Skill
            {
                CandidateId = model.CandidateId
            };
            _skillRepository.Create(Skill);
            _skillRepository.SaveChanges();
            return new SkillAddViewModel
            {
                CandidateId = model.CandidateId,
                Skill = model.Skill
            };
        }
        
        public SkillEditViewModel EditSkill(SkillEditViewModel model)
        {
            var entity = _skillRepository.ById(model.CandidateId);
            _skillRepository.Update(entity);
            _skillRepository.SaveChanges();
            return model;
        }

        public SkillDeleteViewModel DeleteSkill(SkillDeleteViewModel model)
        {
            var entity = _industryRepository.ById(model.CandidateId);
            _industryRepository.Delete(entity);
            return model;
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