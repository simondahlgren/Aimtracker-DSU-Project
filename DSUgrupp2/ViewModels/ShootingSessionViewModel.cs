using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Web;

namespace DSUgrupp2.ViewModels
{
    public class ShootingSessionViewModel
    {
        #region Properties
        public decimal Hits { get; set; }
        public string TypeOfSerie { get; set; }
        public decimal HitsStanding { get; set; }
        
        public decimal Miss { get; set; }
        public decimal Accuracy { get; set; }
        public decimal AccuracyProne { get; set; }
        public int TotalShoots { get; set; }
        public int TotalShootsProne { get; set; }

        public int TotalProneSessions { get; set; }
        public int TotalStandingSessions { get; set; }
        public int TotalPulseProne { get; set; }
        public int TotalPulse { get; set; }

        public double AveragePulseProne { get; set; }
        public double AveragePulse { get; set; }

        public double TimeToFire { get; set; }
        public double AverageTimeToFire { get; set; }
        #endregion
     
        public ShootingSessionDto? ShootingSession { get; set; }

        

        public ShootingSessionViewModel(ShootingSessionDto session)
        {
            ShootingSession = session;

        }


        #region Methods to show average statstic on a shooting series (5shots)
        public string TimeToFireAverage(ResultseriesDto resultseries) // Calculate average time to fire on a shooting series (5 shots)
        {
            double total = 0;
            double numberOfShots = resultseries.Shots.Count();
            foreach (var item in resultseries.Shots)
            {
                total += item.TimeToFire;
            }
            double average = total / numberOfShots; 
            average = Math.Round(average, 1);
            return average.ToString();  
        }
        public string AverageHeartRate(ResultseriesDto resultseries) // Calculate average Heartrate on a shooting series (5 shots)
        {
            double totalPuls = 0;
            double numberOfShots = resultseries.Shots.Count();
            foreach (var item in resultseries.Shots)
            {
                totalPuls += item.HeartRate;
            }
            double averagePuls = totalPuls / numberOfShots;
            return averagePuls.ToString();
        }
        public string NumberOfHits(ResultseriesDto resultseries) // Calculate average number of hits on a shooting series (5 shots)
        {
            int totalHits = 0;
            foreach (var item in resultseries.Shots)
            {
                if (item.Result == "hit")
                {
                    totalHits++;
                }
            }
            return totalHits.ToString();
        }
        public string NumberOfMiss(ResultseriesDto resultseries) // Calculate average number of miss on a shooting series (5 shots)
        {
            int totalMiss = 0;
            foreach (var item in resultseries.Shots)
            {
                if (item.Result == "miss")
                {
                    totalMiss++;
                }
            }
            return totalMiss.ToString();
        }
        public string GetShotNumber(ResultseriesDto resultseries, shotsDto shots) // returns the shot number so we can display what number the shot have in html
        {
            int number = resultseries.Shots.IndexOf(shots);
            number += 1;
            return number.ToString();
        }
        public string GetStanceNumber(ShootingSessionDto shootingSession, ResultseriesDto resultseries)
        {
            int number = shootingSession.Results.IndexOf(resultseries);
            number += 1;
            return number.ToString();
        }
    }
    #endregion
}
