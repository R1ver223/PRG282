using BaobabWildlifeRehab.Features;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private const string FileName = "animals.txt"; //constant variable for the file name
        private List<Animal> AnimalList = new List<Animal>(); //list for animals
        public Form1()
        {
            InitializeComponent();
             //add? load animals from file on form load
            InitializeDataGridView();
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
        private void RefreshGrid() //refreshes the datagridview with the current list of animals
        {
            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = AnimalList;
        }

        private void LoadAnimalsFromFile()
        {
            AnimalList.Clear();
            int skippedLines = 0;

            try
            {
                if (!File.Exists(FileName)) //checks if file does not exist
                {
                    File.WriteAllText(FileName, ""); // Create an empty file if it doesn't exist
                    RefreshGrid();
                }

                string[] lines = File.ReadAllLines(FileName); //temporary array to hold the lines of the file

                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line)) continue; // Skip empty lines to ensure the datagrid view doesn't break

                    var parts = line.Split('|').Select(p => p.Trim()).ToArray(); //splits the line into parts and trims whitespace


                    string id = parts[0];
                    string name = parts[1];
                    string species = parts[2];


                    if (!int.TryParse(parts[3], out int fileAge) || !int.TryParse(parts[4], out int fileScore))
                    {
                        skippedLines++;
                        continue;
                    }
                    AnimalList.Add(new Animal(id.ToUpper(), name, species, fileAge, fileScore));
      

                    if (skippedLines > 0)
                    {
                        MessageBox.Show($"Record was succesfully stored. Note: {skippedLines} lines were skipped.", "File Load Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (IOException ex)    //light exception handeling just so the program doesn t crash
            {
                MessageBox.Show($"Error loading animals from file: {ex.Message}", "File Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied. Please ensure you have the necessary permissions to access the file.", "File Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshGrid();
        }
        //Auto generates an available ID for the next animal to be added
        private string GenerateNextAnimalId()
        {
            int nextNumber = 1;

            if (AnimalList != null && AnimalList.Count > 0) //checks if the list is not null and has animals in it
            {
                var numbers = new List<int>();
                foreach (var animal in AnimalList) //iterates through the list of animals to find the highest number used in the ID
                {
                    if (!string.IsNullOrEmpty(animal.ID) && animal.ID.StartsWith("WR-") && int.TryParse(animal.ID.Substring(3), out int num)) 
                    {
                        numbers.Add(num); 
                    }
                }

                if (numbers.Count > 0)
                {
                    nextNumber = numbers.Max() + 1; 
                }
            } 
            return $"WR-{nextNumber:D4}"; //formats the number to be 4 digits long, padded with leading zeros 
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            txtID.Text = GenerateNextAnimalId(); //Set the initial ID when the form loads
            txtID.ReadOnly = true; 
            btnSaveChanges.Enabled = false;
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

            //checks if the ID is valid, must be 7 characters long, start with WR- and the last 4 characters must be numbers
            bool InvalidID = id == null || id.Length != 7 || !id.StartsWith("WR-") || !int.TryParse(id.Substring(3), out _);
            if (InvalidID)
            {
                MessageBox.Show("Invalid ID format. Please use the format WR-XXXX where XXXX are numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus(); //refocuses on where the issue is
            }
            string[] forbiddenCharacters = {"|", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "."}; //forbidden characters foor validation

            if (string.IsNullOrEmpty(name) || name.Length > 30 || forbiddenCharacters.Any(c => name.Contains(c))) //checks if name contains any forbidden characters
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

            Animal newAnimal = new Animal(id, name, species, ageValue, scoreValue); //check it out don t work yet
            AnimalList.Add(newAnimal);

            try { File.AppendAllText(FileName, newAnimal.ToString() + Environment.NewLine, System.Text.Encoding.UTF8); }
            catch 
            { 
                MessageBox.Show("Error occurred while saving data. Make sure the file is not open in another application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Confirmation message after successful addition
            MessageBox.Show("Animal added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Clear & refresh the UI (textboxes and grid)
            ClearInputs();
            RefreshGrid();
            txtID.Text = GenerateNextAnimalId();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvAnimals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            txtID.Text = dgvAnimals.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvAnimals.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSpecies.Text = dgvAnimals.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtAge.Text = dgvAnimals.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtRecoveryScore.Text = dgvAnimals.Rows[e.RowIndex].Cells[4].Value.ToString();

            btnAdd.Enabled = false;
            btnAdd.BackColor = Color.DarkGray;
            btnSaveChanges.Enabled = true;

        }
    }
}
