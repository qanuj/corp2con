using Talent21.Data.Core;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IScheduleRepository _scheduleRepository;

        public CandidateService(ICandidateRepository candidateRepository,
            IJobApplicationRepository jobApplicationRepository,
            IScheduleRepository scheduleRepository)
        {
            _candidateRepository = candidateRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _scheduleRepository = scheduleRepository;
        }

        public CandidateViewModel CreateCandidate(CandidateCreateViewModel profile)
        {
            var candidate = new Candidate() { Name = profile.Name };
            _candidateRepository.Create(candidate);
            _candidateRepository.SaveChanges();
            return new CandidateViewModel
            {
                Name = candidate.Name,
                Id = candidate.Id
            };
        }

        public JobApplictionViewModel CreateCandidateAndApplyToJob(
            CreateCandidateAndApplyToJobViewModel model)
        {
            var candiate = CreateCandidate(new CandidateCreateViewModel()
            {
                Name = model.Name
            });
            return ApplyToJob(new JobApplictionCreateViewModel
            {
                CandidateId = candiate.Id,
                JobId = model.JobId
            });
        }
        public JobApplictionViewModel ApplyToJob(JobApplictionCreateViewModel job)
        {
            var jobApplication = new JobApplication
            {
                Act = JobActionEnum.Application,
                CandidateId = job.CandidateId,
                JobId = job.JobId,
            };
            _jobApplicationRepository.Create(jobApplication);
            _candidateRepository.SaveChanges();
            return new JobApplictionViewModel
            {
                Id = jobApplication.Id,
                Act = jobApplication.Act
            };
        }
        public CandidateProfileViewModel UpdateProfile(CandidateProfileViewModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            candidate.Name = profile.Name;
            candidate.LocationId = profile.LocationId;
            candidate.Email = profile.Email;
            _candidateRepository.Update(candidate);
            _candidateRepository.SaveChanges();
            return profile;
        }

        public CandidateAddScheduleModel AddSchedule(CandidateAddScheduleModel model)
        {
            {
                var schedule = new Schedule
                {
                    Start = model.Start,
                    End = model.End

                };
                _scheduleRepository.Create(schedule);
                _scheduleRepository.SaveChanges();
                return new CandidateAddScheduleModel
                {
                    CandidateId = schedule.CandidateId,
                    Start = schedule.Start,
                    End = schedule.End
                };
            }

        }

        public ScheduleCreateViewModel CreateSchedule(ScheduleCreateViewModel model)
        {
            var entity = new Schedule() { CandidateId = model.CandidateId };
            _scheduleRepository.Create(entity);
            _scheduleRepository.SaveChanges();
            return new ScheduleCreateViewModel
            {
                Id = entity.Id,
                CandidateId = entity.CandidateId,
            };
        }


        public bool DeleteProfile(CandidateDeleteProfileModel profile)
        {
            var candidate = _candidateRepository.ById(profile.Id);
            _candidateRepository.Delete(candidate);
            return _candidateRepository.SaveChanges() > 0;
        }

        public CandidateUpdateScheduleModel UpdateSchedule(CandidateUpdateScheduleModel model)
        {
            var entity = _scheduleRepository.ById(model.CandidateId);
            entity.Start = model.Start;
            entity.End = model.End;
            _scheduleRepository.Update(entity);
            _scheduleRepository.SaveChanges();
            return model;
        }


        public CandidateDeleteScheduleModel DeleteSchedule(CandidateDeleteScheduleModel model)
        {
            var entity = _scheduleRepository.ById(model.CandidateId);
            _scheduleRepository.Delete(entity);
            return model;
        }


        public CandidateViewScheduleModel ViewSchedule(CandidateViewScheduleModel model)
        {
            throw new System.NotImplementedException();
            //var entity = _scheduleRepository.ById(model.Id);
            //entity.Candidate = model.Name;
            //if(entity== null)
            //{
                
            //}
            //int employeeCode = Convert.ToInt16(context.Request["id"]);

            //emp = dal.GetEmployee(employeeCode);
            //if (emp == null)
            //    context.Response.Write(employeeCode + "No Employee Found");

            //string serializedEmployee = Serialize(emp);
            //context.Response.ContentType = "text/xml";
            //WriteResponse(serializedEmployee);
        }


    }
}
