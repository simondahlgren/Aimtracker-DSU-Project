namespace DSUgrupp2.Models
{
    public class SummaryStatisticGraph
    {

        public string AccuracyStanding { get; set; }
        public double PulseStanding { get; set; }
        public string AccuracyProne { get; set; }
        public double PulseProne { get; set; }

        public SummaryStatisticGraph(double pulseStanding, double pulseProne, string accuracyStanding, string accuracyProne)
        {
            PulseProne = pulseProne;
            PulseStanding = pulseStanding;
            AccuracyStanding = accuracyStanding;
            AccuracyProne = accuracyProne;
            
        }
    }
}
