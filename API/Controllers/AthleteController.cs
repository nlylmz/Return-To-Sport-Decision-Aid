using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ReturnToSport.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using Data.Athlete;
using Data.AthCriteria;
using Data.Criter;
using Data.Option;
using Data.Feedback;
using Data.AthleteOption;

namespace API.Controllers
{
    [Route("api/rtp")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IMainServis _mainServis;

        public AthleteController(IMainServis mainServis)
        {
            _mainServis = mainServis;
        }
        // POST: api/rtp/Athlete
        [HttpPost("Athlete")]
        public async Task<IActionResult> PostAthlete(PatientDTO patientDTO)
        {
            Patient newPatient = new Patient
            {
                FirstName = patientDTO.FirstName,
                LastName = patientDTO.LastName,
                Detail = patientDTO.Detail,
                //Time = DateTime.Now,
            };
            _mainServis.AddPatient(newPatient);

            try
            {
                await _mainServis.DegisiklikleriKaydetAsync();
                patientDTO.Id = newPatient.Id;
                return Ok(new { success = true, newPatient = patientDTO });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error!" });
            }
        }

        //Get: api/rtp/Athletes
        [HttpGet("Athletes")]
        public async Task<IActionResult> GetAthletes()
        {
            //Daha önce doktor değerlendirmesi yapılan Idleri listeden çıkart.

            List<Patient> allAthletes = await _mainServis.GetAllPatientsAsync().Result.ToListAsync();
            //var athleteIdleri = _mainServis.GetAllOptionsEvaluationAsync().Result.Select(s => new { s.AthleteId }).Distinct();
            //var result = allAthletes.Where(p => !athleteIdleri.Any(p2 => p2.AthleteId == p.Id));

            List<AthletesDTO> athletes = allAthletes
                .Select(s => new AthletesDTO
                {
                    AthleteInfo = s.FirstName + " " + s.LastName + "(" + s.Detail + ")",
                    Id = s.Id
                }).ToList();
            try
            {
                return Ok(new { success = true, athletes = athletes });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error!" });
            }
        }

        // POST: api/rtp/Athlete
        [HttpPost("Feedbacks")]
        public async Task<IActionResult> PostFeedbacks(FeedbacksDTO feedbacksDTO)
        {
            Feedbacks feedbacks = new Feedbacks
            {
                Evaluation = feedbacksDTO.Evaluation,
                DoesAthlete = feedbacksDTO.DoesAthlete,
            };
            _mainServis.AddFeedbacks(feedbacks);

            try
            {
                await _mainServis.DegisiklikleriKaydetAsync();
                feedbacksDTO.Id = feedbacks.Id;
                return Ok(new { success = true, feedbacks = feedbacksDTO });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error!" });
            }
        }

        //Get: api/rtp/AthletesCriteria
        [HttpGet("AthletesCriteria/{athleteId:long}")]
        public async Task<IActionResult> GetAthletesCriteria(long athleteId)
        {
            List<CriteriaDTO> criteria = await _mainServis.GetAthleteCriteriaAsync(athleteId).Result
                 .Select(s => new CriteriaDTO
                 {
                     Id = s.CriteriaId,
                     Name = s.Criteria.Name,

                 }).ToListAsync();

            var combinationOptionsList = new List<OptionsCombinationDTO>();
            List<Options> options = await _mainServis.GetAllOptionsAsync().Result.ToListAsync();
            int count = options.Count;
            int size = criteria.Count();


            for (int z = 0; z < size; z++)
            {
                int y = 1;
                for (int i = 0; i < count; i++)
                {
                    for (int x = y; x < count; x++)
                    {
                        combinationOptionsList.Add(new OptionsCombinationDTO
                        {
                            Option1 = options[i].Name,
                            Option2 = options[x].Name,
                            Option1Id = options[i].Id,
                            Option2Id = options[x].Id,
                            Criteria = criteria[z].Name,
                            CriteriaId = criteria[z].Id,
                        });

                    }

                    y = y + 1;
                }
            }

            try
            {
                return Ok(new { success = true, athleteId, combinationOptionsList });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error!" });
            }
        }

        // POST: api/rtp/AthleteCriteria
        [HttpPost("AthleteCriteria")]
        public async Task<IActionResult> PostAthleteCriteria(AthleteCriteriaDTO athleteCriteriaDTO)
        {
            List<CriteriaDTO> criteriaDTOlar = athleteCriteriaDTO.Criteria.ToList();
            List<Criteria> criteria = await _mainServis.GetAllCriteriaAsync().Result.ToListAsync();

            var result = criteriaDTOlar.Where(p => !criteria.Any(p2 => p2.Name == p.Name));

            int y = 1;
            int count = criteriaDTOlar.Count;
            var combinationCriteriaList = new List<CriteriaCombinationDTO>();

            //result ilk beş Id ile aynı olmasın! 
            //Eğer kullanıcı yeni kriter eklediyse bunu Kriter db ekle.

            if (result != null)
            {

                foreach (CriteriaDTO criterias in result)
                {
                    Criteria newCriteria = new Criteria
                    {
                        Name = criterias.Name
                    };
                    _mainServis.AddCriteria(newCriteria);
                    await _mainServis.DegisiklikleriKaydetAsync();
                }
            }


            foreach (CriteriaDTO athleteCriteria in criteriaDTOlar)
            {
                var CriteriaId = await _mainServis.GetCriteriaIdAsync(athleteCriteria.Name);

                AthleteCriteria newAthleteCriteria = new AthleteCriteria
                {
                    CriteriaId = CriteriaId.Id,
                    AthleteId = athleteCriteriaDTO.AthleteId,
                };

                _mainServis.AddAthleteCriteria(newAthleteCriteria);
                await _mainServis.DegisiklikleriKaydetAsync();
            }

            var athleteId = athleteCriteriaDTO.AthleteId;

            for (int i = 0; i < count; i++)
            {
                for (int x = y; x < count; x++)
                {
                    var Criteria1 = await _mainServis.GetCriteriaIdAsync(criteriaDTOlar[i].Name);
                    var Criteria2 = await _mainServis.GetCriteriaIdAsync(criteriaDTOlar[x].Name);
                    combinationCriteriaList.Add(new CriteriaCombinationDTO
                    {
                        Criteria1 = criteriaDTOlar[i].Name,
                        Criteria2 = criteriaDTOlar[x].Name,
                        Criteria1Id = Criteria1.Id,
                        Criteria2Id = Criteria2.Id,
                    });

                }

                y = y + 1;
            }

            try
            {
                return Ok(new { success = true, athleteId, combinationCriteriaList });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error!" });
            }
        }

        // POST: api/rtp/AthleteCriteriaValue
        [HttpPost("AthleteCriteriaValue")]
        public async Task<IActionResult> PostAthleteCriteriaValue(AthleteCriteriaValuesDTO athleteCriteriaValuesDTO)
        {

            List<CriteriaValueDTO> criteriaValueDTOlar = athleteCriteriaValuesDTO.CriteriaValues.ToList();

            double[] cweight = await CriteriaWeightCalculationAsync(criteriaValueDTOlar, athleteCriteriaValuesDTO.AthleteId);

            if (cweight == null)
            {
                return Ok(new { success = false, message = "There is inconsistency among criteria values. Please evaluate criteria again." });
            }

            else
            {
                var criteria = await _mainServis.GetAllCriteriaAsync().Result.ToListAsync();
                var athCrt = await _mainServis.GetAthleteCriteriaAsync(athleteCriteriaValuesDTO.AthleteId).Result.ToListAsync();

                List<CriteriaWeightDTO> crtWeight = athCrt
                     .Select(s => new CriteriaWeightDTO
                     {
                         CriteriaName = criteria.Where(c => c.Id == s.CriteriaId).Select(c => c.Name).FirstOrDefault(),
                         Weight = s.Weight,

                     }).ToList();

                var orderedCrtWeight = crtWeight.OrderByDescending(x => x.Weight).ToList();


                foreach (CriteriaValueDTO athleteCriteriaValues in criteriaValueDTOlar)
                {
                    CriteriaValue newCriteriaValue = new CriteriaValue
                    {
                        AthleteId = athleteCriteriaValuesDTO.AthleteId,
                        Criteria1Id = athleteCriteriaValues.Criteria1Id,
                        Criteria2Id = athleteCriteriaValues.Criteria2Id,
                        Value = athleteCriteriaValues.Value,
                    };

                    _mainServis.AddCriteriaValue(newCriteriaValue);
                    await _mainServis.DegisiklikleriKaydetAsync();
                    athleteCriteriaValuesDTO.Id = newCriteriaValue.Id;
                }

                try
                {
                    return Ok(new { success = true, message = "The process is successful.", orderedCrtWeight });
                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = "Error!" });
                }
            }
        }


        // POST: api/rtp/OptionsEvaluation
        [HttpPost("OptionsEvaluation")]
        public async Task<IActionResult> PostOptionsEvaluation(DoctorOptionsEvaluationDTO doctorOptionsEvaluationDTO)
        {
            double[] optResult = new double[3];
            List<OptionValuesDTO> optionsEvaluationDTOlar = doctorOptionsEvaluationDTO.OptionValues.ToList();

            double[,] optionsWeight = await OptionsWeightCalculationAsync(optionsEvaluationDTOlar, doctorOptionsEvaluationDTO.AthleteId);

            if (optionsWeight == null || optionsWeight.Length == 0)
                return Ok(new { success = false, message = "There is inconsistency among option values. Please evaluate options again." });

            else
            {
                foreach (OptionValuesDTO optionsEvaluationValues in optionsEvaluationDTOlar)
                {
                    OptionsEvaluation newOptionsEvaluation = new OptionsEvaluation
                    {
                        AthleteId = doctorOptionsEvaluationDTO.AthleteId,
                        CriteriaId = optionsEvaluationValues.CriteriaId,
                        Option1Id = optionsEvaluationValues.Option1Id,
                        Option2Id = optionsEvaluationValues.Option2Id,
                        Value = optionsEvaluationValues.Value,
                    };

                    _mainServis.AddOptionsEvaluation(newOptionsEvaluation);
                    await _mainServis.DegisiklikleriKaydetAsync();
                    doctorOptionsEvaluationDTO.Id = newOptionsEvaluation.Id;
                }

                var criteria = await _mainServis.GetAllCriteriaAsync().Result.ToListAsync();
                var athCrt = await _mainServis.GetAthleteCriteriaAsync(doctorOptionsEvaluationDTO.AthleteId).Result.ToListAsync();

                var athelete = await _mainServis.GetPatientAsync(doctorOptionsEvaluationDTO.AthleteId);

                List<CriteriaWeightDTO> crtWeight = athCrt
                     .Select(s => new CriteriaWeightDTO
                     {
                         CriteriaName = criteria.Where(c => c.Id == s.CriteriaId).Select(c => c.Name).FirstOrDefault(),
                         Weight = s.Weight,

                     }).ToList();

                var orderedCrtWeight = crtWeight.OrderByDescending(x => x.Weight).ToList();

                optResult = OptionResult(optionsWeight, crtWeight); //sonucu hesapla
                int crtCount = crtWeight.Count();

                List<double> optcrtWeights = optionsWeight.Cast<double>().ToList();

                  for (int i = 0; i < optResult.Length; i++)
                  {
                      OptionResult newOptionResult = new OptionResult
                      {
                          AthleteId = doctorOptionsEvaluationDTO.AthleteId,
                          OptionId = i+1,
                          Weight = optResult[i],
                      };

                      _mainServis.AddOptionResult(newOptionResult);
                      await _mainServis.DegisiklikleriKaydetAsync();
                  }

                /*List<List<double>> optcrtWeights = new List<List<double>>();
                
                for (int i = 0; i < crtCount; i++)
                {
                    var val = new List<double>();
                    
                    for (int x = 0; x < 3; x++)
                    {
                        val.Add(optionsWeight[x, i]);
                    }

                    optcrtWeights.Add(val);
                }*/

                try
                {
                    return Ok(new { success = true, optcrtWeights, orderedCrtWeight, optResult, crtWeight, crtCount, athelete });
                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, message = "Error!" });
                }
            }
        }

        //Get: api/rtp/DashboardResult
        [HttpGet("DashboardResult/{athleteId:long}")]
        public async Task<IActionResult> GetDashboardResult(long athleteId)
        {
            //double[] optResult = new double[3];
            var criteria = await _mainServis.GetAllCriteriaAsync().Result.ToListAsync();
            var athCrt = await _mainServis.GetAthleteCriteriaAsync(athleteId).Result.ToListAsync();

            var athelete = await _mainServis.GetPatientAsync(athleteId);

            List<CriteriaWeightDTO> crtWeight = athCrt
                 .Select(s => new CriteriaWeightDTO
                 {
                     CriteriaName = criteria.Where(c => c.Id == s.CriteriaId).Select(c => c.Name).FirstOrDefault(),
                     Weight = s.Weight,

                 }).ToList();

            var orderedCrtWeight = crtWeight.OrderByDescending(x => x.Weight).ToList();

            var optResult = _mainServis.GetOptionResultAsync().Result.ToList().Where(x => x.AthleteId == athleteId).Select(o => o.Weight);

            try
            {
                return Ok(new { success = true, orderedCrtWeight, optResult, athelete });
            }
            catch (Exception ex)
            {
                return Ok(new { success = false, message = "Error!" });
            }
        }


        private async Task<double[,]> OptionsWeightCalculationAsync(List<OptionValuesDTO> optionValue, long athleteId)
        {
            List<long> creteriaId = new List<long>();
            List<AthleteCriteria> athleteCriterias = await _mainServis.GetAthleteCriteriaAsync(athleteId).Result.ToListAsync();

            foreach (long criteriasId in athleteCriterias.Select(a => a.CriteriaId))
            {
                creteriaId.Add(criteriasId);
            }

            //3 tane alternative olduğu için GetRange'in count 3
            int optCount = 3;
            int size = creteriaId.Count();
            double[,] optionWeight = new double[optCount, size];
            int y = 0;

            for (int i=0; i<optionValue.Count();i=i+3)
            {
                double[,] optionVal = MatrixValues(optCount, optionValue.GetRange(i, optCount));
                double[] crtOption = WeightConsistency(optCount, optionVal);

                if (crtOption == null || crtOption.Length == 0)
                { 
                    return new double[,] { }; ;
                }

                else
                {
                    for (int x = 0; x< optCount; x++)
                    {
                        optionWeight[x,y] = crtOption[x];
                    }
                    y++;
                }
            }       

            return optionWeight;
        }

        public static double[,] MatrixValues(int size, List<OptionValuesDTO> optionValue)
        {
            int index = 0;
            double[,] crtOption = new double[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (x == y)
                    {
                        crtOption[x, x] = 1;
                    }
                    else
                    {
                        if (y > x)
                        {
                            if (optionValue[index].Value < 0)
                                crtOption[x, y] = -optionValue[index].Value;
                            else if (optionValue[index].Value == 0)
                                crtOption[x, y] = 1;
                            else
                                crtOption[x, y] = (double)1 / optionValue[index].Value;

                            index++;
                        }
                        else
                        {
                            crtOption[x, y] = 1 / crtOption[y, x];
                        }

                    }
                }
            }

            return crtOption;

        }

        //Sonucu buluyor
        public static double [] OptionResult (double[,] options, List<CriteriaWeightDTO> weights)
        {
            int size = 3; //3 tane alternatif var
            double[] result = new double[size];

            double[] weight = weights.Select(I => Convert.ToDouble(I.Weight)).ToArray();

            for (int i = 0; i < size; i++)
            {
                result[i] = 0;
                for (int j = 0; j < weights.Count; j++)
                {
                    result[i] = result[i] + (options[i, j] * weight[j]);
                }
            }

            return result;
        }


        private async Task<double[]> CriteriaWeightCalculationAsync(List<CriteriaValueDTO> criteriaValue, long athleteId)
        {
            List<long> creteriaId = new List<long>();
            List<AthleteCriteria> athleteCriterias = await _mainServis.GetAthleteCriteriaAsync(athleteId).Result.ToListAsync();

            foreach (long criteriasId in athleteCriterias.Select(a => a.CriteriaId))
            {
                creteriaId.Add(criteriasId);
            }

            int size = creteriaId.Count();
            double[,] criteria = new double[size, size];
            int index = 0;
            double[] cweight = new double[size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (x == y)
                    {
                        criteria[x, x] = 1;
                    }
                    else
                    {
                        if (y > x)
                        {
                            if (criteriaValue[index].Value < 0)
                                criteria[x, y] = -criteriaValue[index].Value;
                            else if (criteriaValue[index].Value == 0)
                                criteria[x, y] = 1;
                            else
                                criteria[x, y] = (double)1 / criteriaValue[index].Value;

                            index++;
                        }
                        else
                        {
                            criteria[x, y] = 1 / criteria[y, x];
                        }

                    }
                }
            }

            cweight = WeightConsistency(size, criteria);

            //Eğer inconsistency yoksa cweight dolu gelir. 
            if (cweight != null)
            {
                int m = 0;

                foreach (long acId in athleteCriterias.Select(c => c.Id))
                {
                    AthleteCriteria updatedAthleteCriteria = await _mainServis.GetOneAthleteCriteriaAsync(acId);
                    updatedAthleteCriteria.Weight = cweight[m];
                    _mainServis.UpdateAthleteCriteria(updatedAthleteCriteria);
                    m++;
                }
                await _mainServis.DegisiklikleriKaydetAsync();
            }
            return cweight;
        }

        public static double[] WeightConsistency(int size, double[,] values)
        {
            double[,] normalized = new double[size, size];
            double[,] consistency = new double[size, size];

            double[] csum = new double[size];
            double[] rsum = new double[size];
            double[] weight = new double[size];
            double[] weightedSum = new double[size];
            double lambda = 0;
            double consistencyIndex;
            double consistencyRatio;
            double rI;

            /* Sum of Column */
            for (int i = 0; i < size; i++)
            {
                csum[i] = 0;
                for (int j = 0; j < size; j++)
                    csum[i] = csum[i] + values[j, i];
            }

            /* Divide of Column Normalization */
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    normalized[j, i] = values[j, i] / csum[i];
            }

            /* Sum of rows and weight calculation */
            for (int i = 0; i < size; i++)
            {
                rsum[i] = 0;
                for (int j = 0; j < size; j++)
                {
                    rsum[i] = rsum[i] + normalized[i, j];
                }
                weight[i] = rsum[i] / size;
            }

            /* Consistency check */
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    consistency[j, i] = weight[i] * values[j, i];
            }

            /*Weighted Sum Value */
            for (int i = 0; i < size; i++)
            {
                weightedSum[i] = 0;
                for (int j = 0; j < size; j++)
                    weightedSum[i] = weightedSum[i] + consistency[i, j];
            }

            /*Lambda Max */
            for (int i = 0; i < size; i++)
            {
                lambda = lambda + (weightedSum[i] / weight[i]);
            }

            lambda = lambda / size;

            /* Consistency Index CI */
            consistencyIndex = (lambda - size) / (size - 1);

            /*Random Consistency Index */
            rI = RandomCIndex(size);

            /* Consistency Ratio */
            consistencyRatio = consistencyIndex / rI;

            if (consistencyRatio < 0.2)
            {
                return weight;
            }
            else
            {
                return null;
               //return new double[] { };
            }    
        }

        public static double RandomCIndex(int n)
        {
            if (n == 1)
            {
                return 0;
            }
            else if (n == 2)
            {
                return 0;
            }
            else if (n == 3)
            {
                return 0.58;
            }
            else if (n == 4)
            {
                return 0.9;
            }
            else if (n == 5)
            {
                return 1.12;
            }
            else if (n == 6)
            {
                return 1.24;
            }
            else if (n == 7)
            {
                return 1.32;
            }
            else if (n == 8)
            {
                return 1.41;
            }
            else if (n == 9)
            {
                return 1.45;
            }
            else if (n == 10)
            {
                return 1.49;
            }
            else
            {
                return -1;
            }
        }
 
    }
}

