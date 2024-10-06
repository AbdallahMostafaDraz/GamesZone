namespace GameZone.Enities.Models
{
    public class GameDevice 
    {
        public int GameId { get; set; }
        public Game Game { get; set; } 

        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
