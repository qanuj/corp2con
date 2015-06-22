using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;
using Talent21.Service.Models.Core;

namespace Talent21.Service.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemService : ISystemService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IIndustryRepository _industryRepository;
        private readonly ISkillRepository _skillRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationRepository"></param>
        /// <param name="industryRepository"></param>
        /// <param name="skillRepository"></param>
        public SystemService(ILocationRepository locationRepository,
            IIndustryRepository industryRepository, ISkillRepository skillRepository
                )
        {
            _locationRepository = locationRepository;
            _industryRepository = industryRepository;
            _skillRepository = skillRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _industryRepository.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AddIndustryViewModel AddIndustry(AddIndustryViewModel model)
        {
            var industry = new Industry

            {
                IndustryName = model.IndustryName
            };
            _industryRepository.Create(industry);
            _industryRepository.SaveChanges();
            return new AddIndustryViewModel
            {
                IndustryName = industry.IndustryName,
            };
        }

        private void Create(Industry model)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EditIndustryViewModel EditIndustry(EditIndustryViewModel model)
        {
            var industry = _industryRepository.ById(model.Id);
            industry.Title = model.Title;
            _industryRepository.Update(industry);
            _industryRepository.SaveChanges();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DeleteIndustryViewModel DeleteIndustry(DeleteIndustryViewModel model)
        {
            var entity = _industryRepository.ById(model.IndustryId);
            _industryRepository.Delete(entity);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IndustryViewModel ViewIndustry(IndustryViewModel model)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AddSkillViewModel AddSkill(AddSkillViewModel model)
        {
            var Skill = new Skill
            {
                CandidateId = model.CandidateId
            };
            _skillRepository.Create(Skill);
            _skillRepository.SaveChanges();
            return new AddSkillViewModel
            {
                CandidateId = model.CandidateId,
                Skill = model.Skill
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EditSkillViewModel EditSkill(EditSkillViewModel model)
        {
            var entity = _skillRepository.ById(model.CandidateId);
            _skillRepository.Update(entity);
            _skillRepository.SaveChanges();
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DeleteSkillViewModel DeleteSkill(DeleteSkillViewModel model)
        {
            var entity = _industryRepository.ById(model.CandidateId);
            _industryRepository.Delete(entity);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SkillViewModel ViewSkill(SkillViewModel model)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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



        public object AddIndustryViewModel(AddIndustryViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}