// See https://aka.ms/new-console-template for more information


using AltLangProj.Classes;
using System;
using System.Collections;
using System.Collections.Generic;

//ParseCsvFile temp = new ParseCsvFile(@"Input\cells.csv");

//Console.WriteLine(temp.getRowData().Count);
//Console.WriteLine(temp.getRowData()[0].Count);
//Console.WriteLine(temp.getColumnData().Count);
//Console.WriteLine(temp.getColumnData()[0].Count);


//CleanCellData clean = new CleanCellData(temp.getColumnData(), temp.getRowData()[0]);

//Console.WriteLine(clean.getCleanColumnData().Count);
//Console.WriteLine(clean.getCleanColumnData()[2][3] == null);
//Console.WriteLine(clean.getCleanColumnData()[2][886]);

//CellFields test = new CellFields(clean.getCleanColumnData(), temp.getRowData());


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



//cell.printCellTable();


//cell.printRecord(1);
//Console.WriteLine();
//cell.printRecord(500);
//Console.WriteLine();
//cell.printRecord(1000);
//Console.WriteLine();


//cell.printMultipleRecords(new int[] {1, 500, 1000});

//cell.printCustomString(1, new string[] { "id", "model", "oem" });

//cell.printCustomMultipleRecords(new int[] { 1, 500, 1000 }, new string[] { "id", "platform_os", "oem" });

//cell.deleteRecord(5);


//cell.addRecord(null, null, null, "poop", null, null, null, null, null, null, "a,b,c", null);

/*
foreach (string VARIABLE in cell.getFieldsMap().GetFeaturesSensors())
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


//cell.printCellTable();
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

//cell.printCellTable("oem");

//cell.printCellTable();

///////////////////////////////

FilterParameters query = new FilterParameters();
query.getFilterIntRange().Add("launch_status", new KeyValuePair<int,int>(2000, 2030));
//query.getFilterInt().Add("launch_status", new []{2020});



CellTable cell2 = cell.createQueryTable(query);


cell2.printCellTable("launch_status");

cell2.printFrequencyTable("launch_status");

Console.WriteLine("\nThis year had the most phones launched in the 2000's:  " + cell2.getModeLaunchStatus());

/////////////////////////////

Console.WriteLine();

FilterParameters query2 = new FilterParameters();
query2.getFilterInt().Add("features_sensors", new[]{1});

CellTable cell3 = cell.createQueryTable(query2);

cell3.printCellTable();

cell3.printFrequencyTable("features_sensors");

Console.WriteLine("\nThis is the number of phones with only one feature sensor:  " + cell3.getFeaturesSensorsCountElementCount(1));


/////////////////////////////////////

Console.WriteLine();


Console.WriteLine("\nThe company (oem) with the highest average weight of the phone body: " + cell.printAvgPerOem("body_weight"));

///////////////////////////////////////////

Console.WriteLine();

CellTable cell4 = cell.getPhonesLaunchedAfterAnnouncedTable();

cell4.printTableSize(); ////////////

//cell4.printCellTable();

cell4.printCustomMultipleRecords(cell4.getRecordsMap().get_cell_table().Keys.ToArray(), new []{"id", "oem", "model", "launch_announced", "launch_status"});


///////////////////////////////////

Console.WriteLine();
cell4.printCellTable();
cell4.printFeatureSensorList(835);

Console.WriteLine(cell4.getType("body_weight"));



////////////////////////////////////////////


FilterParameters query3 = new FilterParameters();
query3.getFilterString().Add("oem", new []{"Google", "Sony"});

CellTable cell5 = cell.createQueryTable(query3);

cell5.printCellTable();

cell5.printTableStats();

//Console.WriteLine(cell5.getType("features_sensors"));

Console.WriteLine();

ParseCsvFile parser = new ParseCsvFile(@"Resources\Input\cells.csv");
CleanCellData cleanData = new CleanCellData(parser.getColumnData(), parser.getRowData()[0]);
cleanData.printHasMissingData();


Console.ReadKey();










