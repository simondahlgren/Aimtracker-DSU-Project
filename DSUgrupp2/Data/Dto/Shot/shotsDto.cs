using System.Drawing;

namespace DSUgrupp2.Data.Dto.Shot
{
    public class shotsDto
    {
        /// <summary>
        /// Model for response from API
        /// </summary>

        public int Id { get; set; }
        public int HeartRate { get; set; }
        public string Result { get; set; }
        public int FiringIndex { get; set; }
        public double FiringAngle { get; set; }
        public CoordsDto FiringCoords { get; set; }
        public List<ShotCoordinatesDto> ShotXY {get; set;}
        public double TimeToFire { get; set; }


        

    }
}
