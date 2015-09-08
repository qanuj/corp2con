using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using e10.Shared.Util;
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
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICountryRepository _countryRepository;

        protected readonly IMemberRepository _memberRepository;
        protected readonly SellingOptions _sellingOptions;

        public SystemService(ILocationRepository locationRepository,
           IIndustryRepository industryRepository, ISkillRepository skillRepository, IFunctionalAreaRepository functionalAreaRepository, ITransactionRepository transactionRepository, ICountryRepository countryRepository, IMemberRepository memberRepository, SellingOptions sellingOptions)
        {
            _locationRepository = locationRepository;
            _industryRepository = industryRepository;
            _skillRepository = skillRepository;
            _functionalAreaRepository = functionalAreaRepository;
            _transactionRepository = transactionRepository;
            _countryRepository = countryRepository;
            _memberRepository = memberRepository;
            _sellingOptions = sellingOptions;
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

        public bool Delete(CountryDeleteViewModel model)
        {
            _countryRepository.Delete(model.Id);
            var rowsAffested = _countryRepository.SaveChanges();
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

        public CountryDictionaryEditViewModel Create(CountryDictionaryCreateViewModel model)
        {
            var entity = new Country
            {
                Code = model.Code,
                Title = model.Title
            };
            _countryRepository.Create(entity);
            _countryRepository.SaveChanges();
            return new CountryDictionaryEditViewModel()
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

        public CountryDictionaryEditViewModel Update(CountryDictionaryEditViewModel model)
        {
            var entity = _countryRepository.ById(model.Id);
            if (entity == null) return null;

            entity.Code = model.Code;
            entity.Title = model.Title;

            _countryRepository.Update(entity);
            _countryRepository.SaveChanges();

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

        public IQueryable<CountryDictionaryViewModel> Countries
        {
            get
            {
                return _countryRepository.All.Select(x => new CountryDictionaryViewModel
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


        public EnumList Enums()
        {
            var types = ReflectionHelper.GetEnumTypesInNamespace("Talent21.Data.Core", "Talent21");
            var vals = new EnumList();
            foreach(var type in types)
            {
                vals.Add(type.Name, Enum.GetNames(type).Select(x => new IdLabel<string> { Id = x, Label = x.ToUnCamel()}));
            }
            return vals;
        }

        public IQueryable<Transaction> Transactions()
        {
            return _transactionRepository.All;
        }

        public InvoiceViewModel TransactionById(int id)
        {
            var transaction = Transactions().FirstOrDefault(x => x.Id == id);
            if (transaction == null) return null;
            var member = _memberRepository.ByUserId(transaction.UserId);
            return new InvoiceViewModel()
            {
                TaxAmount = transaction.Amount * _sellingOptions.TaxRate / 100,
                Tax = _sellingOptions.TaxRate,
                TaxName = _sellingOptions.TaxName,
                Id = transaction.Id,
                Created = transaction.Created,
                Total = transaction.Amount,
                UnitPrice = _sellingOptions.CreditPrice,
                Member = new MemberViewModel
                {
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Location = member.Location != null ? member.Location.Title : string.Empty,
                    AlternateNumber = member.AlternateNumber,
                    Mobile = member.Mobile,
                    Address = member.Address,
                    PinCode = member.PinCode,
                    Email = member.Email
                },
                Transactions = new List<Transaction> {
                    transaction
                }
            };
        }


        public string Hash(string email)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string. 
        }
    }
}