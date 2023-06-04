// See https://aka.ms/new-console-template for more information


using AltLangProj.Classes;


/* first I will test...
 1. That a parsed/cleaned file isn't missing data, should display [The data doesn't have missing values] 
 2. To demonstrate that my code catches when a file doesn't exist, should display [File Does Not Exist]
 3. To demonstrate that my code catches when a file is empty, should display [File Is Empty]
 */

Console.WriteLine("These first tests check for bad input and displays a message to the console:\n");

// 1
ParseCsvFile parser = new ParseCsvFile(@"Resources\Input\cells.csv");
CleanCellData cleanData = new CleanCellData(parser.GetColumnData(), parser.GetRowData()[0]);
cleanData.PrintHasMissingData();
Console.WriteLine();

// 2
CellTable fileDoesntExist = new CellTable(@"test");
Console.WriteLine();

// 3
CellTable fileIsEmpty = new CellTable(@"Resources\Input\empty.csv");
Console.WriteLine();


/* next I will test...
 1. That a parsed/cleaned file isn't missing data, should display [The data doesn't have missing values] 
 2. To demonstrate that my code catches when a file doesn't exist, should display [File Does Not Exist]
 3. To demonstrate that my code catches when a file is empty, should display [File Is Empty]
 */

Console.WriteLine("These first tests check for bad input and displays a message to the console:\n");















CellTable cell = new CellTable(@"Resources\Input\cells.csv");



//cell.PrintCellTable();


//cell.PrintRecord(1);
//Console.WriteLine();
//cell.PrintRecord(500);
//Console.WriteLine();
//cell.PrintRecord(1000);
//Console.WriteLine();


//cell.PrintMultipleRecords(new int[] {1, 500, 1000});

//cell.PrintCustomString(1, new string[] { "id", "model", "oem" });

//cell.PrintCustomMultipleRecords(new int[] { 1, 500, 1000 }, new string[] { "id", "platform_os", "oem" });

//cell.DeleteRecord(5);


//cell.AddRecord(null, null, null, "poop", null, null, null, null, null, null, "a,b,c", null);

/*
foreach (string VARIABLE in cell.GetFieldsMap().GetFeaturesSensors())
{
    if (VARIABLE == null)
    {
        Console.WriteLine("null");
    }
    else
    {
        Console.WriteLine(VARIABLE);
    }
    
}
*/




//cell.PrintCellTable("oem");

//cell.PrintCellTable();

///////////////////////////////

FilterParameters query = new FilterParameters();
query.GetFilterIntRange().Add("launch_status", new KeyValuePair<int,int>(2000, 2030));
//query.GetFilterInt().Add("launch_status", new []{2020});



CellTable cell2 = cell.CreateQueryTable(query);


cell2.PrintCellTable("launch_status");

cell2.PrintFrequencyTable("launch_status");

Console.WriteLine("\nThis year had the most phones launched in the 2000's:  " + cell2.GetModeLaunchStatus());

/////////////////////////////

Console.WriteLine();

FilterParameters query2 = new FilterParameters();
query2.GetFilterInt().Add("features_sensors", new[]{1});

CellTable cell3 = cell.CreateQueryTable(query2);

cell3.PrintCellTable();

cell3.PrintFrequencyTable("features_sensors");

Console.WriteLine("\nThis is the number of phones with only one feature sensor:  " + cell3.GetFeaturesSensorsCountElementCount(1));


/////////////////////////////////////

Console.WriteLine();


Console.WriteLine("\nThe company (oem) with the highest average weight of the phone body: " + cell.PrintAvgPerOemTable("body_weight"));

///////////////////////////////////////////

Console.WriteLine();

CellTable cell4 = cell.GetPhonesLaunchedAfterAnnouncedTable();

cell4.PrintTableSize(); ////////////

//cell4.PrintCellTable();

cell4.PrintCustomMultipleRecords(cell4.GetRecordsMap().GetCellTable().Keys.ToArray(), new []{"id", "oem", "model", "launch_announced", "launch_status"});


///////////////////////////////////

Console.WriteLine();
cell4.PrintCellTable();
cell4.PrintFeatureSensorList(835);

Console.WriteLine(cell4.GetType("body_weight"));



////////////////////////////////////////////


FilterParameters query3 = new FilterParameters();
query3.GetFilterString().Add("oem", new []{"Google", "Sony"});

CellTable cell5 = cell.CreateQueryTable(query3);

cell5.PrintCellTable();

cell5.PrintTableStats();

//Console.WriteLine(cell5.GetType("features_sensors"));

Console.WriteLine();




Console.ReadKey();










