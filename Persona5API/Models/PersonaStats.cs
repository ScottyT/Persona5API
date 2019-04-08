using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Persona5API.Models
{
    public class PersonaStats
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int Strength { get; set; }
        public int Magic { get; set; }
        public int Endurance { get; set; }
        public int Agility { get; set; }
        public int Luck { get; set; }
    }
}