using System.ComponentModel.DataAnnotations.Schema;

namespace DSUgrupp2.Data.Dto
{
    public class HourDto // Data from given hour
    {/// <summary>
     /// Model for response from API
     /// </summary>
        public int Id { get; set; }
        public decimal Temp_c { get; set; } // Temperature in Celsius
        public decimal Wind_kph { get; set; } // Wind speed in kph
        public decimal Gust_kph { get; set; } // Gustspeed in kph (vindby)
        public decimal Vis_km { get; set; } // Visablility in kilometers
        public string Wind_dir { get; set; } // wind direction
        public ConditionDto Condition { get; set; }
        
        public int SessionDatasId { get; set; }

    }
}
