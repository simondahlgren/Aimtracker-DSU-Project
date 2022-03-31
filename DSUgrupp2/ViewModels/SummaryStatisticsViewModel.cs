using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DSUgrupp2.ViewModels
{
    public class SummaryStatisticsViewModel
    {
        private readonly AppDbContext _appDbContext;
        public AthleteDto Athlete { get; set; }

        #region All calculating method properties
        
        public decimal HitsProne { get; set; }
        public int TotalHitsProne   { get; set; }
        public decimal MissProne { get; set; }
        public decimal HitsStanding { get; set; }
        public int TotalHitsStanding { get; set; }
        public decimal MissStanding { get; set; }
        public decimal AccuracyStanding { get; set; }
        public decimal AccuracyProne { get; set; }
        public int TotalShootsStanding { get; set; }
        public int TotalShootsProne { get; set; }

        public string AccuracyStandingFormat { get; set; }
        public string AccuracyProneFormat { get; set; }
        public int TotalProneSessions { get; set; }
        public int TotalStandingSessions { get; set; }
        public int TotalPulseProne {get; set; }
        public int TotalPulseStanding { get; set; }

        public double AveragePulseProne { get; set; }
        public double AveragePulseStanding { get; set; }

        public double TimeToFire { get; set; }
        public double AverageTimeToFire { get; set; }

        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int WindForce { get; set; }
        public int Temperature { get; set; }
        public string NoResult { get; set; }
        

        public string SessionDate { get; set; } 

        #endregion // All calculating properties
        #region All different Lists for method and View
        public List<SummaryStatisticChart> summaryStatisticCharts { get; set; } = new List<SummaryStatisticChart>();
        public List<SummaryStatisticGraph> summaryStatisticGraphs { get; set; } = new List<SummaryStatisticGraph>();
        public List<AthleteDto> Athletes { get; set;} = new List<AthleteDto>();
        public List<ShootingSessionDto> shootingSessions{ get; set; } = new List<ShootingSessionDto>();
        #endregion  // Al
        public SummaryStatisticsViewModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public SummaryStatisticsViewModel(IEnumerable<AthleteDto> athleteDto)
        {
            foreach(var athlete in athleteDto)
            {
                Athletes.Add(athlete);
            }
            
        }
        public SummaryStatisticsViewModel()
        {

        }
        

        public SummaryStatisticsViewModel(IEnumerable<ShootingSessionDto> shootingSessionDtos, IEnumerable<AthleteDto> athleteDto, AthleteDto currentAthlete)
        {
            Athlete = currentAthlete;
            foreach (var athlete in athleteDto)
            {
                Athletes.Add(athlete);
            }
            foreach (var session in shootingSessionDtos)
            {
                //AvrageShot(session);
                shootingSessions.Add(session);
            }
            shootingSessions = shootingSessions.OrderBy(shootingSessions => shootingSessions.Date).ToList();



            if (shootingSessions.Count != 0) // FailSafe to check if viewmodel contains any trainingsessions
            {
                CalculateEverySession();
                CalculateAverageFromAllSeason();
                AccuracyAthlete(); 
                CalculateAccuracy();
                CalculateAveragePulse();

            }
            else

                NoResult = "No results found";
      

        }

       public void AccuracyAthlete()
       {
           for (int i = 0; i < shootingSessions.Count; i++)
           {
               foreach (var stance in shootingSessions[i].Results)
               {
                   
                   if (stance.stance == "Prone")
                   {
                       for (int y = 0; y < stance.Shots.Count; y++)
                       {
                           if (stance.Shots[y].Result == "hit")
                           {
                               HitsProne++;
                               
                               TotalShootsProne++;
                               TotalPulseProne += stance.Shots[y].HeartRate;
                               TotalProneSessions++;
                           }
                           else
                           {
                               MissProne++;
                               TotalShootsProne++;
                               
                               TotalPulseProne += stance.Shots[y].HeartRate;
                               TotalProneSessions++;
                           }
                       }
                   }
                   else
                   {
                       for (int u = 0; u < stance?.Shots?.Count; u++)
                       {
                           if (stance?.Shots[u].Result == "hit")
                           {
                               HitsStanding++;
                               
                               TotalShootsStanding++;
                               TotalPulseStanding += stance.Shots[u].HeartRate;
                               TotalStandingSessions++;
                           }
                           else
                           {
                               MissStanding++;
                               TotalShootsStanding++;
                               
                               TotalPulseStanding += stance.Shots[u].HeartRate;
                               TotalStandingSessions++;
                           }
                       }
                   }
               }
           }
       
        
       
       }
        public void CalculateAccuracy()
        {
            AccuracyStanding = HitsStanding / TotalShootsStanding;
            AccuracyStanding = Math.Round(AccuracyStanding, 3);
            if (AccuracyStanding != 0)
            {
                AccuracyStanding *= 100;
            }
            else
            {
                AccuracyStanding = 100;
            }
            AccuracyStanding = Math.Round(AccuracyStanding, 1);
            

            AccuracyProne = HitsProne / TotalShootsProne;
            AccuracyProne = Math.Round(AccuracyProne, 3);
            if (AccuracyProne != 0)
            {
                AccuracyProne *= 100;
            }
            else
            {
                AccuracyProne = 100;
            }
            AccuracyProne = Math.Round(AccuracyProne, 1);
            
        }//method for calculating accuracy for a session
        public void CalculateAverageTimeToFire()
        {
            AverageTimeToFire = TimeToFire / (TotalShootsProne + TotalShootsStanding);
            AverageTimeToFire = Math.Round(AverageTimeToFire, 1);
        }// method for calculating average time to fire for a session
        public void CalculateAveragePulse()
        {
            AveragePulseStanding = TotalPulseStanding / TotalStandingSessions;

            AveragePulseProne = TotalPulseProne / TotalProneSessions;


        }// method for calculating average pulse for a session



        public void CalculateAverageFromAllSeason() //method for calculating every session we get from controller and add to a different list for displaying selected information to view
        {
            for (int i = 0; i < shootingSessions.Count; i++)
            {
                

                foreach (var session in shootingSessions[i].Results)
                {
                    if (session.stance == "Prone")
                    {
                        foreach (var shoot in session.Shots)
                        {
                            if (shoot.Result == "hit")
                            {
                                HitsProne++;
                                TotalShootsProne++;
                                TotalProneSessions++;
                                TotalPulseProne += shoot.HeartRate;
                                TimeToFire += shoot.TimeToFire;
                            }
                            else
                            {
                                MissProne++;
                                TotalShootsProne++;
                                TotalProneSessions++;
                                TotalPulseProne += shoot.HeartRate;
                                TimeToFire += shoot.TimeToFire;
                            }
                        }
                            
                    }

                    else
                    {
                        foreach (var shoot in session.Shots)
                        {
                            if (shoot.Result == "hit")
                            {
                                HitsStanding++;
                                TotalShootsStanding++;
                                TotalPulseStanding += shoot.HeartRate;
                                TimeToFire += shoot.TimeToFire;
                                TotalStandingSessions++;
                            }
                            else
                            {
                                MissStanding++;
                                TotalShootsStanding++;
                                TotalPulseStanding += shoot.HeartRate;
                                TimeToFire += shoot.TimeToFire;
                                TotalStandingSessions++;
                            }
                        }    
                    }  
                }
                CalculateAccuracy();
                CalculateAveragePulse();
                CalculateAverageTimeToFire();
                 var test = new SummaryStatisticChart(shootingSessions[i], AverageTimeToFire, HitsProne, MissProne, TotalShootsProne, AccuracyProne, AveragePulseProne, HitsStanding, MissStanding, TotalShootsStanding, AccuracyStanding, AveragePulseStanding);
                 summaryStatisticCharts.Add(test);

                ResetProperties();
            }
        }
        public void CalculateEverySession() //method for calculating every session we get from controller and add to a different list specific for our chartJS graph.
        {
            for (int i = 0; i < shootingSessions.Count; i++)
            {
                
                foreach (var stance in shootingSessions[i].Results)
                {
                    
                    if (stance.stance == "Prone")
                    {
                        for (int y = 0; y < stance.Shots.Count; y++)
                        {
                            if (stance.Shots[y].Result == "hit")
                            {
                                
                                HitsProne++;
                                TotalShootsProne++;
                                TotalPulseProne += stance.Shots[y].HeartRate;
                                TotalProneSessions++;
                            }
                            else
                            {
                                MissProne++;
                                TotalShootsProne++;
                                TotalPulseProne += stance.Shots[y].HeartRate;
                                TotalProneSessions++;
                            }
                        }
                    }
                    else
                    {
                        for (int u = 0; u < stance.Shots.Count; u++)
                        {
                            if (stance.Shots[u].Result == "hit")
                            {
                                HitsStanding++;
                                TotalShootsStanding++;
                                TotalPulseStanding += stance.Shots[u].HeartRate;
                                TotalStandingSessions++;
                            }
                            else
                            {
                                MissStanding++;
                                TotalShootsStanding++;
                                TotalPulseStanding += stance.Shots[u].HeartRate;
                                TotalStandingSessions++;
                            }
                        }
                    }
                }
                CalculateAccuracy();
                CalculateAveragePulse();
                AccuracyStandingFormat = AccuracyStanding.ToString(CultureInfo.InvariantCulture);
                AccuracyProneFormat = AccuracyProne.ToString(CultureInfo.InvariantCulture);
                var stat = new SummaryStatisticGraph(AveragePulseProne, AveragePulseStanding, AccuracyStandingFormat, AccuracyProneFormat);
                summaryStatisticGraphs.Add(stat);

                ResetProperties();
            }
            

        }

        public void ResetProperties() // method for reseting different properties
        {
            HitsProne = 0;
            MissProne = 0;
            HitsStanding = 0;
            MissStanding = 0;
            AccuracyStanding = 0;
            AccuracyProne = 0;

            TotalProneSessions = 0;
            TotalStandingSessions = 0;
            TotalShootsStanding = 0;
            TotalShootsProne = 0;

            TotalPulseProne = 0;
            TotalPulseStanding = 0;
            
            AveragePulseProne = 0;
            AveragePulseStanding = 0;
            TimeToFire = 0;

        }
        
    }
}
