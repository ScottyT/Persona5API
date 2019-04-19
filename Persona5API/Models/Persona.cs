using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persona5API.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Arcana { get; set; }
        public PersonaStats Stats { get; set; }
        [NotMapped]
        public List<Skills> Skills { get; set; } = new List<Skills>();
        [JsonIgnore]
        public string SkillsJson {
            get { return JsonConvert.SerializeObject(Skills); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Skills.Clear();
                else
                    Skills = JsonConvert.DeserializeObject<List<Skills>>(value);
            }
        }
    }
}
