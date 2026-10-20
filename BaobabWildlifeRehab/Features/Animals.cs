using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaobabWildlifeRehab.Features
{
    public class Animal
    {
        // Animal class with properties for ID, Name, Species, Age, RecoveryScore, Status, and HousingUnit
        public string ID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public int RecoveryScore { get; set; }
        public string Status { get; private set; }
        public string HousingUnit { get; private set; }

        public Animal(string id, string name, string species, int age, int recoveryScore)
        {
            ID = id;
            Name = name;
            Species = species;
            Age = age;
            RecoveryScore = recoveryScore;
            ClassifyAnimal(recoveryScore); // Automatically sets status and housing unit on creation
        }

        // Centralized classification method as required by Section 4 Determines Status and Housing Unit based on the Recovery Score.
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
        // foramt to store data in animal.txt file
        public override string ToString()
        {
            return $"{ID} | {Name} | {Species} | {Age} | {RecoveryScore} | {Status} | {HousingUnit}";
        }
    }
}