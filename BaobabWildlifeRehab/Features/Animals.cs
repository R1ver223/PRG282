using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabWildlifeRehab.Features
{
    public class Animal
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public int RecoveryScore { get; set; }

        // Private set is used for Data Protection as these properties should only be set within the class 
        public string Status { get; private set; }
        public string HousingUnit { get; private set; }

        public Animal(string id, string name, string species, int age, int recoveryScore)
        {
            ID = id;
            Name = name;
            Species = species;
            Age = age;
            RecoveryScore = recoveryScore;
            ClassifyAnimal(recoveryScore); 
        }

        // Classify the animal based on the recovery score 
        public void ClassifyAnimal(int score)
        {
            if (score >= 0 && score <= 19)
            {
                Status = "Critical";
                HousingUnit = "Intensive Care Unit";
            }
            else if (score >= 20 && score <= 39)
            {
                Status = "Serious";
                HousingUnit = "High-Dependency Ward";
            }
            else if (score >= 40 && score <= 59)
            {
                Status = "Stable";
                HousingUnit = "Recovery Ward";
            }
            else if (score >= 60 && score <= 79)
            {
                Status = "Recovering";
                HousingUnit = "Outdoor Enclosure";
            }
            else if (score >= 80 && score <= 100)
            {
                Status = "Release-Ready";
                HousingUnit = "Pre-Release Camp";
            }
        }
        // Overides the built in ToString method to store the animal data in the specified format  
        public override string ToString()
        {
            return $"{ID} | {Name} | {Species} | {Age} | {RecoveryScore} | {Status} | {HousingUnit}";
        }
    }
}