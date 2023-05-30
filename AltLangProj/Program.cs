// See https://aka.ms/new-console-template for more information


using AltLangProj.Classes;
using System;

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

cell.printCustomMultipleRecords(new int[] { 1, 500, 1000 }, new string[] { "id", "platform_os", "oem" });


Console.ReadKey();










