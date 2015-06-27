using System.Linq;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

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

        public bool Delete(IndustryDeleteViewModel model)
        {
            _industryRepository.Delete(model.Id);
            var rowsAffested = _industryRepository.SaveChanges();
            return rowsAffested > 0;
        }
        public bool Delete(SkillDeleteViewModel model)
        {
            _skillRepository.Delete(model.Id);
            var rowsAffested = _skillRepository.SaveChanges();
            return rowsAffested > 0;
        }
        public bool Delete(LocationDeleteViewModel model)
        {
            _locationRepository.Delete(model.Id);
            var rowsAffested = _locationRepository.SaveChanges();
            return rowsAffested > 0;
        }


        public IndustryEditViewModel Create(IndustryCreateViewModel model)
        {
            var entity = new Industry
            {
                Code = model.Code,
                Title = model.Title
            };
            _industryRepository.Create(entity);
            _industryRepository.SaveChanges();
            return new IndustryEditViewModel()
            {
                Code = entity.Code,
                Title = entity.Title,
                Id = entity.Id
            };
        }

        public SkillEditViewModel Create(SkillCreateViewModel model)
        {
            var entity = new Skill
            {
                Code = model.Code,
                Title = model.Title
            };
            _skillRepository.Create(entity);
            _skillRepository.SaveChanges();
            return new SkillEditViewModel()
            {
                Code = entity.Code,
                Title = entity.Title,
                Id = entity.Id
            };
        }
        public LocationEditViewModel Create(LocationCreateViewModel model)
        {
            var entity = new Location
            {
                Code = model.Code,
                Title = model.Title,
                PinCode = model.PinCode,
                Country = model.Country,
                State = model.State
            };

            _locationRepository.Create(entity);
            _locationRepository.SaveChanges();
            return new LocationEditViewModel()
            {
                Code = entity.Code,
                Title = entity.Title,
                PinCode = entity.PinCode,
                Country = entity.Country,
                State = entity.State,
                Id = entity.Id
            };
        }


        public IndustryEditViewModel Update(IndustryEditViewModel model)
        {
            var entity = _industryRepository.ById(model.Id);
            if (entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;

            _industryRepository.Update(entity);
            _industryRepository.SaveChanges();

            return model;
        }

        public SkillEditViewModel Update(SkillEditViewModel model)
        {
            var entity = _skillRepository.ById(model.Id);
            if(entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;

            _skillRepository.Update(entity);
            _skillRepository.SaveChanges();

            return model;
        }

        public LocationEditViewModel Update(LocationEditViewModel model)
        {
            var entity = _locationRepository.ById(model.Id);
            if(entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;
            entity.Country = model.Country;
            entity.State = model.State;
            entity.PinCode = model.PinCode;

            _locationRepository.Update(entity);
            _locationRepository.SaveChanges();

            return model;
        }

        public IQueryable<IndustryViewModel> Industries
        {
            get {
                return _industryRepository.All.Select(x=> new IndustryViewModel
                {
                    Id=x.Id,
                    Code = x.Code,
                    Title = x.Title
                }); 
            }
        }

        public IQueryable<LocationViewModel> Locations
        {
            get
            {
                return _locationRepository.All.Select(x => new LocationViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Title = x.Title,
                    State = x.State,
                    Country = x.Country,
                    PinCode = x.PinCode
                });
            }
        }

        public IQueryable<SkillViewModel> Skills
        {
            get
            {
                return _skillRepository.All.Select(x => new SkillViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Title = x.Title
                });
            }
        }
    }
}