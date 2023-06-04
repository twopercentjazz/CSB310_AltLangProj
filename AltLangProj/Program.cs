// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using AltLangProj.Classes;
// When running this console application driver I recommend full screening the console


/* first I will test...
 1. That a parsed/cleaned file isn't missing data, should display [The data doesn't have missing values] 
 2. To demonstrate that my code catches when a file doesn't exist, should display [File Does Not Exist]
 3. To demonstrate that my code catches when a file is empty, should display [File Is Empty]
 */
string m1 = "These first tests check for bad input and displays a message to the console";
Console.WriteLine(m1 + "\n" + CellRecords.TableBorder(m1) + "\n");

// 1
Console.WriteLine("Checking my cleaned data...");
ParseCsvFile parser = new ParseCsvFile(@"Resources\Input\cells.csv");
CleanCellData cleanData = new CleanCellData(parser.GetColumnData(), parser.GetRowData()[0]);
cleanData.PrintHasMissingData();
Console.WriteLine();

// 2
Console.WriteLine("Checking a bad file path...");
CellTable fileDoesntExist = new CellTable(@"test");
Console.WriteLine();

// 3
Console.WriteLine("Checking an empty file...");
CellTable fileIsEmpty = new CellTable(@"Resources\Input\empty.csv");
Console.WriteLine();


/* next I will test...
 1. Creating a Cell Table
 2. Print a single record (if record doesn't exist displays message)
 3. As I mentioned in my cell class comments, for the sake of saving space I display the 
    features sensors count in place of the actual feature sensors when displaying a 
    record to the console, so I created a method to print this information
 4. print multiple records
 5. print custom multiple records (only displaying given fields)
 6. Because 1000 records take up a lot of screen real estate, I will demo my other methods by 
    first creating a smaller query table where the oem is either "Lenovo" or "Sony" and the 
    launch_status is "2019" or "2020"
    (to learn more about creating queries see my FilterParameters class for details)
 7. Print entire query table
 8. print entire query table sorted on any given column
 9. Delete record
10. Add record
11. Display table stats including:
        - table row count
        - table column count
        - Per field:
                - Data type (for each)
                - unique element count (for each)
                - mode (for each)
                - mode frequency count (for each)
                - mean (for numeric columns)
                - median (for numeric columns)
 12. Print frequency table
 */

Console.WriteLine();
string m2 = "These next tests demonstrate the use of my series of methods";
Console.WriteLine(m2 + "\n" + CellRecords.TableBorder(m2));

// 1
CellTable cell = new CellTable(@"Resources\Input\cells.csv");
Console.WriteLine("[cell table was successfully created from cells.csv file]\n");

// 2
Console.WriteLine("Print record with record id 2000...");
cell.PrintRecord(2000);
Console.WriteLine();
Console.WriteLine("\nPrint record with record id 5...");
cell.PrintRecord(5);

// 3
Console.WriteLine("Retrieve features sensors description for record above...");
cell.PrintFeatureSensorList(5);
Console.WriteLine();

// 4
Console.WriteLine("\nPrint multiple records with record id 1, 500, and 1000...");
cell.PrintMultipleRecords(new []{1,500,1000});
Console.WriteLine();

// 5
Console.WriteLine("Print the same multiple records above but only show id, oem, and launch status columns...");
cell.PrintCustomMultipleRecords(new[] { 1, 500, 1000 }, new []{"id", "oem", "launch_status"});
Console.WriteLine();

// 6
Console.WriteLine("Create new query table that only includes the 'Lenovo' and 'Sony' oem with the years of '2019' or '2020' launch_status...");
FilterParameters query = new FilterParameters();
query.GetFilterString().Add("oem", new[] { "Sony", "Lenovo" });
query.GetFilterString().Add("launch_status", new []{"2019", "2020"});
CellTable queryTable = cell.CreateQueryTable(query);
Console.WriteLine("[query table was successfully created from cell table]\n");

// 7
Console.WriteLine("Print entire query table...");
queryTable.PrintCellTable();
Console.WriteLine();

// 8
Console.WriteLine("Print entire query table sorted on 'body_weight' column (can sort on any column)...");
queryTable.PrintCellTable("body_weight");
Console.WriteLine();

// 9
Console.WriteLine("Delete the last 5 records from the table above...");
queryTable.DeleteRecord(new []{160, 159, 165, 164, 166});
Console.WriteLine("[records deleted successfully]\n");
Console.WriteLine("Print entire query table sorted on 'body_weight' again to see records were deleted...");
queryTable.PrintCellTable("body_weight");
Console.WriteLine();

// 10
Console.WriteLine("Add a new record for the Apple iphone 11...");
queryTable.AddRecord("Apple", "Iphone 11", 2019, "2019", "150 x 75 x 8 mm", 194, "Dual SIM", 
    "IPS LCD Touchscreen", 6.1, "1792 x 828 px", "V1, V2, V3", "Apple iOS");
