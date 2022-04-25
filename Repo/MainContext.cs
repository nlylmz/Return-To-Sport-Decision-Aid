
using Data.Option;
using Data.Athlete;
using Data.Criter;
using Microsoft.EntityFrameworkCore;
using Data.AthCriteria;
using Data.Feedback;
using Data.AthleteOption;

namespace ReturnToSport.Repo
{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Athlete
            new PatientMap(modelBuilder.Entity<Patient>().ToTable("Patients"));
            new OptionsMap(modelBuilder.Entity<Options>().ToTable("Options"));
            new CriteriaMap(modelBuilder.Entity<Criteria>().ToTable("Criteria"));
            new AthleteCriteriaMap(modelBuilder.Entity<AthleteCriteria>().ToTable("AthleteCriteria"));
            new CriteriaValueMap(modelBuilder.Entity<CriteriaValue>().ToTable("CriteriaValue"));
            new OptionsEvaluationMap(modelBuilder.Entity<OptionsEvaluation>().ToTable("OptionsEvaluation"));
            new FeedbackMap(modelBuilder.Entity<Feedbacks>().ToTable("Feedbacks"));
            new OptionResultMap(modelBuilder.Entity<OptionResult>().ToTable("OptionResult"));
            #endregion

        }
    }
}
