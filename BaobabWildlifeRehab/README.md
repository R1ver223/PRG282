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


Group Members
	-Ruben Prince (603319) 
	-
	-
	-


## Features
1. *Add new Animals* : Validates user inputs(ID format `WR-XXXX`, name/species constraints, and numeric ranges for age and recovery score) then appends the record and calcuates the animals status and housing unit automatically. Then appends them to `animal.txt`.
2. *View All Animals* : Loads records from `animals.txt` on startup and displays them cleanly in a read-only `DataGridView`.


## File Format
*Layout* : Each animal record in stored on a seperate line in `animals.txt` with the following format:
`ID | Name | Species | Age | Recovery Score | Status | Housing Unit `
**Empty lines are ignored when loading records to prevent crashes.**


## Future Enhacements & Ideas
1. Data Base Implementation transistioning from palin text file to a database for better data management and scalability.
2. Implementing graphical analystics and visualizations to track recovery trends and patterns over time.