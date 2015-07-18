using System;
using System.Linq;
using System.Threading.Tasks;
using Talent21.Data;
using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{

    public class SystemService : ISystemService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        private readonly IIndustryRepository _industryRepository;
        private readonly ISkillRepository _skillRepository;

        public SystemService(ILocationRepository locationRepository,
           IIndustryRepository industryRepository, ISkillRepository skillRepository, IFunctionalAreaRepository functionalAreaRepository)
        {
            _locationRepository = locationRepository;
            _industryRepository = industryRepository;
            _skillRepository = skillRepository;
            _functionalAreaRepository = functionalAreaRepository;
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

        public bool Delete(FunctionalAreaDeleteViewModel model)
        {
            _functionalAreaRepository.Delete(model.Id);
            var rowsAffested = _functionalAreaRepository.SaveChanges();
            return rowsAffested > 0;
        }


        public IndustryDictionaryEditViewModel Create(IndustryDictionaryCreateViewModel model)
        {
            var entity = new Industry
            {
                Code = model.Code,
                Title = model.Title
            };
            _industryRepository.Create(entity);
            _industryRepository.SaveChanges();
            return new IndustryDictionaryEditViewModel()
            {
                Code = entity.Code,
                Title = entity.Title,
                Id = entity.Id
            };
        }

        public SkillDictionaryEditViewModel Create(SkillDictionaryCreateViewModel model)
        {
            var entity = new Skill
            {
                Code = model.Code,
                Title = model.Title
            };
            _skillRepository.Create(entity);
            _skillRepository.SaveChanges();
            return new SkillDictionaryEditViewModel()
            {
                Code = entity.Code,
                Title = entity.Title,
                Id = entity.Id
            };
        }
        public FunctionalAreaDictionaryEditViewModel Create(FunctionalAreaDictionaryCreateViewModel model)
        {
            var entity = new FunctionalArea
            {
                Code = model.Code,
                Title = model.Title
            };
            _functionalAreaRepository.Create(entity);
            _functionalAreaRepository.SaveChanges();
            return new FunctionalAreaDictionaryEditViewModel
            {
                Code = entity.Code,
                Title = entity.Title,
                Id = entity.Id
            };
        }
        public LocationDictionaryEditViewModel Create(LocationDictionaryCreateViewModel model)
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
            return new LocationDictionaryEditViewModel()
            {
                Code = entity.Code,
                Title = entity.Title,
                PinCode = entity.PinCode,
                Country = entity.Country,
                State = entity.State,
                Id = entity.Id
            };
        }


        public IndustryDictionaryEditViewModel Update(IndustryDictionaryEditViewModel model)
        {
            var entity = _industryRepository.ById(model.Id);
            if (entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;

            _industryRepository.Update(entity);
            _industryRepository.SaveChanges();

            return model;
        }

        public SkillDictionaryEditViewModel Update(SkillDictionaryEditViewModel model)
        {
            var entity = _skillRepository.ById(model.Id);
            if(entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;

            _skillRepository.Update(entity);
            _skillRepository.SaveChanges();

            return model;
        }

        public FunctionalAreaDictionaryEditViewModel Update(FunctionalAreaDictionaryEditViewModel model)
        {
            var entity = _functionalAreaRepository.ById(model.Id);
            if(entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;

            _functionalAreaRepository.Update(entity);
            _functionalAreaRepository.SaveChanges();

            return model;
        }

        public LocationDictionaryEditViewModel Update(LocationDictionaryEditViewModel model)
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

        public IQueryable<IndustryDictionaryViewModel> Industries
        {
            get
            {
                return _industryRepository.All.Select(x => new IndustryDictionaryViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Title = x.Title
                });
            }
        }


        public IQueryable<FunctionalAreaDictionaryViewModel> FunctionalAreas
        {
            get
            {
                return _functionalAreaRepository.All.Select(x => new FunctionalAreaDictionaryViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Title = x.Title
                });
            }
        }

        public IQueryable<LocationDictionaryViewModel> Locations
        {
            get
            {
                return _locationRepository.All.Select(x => new LocationDictionaryViewModel
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

        public IQueryable<SkillDictionaryViewModel> Skills
        {
            get
            {
                return _skillRepository.All.Select(x => new SkillDictionaryViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Title = x.Title
                });
            }
        }


        public string Upgrade()
        {
            return IndustryRepository.Upgrade();
        }
    }
}