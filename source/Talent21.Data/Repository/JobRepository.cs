using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Core;
using Talent21.Data.ViewModels;

namespace Talent21.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class JobRepository : EfDictionaryRepository<Job>, IJobRepository

    {
        public JobRepository(DbContext context, IEventManager eventManager) : base( context, eventManager)
        { 
            
        }

        public override IQueryable<Job> All
        {
            get { return base.All.Include(x => x.Skills).Include(x => x.Locations).Include(x => x.Company); }
        }

        public IQueryable<Job> Mine(string userId)
        {
            return base.All.Where(x => x.Company.OwnerId == userId).Include(x => x.Skills.Select(y => y.Skill)).Include(x => x.Locations).Include(x => x.Company);
        }

        public Job MineFirst(string userId,int id)
        {
            return base.All.Include(x => x.Skills.Select(y => y.Skill)).Include(x => x.Locations).Include(x => x.Company).FirstOrDefault(x=> x.Company.OwnerId == userId && x.Id==id);
        }


        internal static void Register(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasKey(x => x.Id);
            modelBuilder.Entity<Job>().HasMany(x => x.Locations).WithMany(x => x.Jobs).Map(x =>
            {
                x.MapLeftKey("JobId");
                x.MapRightKey("LocationId");
                x.ToTable("JobsLocationMapping");
            });
        }


        public IQueryable<Job> MatchingForConctractor(string userId)
        {
            return All;//TODO:search function fix.
        }

        public IQueryable<JobDuration> Durations(string location)
        {
            return All.SelectMany(x => x.Skills.Where(y => y.Level == LevelEnum.Primary && x.Locations.Any(z=>z.Title==location)).Select(y =>
                new JobDuration
                {
                    Skill = y.Skill.Title,
                    Location =location,
                    Rate=x.Rate,
                    Duration = DbFunctions.DiffDays(x.Duration.Start, x.Duration.End) ?? 0
                }));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IJobRepository : IDictionaryRepository<Job>
    {
        IQueryable<Job> Mine(string userId);
        Job MineFirst(string userId,int id);
        IQueryable<Job> MatchingForConctractor(string userId);
        IQueryable<JobDuration> Durations(string loc);
    }

}
