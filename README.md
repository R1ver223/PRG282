# PRG282
# Boabab Wildlife Rehabilitation Records System

A C# Windows Forms desktop application built for managing injured and orphaned wildlife rehabilitation records, tracking recovery progress, and generating summary reports.


## How to Run the application
1. Clone the repository to your local machine.
`git clone https://github.com/R1ver223/PRG282.git`
2. Change Directory to the project folder.
`cd PRG282`
3. Open the solution file `BaobabWildlifeRehab.sln` in Visual Studio.
`code .`
4. Build the solution and run the application by presseing the `Start` button or `F5`.


## Features 1
1. *Add new Animals* : Validates user inputs(ID format `WR-XXXX`, name/species constraints, and numeric ranges for age and recovery score) then appends the record and calcuates the animals status and housing unit automatically. Then appends them to `animal.txt`.

## Feature 2
3. *View All Animals* : Loads records from `animals.txt` on startup and displays them cleanly in a read-only `DataGridView`.


## File Format
*Layout* : Each animal record in stored on a seperate line in `animals.txt` with the following format:
`ID | Name | Species | Age | Recovery Score | Status | Housing Unit `
**Empty lines are ignored when loading records to prevent crashes.**


## Future Enhacements & Ideas
1. Data Base Implementation transistioning from palin text file to a database for better data management and scalability.
2. Implementing graphical analystics and visualizations to track recovery trends and patterns over time.
# Feature 3 – Update an Animal
The Update Animal feature allows users to edit the details of an existing animal in the Baobab Ridge Wildlife Rehabilitation Records System.

Users can select an animal directly from the DataGridView or search for an animal using its Animal ID. Once an animal is selected, its information is loaded into the input fields for editing.

The Animal ID field is read-only during editing to ensure that the animal's unique identifier cannot be changed. The Add button is disabled, and the Save Changes button is enabled while the form is in edit mode. A Clear button allows users to cancel editing and return the form to Add mode.

When the user saves the changes, the application validates the updated information and recalculates the animal's recovery status and housing unit based on its Recovery Score. The updated record is saved to animals.txt, while all other animal records remain unchanged. The DataGridView refreshes automatically after a successful update.

# Feature 4 – Delete an Animal
The Delete Animal feature allows users to remove an existing animal record from the system.

Users select an animal from the DataGridView and click the Delete button. If no animal is selected, the application displays a message asking the user to select a record.

Before deleting an animal, a confirmation message displays the animal's ID and name to prevent accidental deletion. If the user selects No, the operation is cancelled without changing any records. If the user selects Yes, the selected animal is removed from animals.txt.

The application preserves all remaining animal records and refreshes the DataGridView after deletion. If the deleted animal was loaded into the input fields, the form is cleared to prevent outdated information from being displayed.
