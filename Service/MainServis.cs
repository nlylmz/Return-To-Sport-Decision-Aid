using System.Collections.Generic;
using System.Linq;
using ReturnToSport.Repo;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Athlete;
using Data.Criter;
using Data.Option;
using Data.AthCriteria;
using Data.Feedback;
using Data.AthleteOption;

namespace ReturnToSport.Service
{
    public class MainServis : IMainServis
    {
         private readonly IMainRepository<Patient> _patient;
                          IMainRepository<Options> _options;
                          IMainRepository<Criteria> _criteria;
                          IMainRepository<AthleteCriteria> _athleteCriteria;
                          IMainRepository<CriteriaValue> _criteriaValue;
                          IMainRepository<OptionsEvaluation> _optionsEvaluation;
                          IMainRepository<Feedbacks> _feedbacks;
                          IMainRepository<OptionResult> _optionResult;


        public MainServis(IMainRepository<Patient> patient,
                          IMainRepository<Options> options,
                          IMainRepository<Criteria> criteria,
                          IMainRepository<AthleteCriteria> athleteCriteria,
                          IMainRepository<CriteriaValue> criteriaValue,
                          IMainRepository<OptionsEvaluation> optionsEvaluation,
                          IMainRepository<Feedbacks> feedbacks,
                          IMainRepository<OptionResult> optionResult)
         {            
             this._patient = patient;
            this._options = options;
            this._criteria = criteria;
            this._athleteCriteria = athleteCriteria;
            this._criteriaValue = criteriaValue;
            this._optionsEvaluation = optionsEvaluation;
            this._feedbacks = feedbacks;
            this._optionResult = optionResult;
        }

        #region DegisiklikleriKaydet       
        public async Task DegisiklikleriKaydetAsync()
        {
            await _patient.DegisiklikleriKaydetAsync();
        }
        public void DegisiklikleriKaydet()
        {
            _patient.DegisiklikleriKaydet();
        }
        #endregion

        #region Patient
         public async Task<IQueryable<Patient>> GetAllPatientsAsync()
         {
             return await _patient.TumunuOkunanGetirAsync();
         }

         public async Task<Patient> GetPatientAsync(long id)
         {
             return await _patient.TumunuOkunanGetir().FirstOrDefaultAsync(s => s.Id == id);
         }

         public long AddPatient(Patient patient)
         {
             return _patient.Ekle(patient);
         }
        #endregion

        #region Options
        public async Task<IQueryable<Options>> GetAllOptionsAsync()
        {
            return await _options.TumunuOkunanGetirAsync();
        }

        public async Task<Options> GetOptionAsync(long id)
        {
            return await _options.TumunuOkunanGetir().FirstOrDefaultAsync(s => s.Id == id);
        }
        #endregion

        #region Criteria
        public async Task<IQueryable<Criteria>> GetAllCriteriaAsync()
        {
            return await _criteria.TumunuOkunanGetirAsync();
        }

        public async Task<Criteria> GetCriteriaAsync(long id)
        {
            return await _criteria.TumunuOkunanGetir().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Criteria> GetCriteriaIdAsync(string name)
        {
            return await _criteria.TumunuOkunanGetir().FirstOrDefaultAsync(s => s.Name == name);
        }

        public long AddCriteria(Criteria criteria)
        {
            return _criteria.Ekle(criteria);
        }
        #endregion

        #region AthleteCriteria
        public async Task<IQueryable<AthleteCriteria>> GetAthleteCriteriaAsync(long athleteId)
        {
            return await Task.Run(() => _athleteCriteria.TumunuOkunanGetir().Where(k => k.AthleteId == athleteId));
        }
        public long AddAthleteCriteria(AthleteCriteria athleteCriteria)
        {
            return _athleteCriteria.Ekle(athleteCriteria);
        }
        public async Task<AthleteCriteria> GetOneAthleteCriteriaAsync(long id)
        {
            return await _athleteCriteria.TumunuOkunanGetir().FirstOrDefaultAsync(s => s.Id == id);
        }

        public void UpdateAthleteCriteria(AthleteCriteria athleteCriteria)
        {
           _athleteCriteria.Guncelle(athleteCriteria);
        }
        #endregion

        #region CriteriaValue
        public long AddCriteriaValue(CriteriaValue criteriaValue)
        {
            return _criteriaValue.Ekle(criteriaValue);
        }
        #endregion

        #region OptionsEvaluation
        public long AddOptionsEvaluation(OptionsEvaluation optionsEvaluation)
        {
            return _optionsEvaluation.Ekle(optionsEvaluation);
        }
        public async Task<IQueryable<OptionsEvaluation>> GetAllOptionsEvaluationAsync()
        {
            return await _optionsEvaluation.TumunuOkunanGetirAsync();
        }
        #endregion

        #region Feedbacks
        public long AddFeedbacks(Feedbacks feedbacks)
        {
            return _feedbacks.Ekle(feedbacks);
        }
        #endregion

        #region OptionResult
        public long AddOptionResult(OptionResult optionResult)
        {
            return _optionResult.Ekle(optionResult);
        }

        public async Task<IQueryable<OptionResult>> GetOptionResultAsync()
        {
            return await _optionResult.TumunuOkunanGetirAsync();
        }
        #endregion
    }
}
