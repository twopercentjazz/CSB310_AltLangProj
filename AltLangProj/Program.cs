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










CellTable cell = new CellTable(@"Input\cells.csv");



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
foreach (int? VARIABLE in cell.getFieldsMap().get_year_of_launch())
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

//cell.printCellTable("oem");

//cell.printCellTable();

FilterParameters query = new FilterParameters();
query.getFilterString().Add("oem", new[] { "Google", "Sony" });


CellTable cell2 = cell.createQueryTable(query);


cell2.printCellTable("oem");

cell2.printFrequencyTable("oem");







Console.ReadKey();