Console.WriteLine("[record added successfully]\n");
Console.WriteLine("Print entire query table sorted on 'oem' to see that the new record was added...");
queryTable.PrintCellTable("oem");
Console.WriteLine();

// 11
Console.WriteLine("Display Statistics for this query table (including data types for verification)...");
queryTable.PrintTableStats();
Console.WriteLine();

// 12
Console.WriteLine();
Console.WriteLine("To get more details about the frequency of elements you can print a frequency chart, here is the chart for the oem column...");
queryTable.PrintFrequencyTable("oem");
Console.WriteLine();
Console.WriteLine();


/* next I will answer the report questions...
 1. What company has the highest average weight of the phone body?
 2. Was there any phones that were announced in one year and released in another? What are they? Give me the oem and models?
 3. How many phones have only one feature sensor?
 4. What year had the most phones launched in the 2000s?
 */

Console.WriteLine();
string m3 = "These next tests are used to answer the four questions required for the report";
Console.WriteLine(m3 + "\n" + CellRecords.TableBorder(m3) + "\n");
Console.WriteLine();

// 1
string m4 = "1. What company has the highest average weight of the phone body?";
Console.WriteLine(m4 + "\n" + CellRecords.TableBorder(m4) + "\n");
Console.WriteLine("Using this table...");
Console.WriteLine("\nThe company (oem) with the highest average weight of the phone body: " + cell.PrintAvgPerOemTable("body_weight"));
Console.WriteLine();
Console.WriteLine();

// 2
Console.WriteLine();
string m5 = "2. Was there any phones that were announced in one year and released in another? What are they? Give me the oem and models?";
Console.WriteLine(m5 + "\n" + CellRecords.TableBorder(m5) + "\n");
Console.WriteLine("Create new custom query table to solve this...");
CellTable queryTable2 = cell.GetPhonesLaunchedAfterAnnouncedTable();
Console.WriteLine("[query table was successfully created from cell table]\n");
Console.WriteLine("The oem and models of the phones that were announced in one year and released later are represented in the table below...");
queryTable2.PrintCustomMultipleRecords(queryTable2.GetRecordsMap().GetCellTable().Keys.ToArray(), new[] { "oem", "model", "launch_announced", "launch_status" });
Console.WriteLine();

// 3
Console.WriteLine();
string m6 = "3. How many phones have only one feature sensor?";
Console.WriteLine(m6 + "\n" + CellRecords.TableBorder(m6) + "\n");
Console.WriteLine("Using this table...");
cell.PrintFrequencyTable("features_sensors");
Console.WriteLine("\nThis is the number of phones with only one feature sensor:  " + cell.GetFeaturesSensorsCountElementCount(1));
Console.WriteLine();

// 4 
Console.WriteLine();
Console.WriteLine();
string m7 = "4. What year had the most phones launched in the 2000s?";
Console.WriteLine(m7 + "\n" + CellRecords.TableBorder(m7) + "\n");
Console.WriteLine("Create new query table to solve this...");
FilterParameters query3 = new FilterParameters();
query3.GetFilterIntRange().Add("launch_status", new KeyValuePair<int, int>(2000, 2030));
CellTable queryTable3 = cell.CreateQueryTable(query3);
Console.WriteLine("[query table was successfully created from cell table]\n");
Console.WriteLine("Using this table...");
queryTable3.PrintFrequencyTable("launch_status");
Console.WriteLine("\nThis year had the most phones launched in the 2000's:  " + queryTable3.GetModeLaunchStatus());
Console.WriteLine();

/* next I will use Debug Asserts to create my 3 required unit tests...
 1. Test calcAvg
 2. Test CalcMedian
 3. Test CalcMode
 */

Console.WriteLine();
Console.WriteLine();
string m8 = "These next tests are my three required unit tests (using Debug Asserts)";
Console.WriteLine(m8 + "\n" + CellRecords.TableBorder(m8) );
Console.WriteLine();

List<double?> testData = new List<double?> {1,1,2,2,2,3,4};

// 1
Console.WriteLine("Test CalcAvg method...");
double test1 = cell.CalcAvg(testData);
Debug.Assert(test1.Equals(15.0 / 7.0));
Console.WriteLine("[CalcAvg Test successful]\n");

// 2
Console.WriteLine("Test CalcMedian method...");
double test2 = cell.CalcMedian(testData);
Debug.Assert(test2.Equals(2));
Console.WriteLine("[CalcMedian Test successful]\n");

// 3
Console.WriteLine("Test CalcMode method...");
double test3 = cell.CalcMode(testData);
Debug.Assert(test3.Equals(2));
Console.WriteLine("[CalcMode Test successful]\n");

Console.WriteLine("[All unit tests passed]\n");
Console.ReadKey();

