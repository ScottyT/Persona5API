using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Description { get; set; }
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

        [NotMapped]
        [Display(Name = "Resists")]
        public List<Elements> ResistElements { get; set; } = new List<Elements>();
        [JsonIgnore]
        public string ResistJson
        {
            get { return JsonConvert.SerializeObject(ResistElements); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    ResistElements.Clear();
                else
                    ResistElements = JsonConvert.DeserializeObject<List<Elements>>(value);
            }
        }

        [NotMapped]
        [Display(Name = "Weak")]
        public List<Elements> WeakElements { get; set; } = new List<Elements>();
        [JsonIgnore]
        public string WeakJson
        {
            get { return JsonConvert.SerializeObject(WeakElements); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    WeakElements.Clear();
                else
                    WeakElements = JsonConvert.DeserializeObject<List<Elements>>(value);
            }
        }
    }
}
