using Data.AthCriteria;
using Data.Athlete;
using Data.AthleteOption;
using Data.Criter;
using Data.Feedback;
using Data.Option;
using System.Linq;
using System.Threading.Tasks;


namespace ReturnToSport.Service
{
    public interface IMainServis
    {
        Task DegisiklikleriKaydetAsync();
        void DegisiklikleriKaydet();

        #region Patient
        Task<IQueryable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientAsync(long id);
        long AddPatient(Patient patient);
        #endregion

        #region Options
        Task<IQueryable<Options>> GetAllOptionsAsync();
        Task<Options> GetOptionAsync(long id);
        #endregion

        #region Criteria
        Task<IQueryable<Criteria>> GetAllCriteriaAsync();
        Task<Criteria> GetCriteriaAsync(long id);
        Task<Criteria> GetCriteriaIdAsync(string name);
        long AddCriteria(Criteria criteria);
        #endregion

        #region AthleteCriteria
       //Task<AthleteCriteria> GetAthleteCriteriaAsync(long id);
        long AddAthleteCriteria(AthleteCriteria AthleteCriteria);
        #endregion
        Task<IQueryable<AthleteCriteria>> GetAthleteCriteriaAsync(long athleteId);
        Task<AthleteCriteria> GetOneAthleteCriteriaAsync(long id);
        void UpdateAthleteCriteria(AthleteCriteria athleteCriteria);

        #region CriteriaValue
        long AddCriteriaValue(CriteriaValue criteriaValue);
        #endregion

        #region OptionsEvaluation
        long AddOptionsEvaluation(OptionsEvaluation optionsEvaluation);
        Task<IQueryable<OptionsEvaluation>> GetAllOptionsEvaluationAsync();
        #endregion

        #region Feedbacks
        long AddFeedbacks(Feedbacks feedbacks);
        #endregion

        #region OptionResult
        long AddOptionResult(OptionResult optionResult);
        Task<IQueryable<OptionResult>> GetOptionResultAsync();
        #endregion
    }
}
