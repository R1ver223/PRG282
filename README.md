# PRG282
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
