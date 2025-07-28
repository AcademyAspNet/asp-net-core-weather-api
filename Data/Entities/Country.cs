namespace WeatherAPI.Data.Entities
{
    public class Country : Entity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
    }
}
