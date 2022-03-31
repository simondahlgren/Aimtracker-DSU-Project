using DSUgrupp2.Data.Dto.Shot;

namespace DSUgrupp2.Models
{
    public class SummaryStatisticChart
    {

        public double AverageTimeToFire { get; set; }

        public decimal HitsProne { get; set; }
        public decimal MissProne { get; set; }
        public int TotalShootsProne { get; set; }
        public decimal AccuracyProne { get; set; }
        public double AveragePulseProne { get; set; }

        public ShootingSessionDto ShootingSession { get; set; }

        public decimal HitsStanding { get; set; }
        public decimal MissStanding { get; set; }
        public int TotalShootsStanding { get; set; }
        public decimal AccuracyStanding { get; set; }
        public double AveragePulseStanding { get; set; }

        public SummaryStatisticChart(ShootingSessionDto shooting, double timetofire, decimal hitsProne, decimal missProne, int totalProne, decimal accuracyProne, double pulseProne, decimal hitsStanding, decimal missStanding, int totalStanding, decimal accuracyStanding, double pulseStanding)
        {
        ShootingSession = shooting;
        AverageTimeToFire = timetofire;
        
        HitsProne = hitsProne;
        MissProne = missProne;   
        TotalShootsProne = totalProne;
        AccuracyProne = accuracyProne;
        AveragePulseProne = pulseProne;

        HitsStanding = hitsStanding;
        MissStanding = missStanding;
        TotalShootsStanding = totalStanding;
        AccuracyStanding = accuracyStanding;
        AveragePulseStanding = pulseStanding;
    }

    }
}
