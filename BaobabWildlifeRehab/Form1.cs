using BaobabWildlifeRehab.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using static BaobabWildlifeRehab.Features.Animal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaobabWildlifeRehab
{
    public partial class Form1 : Form
    {
        private const string FileName = "animals.txt"; 
        private List<Animal> AnimalList = new List<Animal>();
        public Form1()
        {
            InitializeComponent();
            // Set up the DataGridView columns and properties
            InitializeDataGridView();
            // Load animal data from "animals.txt" to the DataGridView
            LoadAnimalsFromFile();                        
        }

        private void InitializeDataGridView()
        {
            dgvAnimals.AutoGenerateColumns = false; 
            dgvAnimals.ColumnCount = 7;

            dgvAnimals.Columns[0].Name = "Animal ID";       
            dgvAnimals.Columns[0].DataPropertyName = "ID";  

            dgvAnimals.Columns[1].Name = "Name";
            dgvAnimals.Columns[1].DataPropertyName = "Name";

            dgvAnimals.Columns[2].Name = "Species";
            dgvAnimals.Columns[2].DataPropertyName = "Species";

            dgvAnimals.Columns[3].Name = "Age";
            dgvAnimals.Columns[3].DataPropertyName = "Age";

            dgvAnimals.Columns[4].Name = "Recovery Score";
            dgvAnimals.Columns[4].DataPropertyName = "RecoveryScore";

            dgvAnimals.Columns[5].Name = "Status";
            dgvAnimals.Columns[5].DataPropertyName = "Status";

            dgvAnimals.Columns[6].Name = "Housing Unit";
            dgvAnimals.Columns[6].DataPropertyName = "HousingUnit";
        }
        private void RefreshGrid()
        {
            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = AnimalList;
        }

        private void LoadAnimalsFromFile()
        {
            AnimalList.Clear();
            int skippedLines = 0;

            try
            {   // Checks if the file exists.
                if (!File.Exists(FileName))                 
                {
                    // Writes an empty file
                    File.WriteAllText(FileName, "");        
                    RefreshGrid();
                }

                string[] lines = File.ReadAllLines(FileName); 

                foreach (var line in lines)
                {   // Skips empty lines to avoid datagridview crashes
                    if (string.IsNullOrEmpty(line)) continue;
                    // Stores file data into an array and removes whitespace
                    var parts = line.Split('|').Select(p => p.Trim()).ToArray();

                    string id = parts[0];
                    string name = parts[1];
                    string species = parts[2];
                    if (!int.TryParse(parts[3], out int age) || !int.TryParse(parts[4], out int score)) 
                    {
                        skippedLines++;
                        continue;
                    }
                    AnimalList.Add(new Animal(id.ToUpper(), name, species, age, score));
      

                    if (skippedLines > 0)
                    {
                        MessageBox.Show($"Record was succesfully stored. Note: {skippedLines} lines were skipped.", "File Load Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            
            catch (IOException ex)    
            {
                MessageBox.Show($"Error loading animals from file: {ex.Message}", "File Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied. Please ensure you have the necessary permissions to access the file.", "File Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshGrid();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtID.Text = GenerateNextAnimalId(); // Auto-generates the next available ID when the form loads
        }
        private string GenerateNextAnimalId()
        {
            int nextNumber = 1;
            // Checks if the AnimalList has an animals in it
            if (AnimalList != null && AnimalList.Count > 0) 
            {
                var numbers = new List<int>();
                // Loops through the AnimalList to find the highest numbered ID 
                foreach (var animal in AnimalList) 
                {
                    if (!string.IsNullOrEmpty(animal.ID) && animal.ID.StartsWith("WR-") && int.TryParse(animal.ID.Substring(3), out int num)) 
                    {
                        // Counts th available numbers
                        numbers.Add(num); 
                    }
                }

                if (numbers.Count > 0)
                {
                    // +1 to the highest number to give the next available ID
                    nextNumber = numbers.Max() + 1; 
                }
            }
            // format: WR-0000
            return $"WR-{nextNumber:D4}";
        }

        private void ClearInputs()
        {
            txtID.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtID.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim().ToUpper();
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string age = txtAge.Text.Trim();
            string score = txtRecoveryScore.Text.Trim();

            // Checks valid input and if invalid, focuses on the textbox with the issue
            bool InvalidID = id == null || id.Length != 7 || !id.StartsWith("WR-") || !int.TryParse(id.Substring(3), out _);
            if (InvalidID)
            {
                MessageBox.Show("Invalid ID format. Please use the format WR-XXXX where XXXX are numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus(); 
            }

            string[] forbiddenCharacters = {"|", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "."}; 
            if (string.IsNullOrEmpty(name) || name.Length > 30 || forbiddenCharacters.Any(c => name.Contains(c))) 
            {
                MessageBox.Show("Please re-enter a name for the Animal it must be less than 30 characters and not contain any special characters", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(species) || species.Length > 30 || forbiddenCharacters.Any(c => species.Contains(c)))
            {
                MessageBox.Show("Please re-enter a species for the Animal it must be less than 30 characters and not contain any special characters", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSpecies.Focus();
                return;
            }
            if (!int.TryParse(age, out int ageValue) || ageValue < 0 || ageValue > 100)
            {
                MessageBox.Show("Please re-enter a valid age for the Animal (0-100).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAge.Focus();
                return;
            }
            if(!int.TryParse(score, out int scoreValue) || scoreValue < 0 || scoreValue > 100)
            {
                MessageBox.Show("Please re-enter a valid recovery score for the Animal (0-100).", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRecoveryScore.Focus();
                return;
            }
            // Adds the new animal to the list
            Animal newAnimal = new Animal(id, name, species, ageValue, scoreValue); 
            AnimalList.Add(newAnimal);

            // Saves the new animal to the file and handles any potential errors
            try { File.AppendAllText(FileName, newAnimal.ToString() + Environment.NewLine, System.Text.Encoding.UTF8); }
            catch 
            { 
                MessageBox.Show("Error occurred while saving data. Make sure the file is not open in another application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            MessageBox.Show("Animal added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clears & refreshes the GUI 
            ClearInputs();
            RefreshGrid();
            txtID.Text = GenerateNextAnimalId();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
