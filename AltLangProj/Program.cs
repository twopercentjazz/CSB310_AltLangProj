// See https://aka.ms/new-console-template for more information


using AltLangProj.Classes;

//ParseCsvFile temp = new ParseCsvFile(@"Input\cells.csv");

//Console.WriteLine(temp.GetRowData().Count);
//Console.WriteLine(temp.GetRowData()[0].Count);
//Console.WriteLine(temp.GetColumnData().Count);
//Console.WriteLine(temp.GetColumnData()[0].Count);


//CleanCellData clean = new CleanCellData(temp.GetColumnData(), temp.GetRowData()[0]);

//Console.WriteLine(clean.GetCleanColumnData().Count);
//Console.WriteLine(clean.GetCleanColumnData()[2][3] == null);
//Console.WriteLine(clean.GetCleanColumnData()[2][886]);

//CellFields test = new CellFields(clean.GetCleanColumnData(), temp.GetRowData());


/*
int count = 2;
Console.WriteLine(test.get_initial_fields()[0]);
foreach (var item in test.get_cell_id())
{   Console.Write(count + " ");
    if (item == null)
    {
        Console.Write("null");
    }
    else
    {
        Console.Write(item);
    }
    Console.WriteLine();
    count++;

}
*/




// @"Input\cells.csv"


CellTable test = new CellTable(@"test");

CellTable test2 = new CellTable(@"Resources\Input\empty.csv");


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


//cell.PrintCellTable();
/*
Console.WriteLine();

Console.WriteLine(cell.getAvgBodyWeight());

Console.WriteLine(cell.getAvgDisplaySize());

Console.WriteLine(cell.getAvgFeaturesSensorsCount());

Console.WriteLine();

Console.WriteLine(cell.getMedianBodyWeight());

Console.WriteLine(cell.getMedianDisplaySize());

Console.WriteLine(cell.getMedianFeaturesSensorsCount());

Console.WriteLine();

Console.WriteLine(cell.getModeOem());

Console.WriteLine(cell.getModeModel());

Console.WriteLine(cell.getModeLaunchAnnounced());

Console.WriteLine(cell.getModeLaunchStatus());

Console.WriteLine(cell.getModeBodyDimensions());

Console.WriteLine(cell.getModeBodyWeight());

Console.WriteLine(cell.getModeBodySim());

Console.WriteLine(cell.getModeDisplayType());

Console.WriteLine(cell.getModeDisplaySize());

Console.WriteLine(cell.getModeDisplayResolution());

Console.WriteLine(cell.getModeFeaturesSensorsCount());

Console.WriteLine(cell.getModePlatformOs());

Console.WriteLine();
*/

//cell.PrintCellTable("oem");

//cell.PrintCellTable();

///////////////////////////////

FilterParameters query = new FilterParameters();
query.GetFilterIntRange().Add("launch_status", new KeyValuePair<int,int>(2000, 2030));
//query.GetFilterInt().Add("launch_status", new []{2020});



CellTable cell2 = cell.createQueryTable(query);


cell2.PrintCellTable("launch_status");

cell2.PrintFrequencyTable("launch_status");

Console.WriteLine("\nThis year had the most phones launched in the 2000's:  " + cell2.getModeLaunchStatus());

/////////////////////////////

Console.WriteLine();

FilterParameters query2 = new FilterParameters();
query2.GetFilterInt().Add("features_sensors", new[]{1});

CellTable cell3 = cell.createQueryTable(query2);

cell3.PrintCellTable();

cell3.PrintFrequencyTable("features_sensors");

Console.WriteLine("\nThis is the number of phones with only one feature sensor:  " + cell3.getFeaturesSensorsCountElementCount(1));


/////////////////////////////////////

Console.WriteLine();


Console.WriteLine("\nThe company (oem) with the highest average weight of the phone body: " + cell.PrintAvgPerOemTable("body_weight"));

///////////////////////////////////////////

Console.WriteLine();

CellTable cell4 = cell.getPhonesLaunchedAfterAnnouncedTable();

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

CellTable cell5 = cell.createQueryTable(query3);

cell5.PrintCellTable();

cell5.PrintTableStats();

//Console.WriteLine(cell5.GetType("features_sensors"));

Console.WriteLine();

ParseCsvFile parser = new ParseCsvFile(@"Resources\Input\cells.csv");
CleanCellData cleanData = new CleanCellData(parser.GetColumnData(), parser.GetRowData()[0]);
cleanData.PrintHasMissingData();


Console.ReadKey();










