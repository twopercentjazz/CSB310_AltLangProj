namespace AltLangProj.Classes;

/// <summary>
/// This class represents a Cell Table and contains both a column oriented and a row oriented representation
/// of the Cell Table.
///
/// Note: most of my required additional methods are found in this class. I demonstrate the use of these methods
/// by running the Program.cs class. My methods experiment with both the row oriented table and the column
/// oriented table. 
/// </summary>
public class CellTable
{
    private CellRecords? _recordsMap;
    private CellFields? _fieldsMap;
    private int _nextId;
    private string _filePath;

    /// <summary>
    /// This Constructs a CellTable object.
    /// </summary>
    /// <param name="filePath"> The filepath for the given cells csv file </param>
    public CellTable(string filePath)
    {
        ParseCsvFile parser = new ParseCsvFile(filePath);
        if (File.Exists(filePath) && new FileInfo(filePath).Length != 0)
        {
            CleanCellData cleanData = new CleanCellData(parser.GetColumnData(), parser.GetRowData()[0]);
            CellFields fields = new CellFields(cleanData.GetCleanColumnData(), parser.GetRowData());
            CellRecords records = new CellRecords(fields);
            this._recordsMap = records;
            this._fieldsMap = fields;
            this._nextId = GetRecordsMap().GetCellTable().Count + 1;
            this._filePath = filePath;
        }
        else
        {
            this._recordsMap = null;
            this._fieldsMap = null;
            this._nextId = 0;
            this._filePath = "";
        }
    }

    /// <summary>
    /// This Constructs a CellTable object, and is used to construct a new Copy of an existing CellTable object.
    /// </summary>
    /// <param name="records"> The records map (table) </param>
    /// <param name="fields"> The fields map (table) </param>
    /// <param name="nextId"> The next id (for adding more items) </param>
    /// <param name="filePath"> The filepath for the given cells csv file </param>
    public CellTable(CellRecords records, CellFields fields, int nextId, string filePath)
    {
        this._recordsMap = records;
        this._fieldsMap = fields;
        this._nextId = nextId;
        this._filePath = filePath;
    }

    /// <summary>
    /// This method gets the records table class.
    /// </summary>
    /// <returns> The records table class </returns>
    public CellRecords GetRecordsMap()
    {
        return this._recordsMap;
    }

    /// <summary>
    /// This method sets the records table class.
    /// </summary>
    /// <param name="recordsMap"> The new records table class </param>
    public void SetRecordsMap(CellRecords recordsMap)
    {
        this._recordsMap = recordsMap;
    }

    /// <summary>
    /// This method gets the fields table class.
    /// </summary>
    /// <returns> The fields table class </returns>
    public CellFields GetFieldsMap()
    {
        return this._fieldsMap;
    }

    /// <summary>
    /// This method sets the fields table class.
    /// </summary>
    /// <param name="fieldsMap"> The new fields table class </param>
    public void SetFieldsMap(CellFields fieldsMap)
    {
        this._fieldsMap = fieldsMap;
    }

    /// <summary>
    /// This method gets the next id number.
    /// </summary>
    /// <returns> The next id number </returns>
    public int GetNextId()
    {
        return this._nextId;
    }

    /// <summary>
    /// This method sets the next id number.
    /// </summary>
    /// <param name="nextId"> The next id number </param>
    public void SetNextId(int nextId)
    {
        this._nextId = nextId;
    }

    /// <summary>
    /// This method increments the next id number by one.
    /// </summary>
    public void IncrementNextId()
    {
        this._nextId++;
    }

    /// <summary>
    /// This method gets the cells csv file path.
    /// </summary>
    /// <returns> The cells csv file path </returns>
    public string GetFilePath()
    {
        return this._filePath; 
    }

    /// <summary>
    /// This method sets the cells csv file path.
    /// </summary>
    /// <param name="filePath"> The cells csv file path </param>
    public void SetFilePath(string filePath)
    {
        this._filePath = filePath;
    }

    /// <summary>
    /// This method prints the entire Cell Table.
    /// </summary>
    public void PrintCellTable()
    {
        Console.WriteLine(TableToString());
    }

    /// <summary>
    /// This method prints the entire Cell Table sorted by the given column.
    /// </summary>
    /// <param name="field"> The field to sort the table on </param>
    public void PrintCellTable(string field)
    {
        PrintMultipleRecords(GetIdListSorted(field));
    }

    /// <summary>
    /// This method prints one record from the Cell Table.
    /// </summary>
    /// <param name="id"> The record id to print </param>
    public void PrintRecord(int id)
    {
        if (GetRecordsMap().GetCellTable().ContainsKey(id))
        {
            Console.WriteLine(RecordString(GetRecordsMap().GetCellTable()[id]));
        }
        else
        {
            Console.WriteLine("[a record with the given id does not exist]");
        }
    }

    /// <summary>
    /// This method prints multiple records from the Cell Table.
    /// </summary>
    /// <param name="idList"> The list of record ids to print </param>
    public void PrintMultipleRecords(int[] idList)
    {
        Cell[] temp = new Cell[idList.Length];
        int i = 0;
        foreach (int id in idList)
        {
            temp[i] = GetRecordsMap().GetCellTable()[id];
            i++;
        }
        Console.WriteLine(MultiRecordString(temp));
    }

    /// <summary>
    /// This method prints one record displaying only the given fields.
    /// </summary>
    /// <param name="id"> The id number of the record to print </param>
    /// <param name="field"> The list of field names to include </param>
    public void PrintCustomString(int id, string[] field)
    {
        Console.WriteLine(CustomRecordString(GetRecordsMap().GetCellTable()[id], field));
    }

    /// <summary>
    /// This method prints multiple records displaying only the given fields.
    /// </summary>
    /// <param name="idList"> The list of record ids to print </param>
    /// <param name="fields"> The list of field names to include </param>
    public void PrintCustomMultipleRecords(int[] idList, string[] fields)
    {
        Cell[] temp = new Cell[idList.Length];
        int i = 0;
        foreach (int id in idList)
        {
            temp[i] = GetRecordsMap().GetCellTable()[id];
            i++;
        }
        Console.WriteLine(CustomMultiRecordString(temp, fields));
    }

    /// <summary>
    /// This method prints the features sensors description for a given record.
    /// </summary>
    /// <param name="id"> The record id to print </param>
    public void PrintFeatureSensorList(int id)
    {
        int index = GetFieldsMap().GetId().IndexOf(id);
        Console.WriteLine("Record " + id + " has the following feature sensors: " + GetFieldsMap().GetFeaturesSensors()[index]);
    }

    /// <summary>
    /// This method prints a frequency table for a field.
    /// </summary>
    /// <param name="field"> The field to display </param>
    public void PrintFrequencyTable(string field)
    {
        if (field == "oem")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetOem());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "model")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetModel());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "launch_announced")
        {
            Dictionary<int, int> temp = FindCount(GetFieldsMap().GetLaunchAnnounced());
            PrintFrequencyTable(ConvertToString(temp.Keys.ToList(), null), temp.Values.ToList(), field);
        }
        else if (field == "launch_status")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetLaunchStatus());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "body_dimensions")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetBodyDimensions());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "body_weight")
        {
            Dictionary<double, int> temp = FindCount(GetFieldsMap().GetBodyWeight());
            PrintFrequencyTable(ConvertToString(null, temp.Keys.ToList()), temp.Values.ToList(), field);
        }
        else if (field == "body_sim")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetBodySim());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "display_type")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetDisplayType());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "display_size")
        {
            Dictionary<double, int> temp = FindCount(GetFieldsMap().GetDisplaySize());
            PrintFrequencyTable(ConvertToString(null, temp.Keys.ToList()), temp.Values.ToList(), field);
        }
        else if (field == "display_resolution")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetDisplayResolution());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "features_sensors")
        {
            Dictionary<int, int> temp = FindCount(GetFieldsMap().GetFeaturesSensorsCount());
            PrintFrequencyTable(ConvertToString(temp.Keys.ToList(), null), temp.Values.ToList(), field);
        }
        else if (field == "platform_os")
        {
            Dictionary<string, int> temp = FindCount(GetFieldsMap().GetPlatformOs());
            PrintFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
    }

    /// <summary>
    /// This helper method is used to print the frequency table.
    /// </summary>
    /// <param name="element"> A unique column element </param>
    /// <param name="count"> The frequency of the element </param>
    /// <param name="field"> The field the elements belong to </param>
    private void PrintFrequencyTable(List<string> element, List<int> count, string field)
    {
        string header = String.Format("{0,-38}{1}", "unique_" + field + "_elements", "element_count  ");
        string border = CellRecords.TableBorder(header);
        Console.WriteLine(border);
        Console.WriteLine(header);
        Console.WriteLine(border);
        for (int i = 0; i < element.Count; i++)
        {
            string line = String.Format("{0,-38}{1}", element[i], count[i]);
            Console.WriteLine(line);
            Console.WriteLine(border);
        }
    }

    /// <summary>
    /// This helper method is used for the PrintAvgPerOemTable method (right now just intended to use for oem).
    /// </summary>
    /// <param name="values"> A dictionary with the oem names as keys and averages as values </param>
    /// <param name="sortField"> The field name to find averages for (per unique element) </param>
    /// <param name="avgField"> The field name to find averages of </param>
    private void PrintAvgTable(Dictionary<string, double> values, string sortField, string avgField)
    {
        string header = String.Format("{0,-38}{1}", "unique_" + sortField + "_elements", avgField + "_avg  ");
        string border = CellRecords.TableBorder(header);
        Console.WriteLine(border);
        Console.WriteLine(header);
        Console.WriteLine(border);
        foreach (string oem in values.Keys)
        {
            string line = String.Format("{0,-38}{1:0.00}", oem, values[oem]);
            Console.WriteLine(line);
            Console.WriteLine(border);
        }
    }

    /// <summary>
    /// This helper method converts numbers to strings for formatting.
    /// </summary>
    /// <param name="intList"> List of int numbers to convert </param>
    /// <param name="doubleList"> List of double numbers to convert </param>
    /// <returns> The converted list </returns>
    private List<string> ConvertToString(List<int> intList, List<double> doubleList)
    {
        if (intList != null)
        {
            return intList.ConvertAll<string>(x => x.ToString());
        }
        else
        {
            return doubleList.ConvertAll<string>(x => x.ToString());
        }
    }

    /// <summary>
    /// This method prints a table with averages for each company (oem) based on the given field. 
    /// </summary>
    /// <param name="avgField"> The field name to find averages of </param>
    /// <returns> The oem with the highest average </returns>
    public string PrintAvgPerOemTable(string avgField)
    {
        List<string> unique = FindCount(GetFieldsMap().GetOem()).Keys.ToList();
        Dictionary<string, double> avg = new Dictionary<string, double>();
        foreach (string oem in unique)
        {
            FilterParameters company = new FilterParameters();
            company.GetFilterString().Add("oem", new []{oem});
            CellTable temp = CreateQueryTable(company);
            if (avgField == "body_weight")
            {
                avg.Add(oem, temp.GetAvgBodyWeight());
            }
            else if (avgField == "display_size")
            {
                avg.Add(oem, temp.GetAvgDisplaySize());
            }
        }
        PrintAvgTable(avg, "oem", avgField);
        return FindHighestAvgOem(avg);
    }

    /// <summary>
    /// This helper method finds the oem with the highest average (from the avg table)
    /// </summary>
    /// <param name="temp"> A dictionary with the oem names as keys and averages as values </param>
    /// <returns> The oem with the highest average </returns>
    public string FindHighestAvgOem(Dictionary<string, double> temp)
    {
        return temp.Aggregate((x, y) =>
            x.Value > y.Value ? x : y).Key;
    }

    /// <summary>
    /// This method prints the row and column count of the table.
    /// </summary>
    public void PrintTableSize()
    {
        Console.WriteLine("\nThe table includes " + GetRecordsCount() + " records (rows)");
        Console.WriteLine("\nThe table includes " + (GetFieldsCount() - 2) + " fields (columns)");
    }

    /// <summary>
    /// This method prints stats for a table. Stats include the table size, and for each field
    /// the method displays the data type, the element with the highest mode (including the frequency),
    /// the number of unique elements, and if the field is numeric the average and median values.
    /// </summary>
    public void PrintTableStats()
    {
        string avg = "Average: ";
        string med = "Median: ";
        string mode = "Mode: ";
        string count = " (count: ";
        string unique = "Unique Elements: ";
        string header = "Table Statistics";
        string border1 = CellRecords.TableBorder(header);
        Console.WriteLine("\n" + header + "\n" + border1);
        PrintTableSize();
        Console.WriteLine();
        for (int i = 1; i < GetRecordsMap().GetFieldTitles().Count - 2; i++)
        {
            string columnHeader = "Stats for " + GetRecordsMap().GetFieldTitles()[i] + " field (Data Type: " + GetType(GetRecordsMap().GetFieldTitles()[i]) + ")";
            string border = CellRecords.TableBorder(columnHeader);
            Console.WriteLine("\n" + columnHeader + "\n" + border);
            if (GetRecordsMap().GetFieldTitles()[i] == "oem")
            {
                Console.WriteLine(mode + GetModeOem() + count + GetOemElementCount(GetModeOem()) + ")" );
                Console.WriteLine(unique + GetOemUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "model")
            {
                Console.WriteLine(mode + GetModeModel() + count + GetModelElementCount(GetModeModel()) + ")");
                Console.WriteLine(unique + GetModelUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "launch_announced")
            {
                Console.WriteLine(mode + GetModeLaunchAnnounced() + count + GetLaunchAnnouncedElementCount((int)GetModeLaunchAnnounced()) + ")");
                Console.WriteLine(unique + GetOemUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "launch_status")
            {
                Console.WriteLine(mode + GetModeLaunchStatus() + count + GetLaunchStatusElementCount(GetModeLaunchStatus()) + ")");
                Console.WriteLine(unique + GetLaunchStatusUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "body_dimensions")
            {
                Console.WriteLine(mode + GetModeBodyDimensions() + count + GetBodyDimensionsElementCount(GetModeBodyDimensions()) + ")");
                Console.WriteLine(unique + GetBodyDimensionsUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "body_weight")
            {
                Console.WriteLine(avg + String.Format("{0:0.00}", GetAvgBodyWeight()));
                Console.WriteLine(med + GetMedianBodyWeight());
                Console.WriteLine(mode + GetModeBodyWeight() + count + GetBodyWeightElementCount(GetModeBodyWeight()) + ")");
                Console.WriteLine(unique + GetBodyWeightUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "body_sim")
            {
                Console.WriteLine(mode + GetModeBodySim() + count + GetBodySimElementCount(GetModeBodySim()) + ")");
                Console.WriteLine(unique + GetBodySimUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "display_type")
            {
                Console.WriteLine(mode + GetModeDisplayType() + count + GetDisplayTypeElementCount(GetModeDisplayType()) + ")");
                Console.WriteLine(unique + GetDisplayTypeUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "display_size")
            {
                Console.WriteLine(avg + String.Format("{0:0.00}", GetAvgDisplaySize()));
                Console.WriteLine(med + GetMedianDisplaySize());
                Console.WriteLine(mode + GetModeDisplaySize() + count + GetDisplaySizeElementCount(GetModeDisplaySize()) + ")");
                Console.WriteLine(unique + GetDisplaySizeUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "display_resolution")
            {
                Console.WriteLine(mode + GetModeDisplayResolution() + count + GetDisplayResolutionElementCount(GetModeDisplayResolution()) + ")");
                Console.WriteLine(unique + GetDisplayResolutionUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "features_sensors")
            {
                Console.WriteLine(avg + String.Format("{0:0.00}", GetAvgFeaturesSensorsCount()));
                Console.WriteLine(med + GetMedianFeaturesSensorsCount());
                Console.WriteLine(mode + GetModeFeaturesSensorsCount() + count + GetFeaturesSensorsCountElementCount((int)GetModeFeaturesSensorsCount()) + ")");
                Console.WriteLine(unique + GetFeaturesSensorsUniqueCount());
            }
            else if (GetRecordsMap().GetFieldTitles()[i] == "platform_os")
            {
                Console.WriteLine(mode + GetModePlatformOs() + count + GetPlatformOsElementCount(GetModePlatformOs()) + ")");
                Console.WriteLine(unique + GetPlatformOsUniqueCount());
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// This method gets a string representation of the data type for a given field.
    /// </summary>
    /// <param name="field"> The field to get the type of </param>
    /// <returns> The data type </returns>
    public string GetType(string field)
    {
        string type;
        string temp = GetFieldsMap().GetCellTable()[field].ToString();
        if (temp.Contains("Int"))
        {
            type = "Integer";
        }
        else if (temp.Contains("Double"))
        {
            type = "Double";
        }
        else
        {
            type = "String";
        }
        return type;
    }

    /// <summary>
    /// This method creates a string representation of the entire table.
    /// </summary>
    /// <returns> The table string </returns>
    public string TableToString()
    {
        string temp = DefaultFieldsString();
        foreach (Cell record in this._recordsMap.GetCellTable().Values)
        {
            for (int i = 0; i < _recordsMap.GetFieldTitles().Count - 2; i++)
            {
                if (_recordsMap.GetFieldTitles()[i] != "year_of_launch" && _recordsMap.GetFieldTitles()[i] != "features_sensors_count")
                {
                    temp += ElementString(i, record);
                }
            }
            temp += "\n";
            temp += CellRecords.TableBorder(GetRecordsMap().HeadersToString());
        }
        return temp;
    }

    /// <summary>
    /// This method creates a string representation of a single record.
    /// </summary>
    /// <param name="record"> The record to use </param>
    /// <returns> The record string </returns>
    public string RecordString(Cell record)
    {
        string temp = DefaultFieldsString();
        for (int i = 0; i < _recordsMap.GetFieldTitles().Count - 2; i++)
        {
            if (_recordsMap.GetFieldTitles()[i] != "year_of_launch" && _recordsMap.GetFieldTitles()[i] != "features_sensors_count")
            {
                temp += ElementString(i, record);
            }
        }
        temp += "\n";
        temp += CellRecords.TableBorder(GetRecordsMap().HeadersToString());
        return temp;
    }

    /// <summary>
    /// This method creates a string representation of a single record with the given fields.
    /// </summary>
    /// <param name="record"> The record to use </param>
    /// <param name="fields"> The fields to include </param>
    /// <returns> The custom record string </returns>
    public string CustomRecordString(Cell record, string[] fields)
    {
        string temp = "";
        string border = CellRecords.TableBorder(GetRecordsMap().CustomHeadersToString(fields));
        string headers = GetRecordsMap().CustomHeadersToString(fields);
        temp += border + "\n";
        temp += headers + "\n";
        temp += border + "\n";
        foreach (string field in fields)
        {
            temp += ElementString(GetRecordsMap().GetFieldTitles().IndexOf(field), record);
        }
        temp += "\n";
        temp += border;
        return temp;
    }

    /// <summary>
    /// This method creates a string representation for multiple record with the given fields.
    /// </summary>
    /// <param name="records"> The records to use </param>
    /// <param name="fields"> The fields to include </param>
    /// <returns> The custom multiple record string </returns>
    public string CustomMultiRecordString(Cell[] records, string[] fields)
    {
        string temp = "";
        string border = CellRecords.TableBorder(GetRecordsMap().CustomHeadersToString(fields));
        string headers = GetRecordsMap().CustomHeadersToString(fields);
        temp += border + "\n";
        temp += headers + "\n";
        temp += border + "\n";
        foreach (Cell record in records)
        {
            foreach (string field in fields)
            {
                temp += ElementString(GetRecordsMap().GetFieldTitles().IndexOf(field), record);
            }
            temp += "\n";
            temp += border + "\n";
        }
        return temp;
    }

    /// <summary>
    /// This method creates a string representation for multiple records.
    /// </summary>
    /// <param name="records"> The records to use </param>
    /// <returns> The multiple record string </returns>
    public string MultiRecordString(Cell[] records)
    {
        string temp = DefaultFieldsString();
        foreach (Cell record in records)
        {
            for (int i = 0; i < _recordsMap.GetFieldTitles().Count - 2; i++)
            {
                if (_recordsMap.GetFieldTitles()[i] != "year_of_launch" && _recordsMap.GetFieldTitles()[i] != "features_sensors_count")
                {
                    temp += ElementString(i, record);
                }
            }
            temp += "\n";
            temp += CellRecords.TableBorder(GetRecordsMap().HeadersToString());
        }
        return temp;
    }

    /// <summary>
    /// This helper method get the string representation of all the field titles.
    /// </summary>
    /// <returns> The default field titles string </returns>
    public string DefaultFieldsString()
    {
        string temp = "";
        string border = CellRecords.TableBorder(GetRecordsMap().HeadersToString());
        string headers = GetRecordsMap().HeadersToString();
        temp += border;
        temp += headers + "\n";
        temp += border;
        return temp;
    }

    /// <summary>
    /// This method gets a (formatted) string for any element in the table.
    /// </summary>
    /// <param name="column"> The column index </param>
    /// <param name="row"> The row index </param>
    /// <returns> An element string </returns>
    public string ElementString(int column, Cell row)
    {
        string temp = "";
        if (column == 0)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-5}", "null");
            }
            else
            {
                temp += String.Format("{0,-5}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 1)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-14}", "null");
            }
            else
            {
                temp += String.Format("{0,-14}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 2)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-26}", "null");
            }
            else
            {
                temp += String.Format("{0,-26}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 3)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 4)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-14}", "null");
            }
            else
            {
                temp += String.Format("{0,-14}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 5)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-25}", "null");
            }
            else
            {
                temp += String.Format("{0,-25}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 6)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-12}", "null");
            }
            else
            {
                temp += String.Format("{0,-12}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 7)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 8)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-25}", "null");
            }
            else
            {
                temp += String.Format("{0,-25}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 9)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-13}", "null");
            }
            else
            {
                temp += String.Format("{0,-13}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 10)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-19}", "null");
            }
            else
            {
                temp += String.Format("{0,-19}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 11)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        if (column == 12)
        {
            if (row.GetCellMap()[_recordsMap.GetFieldTitles()[column]] == null)
            {
                temp += String.Format("{0,-32}", "null");
            }
            else
            {
                temp += String.Format("{0,-32}", row.GetCellMap()[_recordsMap.GetFieldTitles()[column]].ToString());
            }
        }
        return temp;
    }

    /// <summary>
    /// This method creates a list of id numbers in sorted order using the given field.
    /// This is used to print a table sorted on any given field name. 
    /// </summary>
    /// <param name="field"> The field to sort on </param>
    /// <returns> The sorted id number list </returns>
    public int[] GetIdListSorted(string field)
    {
        if (field == "id")
        {
            List<int> temp = (List<int>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, int> temp2 = new Dictionary<int, int>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, int> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "oem")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "model")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "launch_announced")
        {
            List<int?> temp = (List<int?>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, int?> temp2 = new Dictionary<int, int?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int? item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, int?> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "launch_status")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "body_dimensions")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "body_weight")
        {
            List<double?> temp = (List<double?>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, double?> temp2 = new Dictionary<int, double?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (double? item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, double?> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "body_sim")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "display_type")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }

            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }

            return temp3.ToArray();
        }
        else if (field == "display_size")
        {
            List<double?> temp = (List<double?>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, double?> temp2 = new Dictionary<int, double?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (double? item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }

            foreach (KeyValuePair<int, double?> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "display_resolution")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "features_sensors")
        {
            List<int?> temp = (List<int?>)GetFieldsMap().GetCellTable()["features_sensors_count"];
            Dictionary<int, int?> temp2 = new Dictionary<int, int?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int? item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, int?> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        else if (field == "platform_os")
        {
            List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(GetFieldsMap().GetId()[i], item);
                i++;
            }
            foreach (KeyValuePair<int, string> item in temp2.OrderBy(key => key.Value))
            {
                temp3.Add(item.Key);
            }
            return temp3.ToArray();
        }
        return null;
    }

    /// <summary>
    /// This method deletes a given record from the Table (from both table representations).
    /// </summary>
    /// <param name="id"> The record id to delete </param>
    public void DeleteRecord(int id)
    {
        DeleteRecordFromRecordMap(id);
        DeleteRecordFromFieldMap(id);
    }

    /// <summary>
    /// This method deletes multiple records from the Table (from both table representations).
    /// </summary>
    /// <param name="id"> The record id list to delete </param>
    public void DeleteRecord(int[] id)
    {
        foreach (int record in id)
        {
            DeleteRecordFromRecordMap(record);
            DeleteRecordFromFieldMap(record);
        }
    }

    /// <summary>
    /// This method deletes a given record from the record map.
    /// </summary>
    /// <param name="id"> The record id to delete </param>
    private void DeleteRecordFromRecordMap(int id)
    {
        GetRecordsMap().GetCellTable().Remove(id);
    }

    /// <summary>
    /// This method deletes a given record from the field map.
    /// </summary>
    /// <param name="id"> The record id to delete </param>
    private void DeleteRecordFromFieldMap(int id)
    {
        int idIndex = GetFieldsMap().GetId().IndexOf(id);
        foreach (string title in GetFieldsMap().GetFieldTitles())
        {
            if (title == "id")
            {
                List<int> temp = (List<int>)GetFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "launch_announced" || title == "year_of_launch" || title == "features_sensors_count")
            {
                List<int?> temp = (List<int?>)GetFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_weight" || title == "display_size")
            {
                List<double?> temp = (List<double?>)GetFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
        }
    }

    /// <summary>
    /// This method adds a new record to the Table (to both table representations).
    /// </summary>
    /// <param name="oem"> The company name </param>
    /// <param name="model"> The _model of the phone</param>
    /// <param name="launch_announced"> The year the phone was announced </param>
    /// <param name="launch_status"> The year the phone was released (string) </param>
    /// <param name="body_dimensions"> The dimensions of the phone </param>
    /// <param name="body_weight"> The weight of the phone </param>
    /// <param name="body_sim"> The type of sim the phone uses </param>
    /// <param name="display_type"> The type of display the phone uses </param>
    /// <param name="display_size"> The size of the phones display </param>
    /// <param name="display_resolution"> The resolution of the phones display </param>
    /// <param name="features_sensors"> The sensors the phone uses </param>
    /// <param name="platform_os"> The operating system the phone uses </param>
    public void AddRecord(string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
        double? body_weight, string body_sim, string display_type, double? display_size, string display_resolution,
        string features_sensors, string platform_os)
    {
        AddRecordToRecordMap(oem, model, launch_announced, launch_status, body_dimensions,
            body_weight, body_sim, display_type, display_size, display_resolution, features_sensors, platform_os);
        AddRecordToFieldMap(oem, model, launch_announced, launch_status, body_dimensions,
            body_weight, body_sim, display_type, display_size, display_resolution, features_sensors, platform_os);
        IncrementNextId();
    }

    /// <summary>
    /// This method adds a new record to the record map.
    /// </summary>
    /// <param name="oem"> The company name </param>
    /// <param name="model"> The _model of the phone</param>
    /// <param name="launch_announced"> The year the phone was announced </param>
    /// <param name="launch_status"> The year the phone was released (string) </param>
    /// <param name="body_dimensions"> The dimensions of the phone </param>
    /// <param name="body_weight"> The weight of the phone </param>
    /// <param name="body_sim"> The type of sim the phone uses </param>
    /// <param name="display_type"> The type of display the phone uses </param>
    /// <param name="display_size"> The size of the phones display </param>
    /// <param name="display_resolution"> The resolution of the phones display </param>
    /// <param name="features_sensors"> The sensors the phone uses </param>
    /// <param name="platform_os"> The operating system the phone uses </param>
    private void AddRecordToRecordMap(string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
        double? body_weight, string body_sim, string display_type, double? display_size, string display_resolution,
        string features_sensors, string platform_os)
    {
        int? features_sensors_count;
        if (features_sensors != null)
        {
            features_sensors_count = features_sensors.Split(",").ToList().Count;
        }
        else
        {
            features_sensors_count = null;
        }
        GetRecordsMap().GetCellTable().Add(GetNextId(), new Cell(GetNextId(),oem,model,launch_announced,launch_status,body_dimensions,
            body_weight,body_sim,display_type,display_size,display_resolution,features_sensors_count,platform_os,GetRecordsMap().GetFieldTitles()));
    }

    /// <summary>
    /// This method adds a new record to the field map.
    /// </summary>
    /// <param name="oem"> The company name </param>
    /// <param name="model"> The _model of the phone</param>
    /// <param name="launch_announced"> The year the phone was announced </param>
    /// <param name="launch_status"> The year the phone was released (string) </param>
    /// <param name="body_dimensions"> The dimensions of the phone </param>
    /// <param name="body_weight"> The weight of the phone </param>
    /// <param name="body_sim"> The type of sim the phone uses </param>
    /// <param name="display_type"> The type of display the phone uses </param>
    /// <param name="display_size"> The size of the phones display </param>
    /// <param name="display_resolution"> The resolution of the phones display </param>
    /// <param name="features_sensors"> The sensors the phone uses </param>
    /// <param name="platform_os"> The operating system the phone uses </param>
    private void AddRecordToFieldMap(string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
        double? body_weight, string body_sim, string display_type, double? display_size, string display_resolution,
        string features_sensors, string platform_os)
    {
        foreach (string title in GetFieldsMap().GetFieldTitles())
        {
            if (title == "id")
            {
                List<int> temp = (List<int>)GetFieldsMap().GetCellTable()[title];
                temp.Add(GetNextId());
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "oem")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(oem);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "model")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(model);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "launch_announced")
            {
                List<int?> temp = (List<int?>)GetFieldsMap().GetCellTable()[title];
                temp.Add(launch_announced);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "launch_status")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(launch_status);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_dimensions")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(body_dimensions);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_weight")
            {
                List<double?> temp = (List<double?>)GetFieldsMap().GetCellTable()[title];
                temp.Add(body_weight);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_sim")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(body_sim);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "display_type")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(display_type);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "display_size")
            {
                List<double?> temp = (List<double?>)GetFieldsMap().GetCellTable()[title];
                temp.Add(display_size);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "display_resolution")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(display_resolution);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "features_sensors")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(features_sensors);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "platform_os")
            {
                List<string> temp = (List<string>)GetFieldsMap().GetCellTable()[title];
                temp.Add(platform_os);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "features_sensors_count")
            {
                int? features_sensors_count;
                if (features_sensors != null)
                {
                    features_sensors_count = features_sensors.Split(",").ToList().Count;
                }
                else
                {
                    features_sensors_count = null;
                }
                List<int?> temp = (List<int?>)GetFieldsMap().GetCellTable()[title];
                temp.Add(features_sensors_count);
                GetFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "year_of_launch")
            {
                List<int?> temp = (List<int?>)GetFieldsMap().GetCellTable()[title];
                int i = 0;
                if (int.TryParse(launch_status,out i))
                {
                    temp.Add(int.Parse(launch_status));
                }
                else
                {
                    temp.Add(null);
                }
                GetFieldsMap().GetCellTable()[title] = temp;
            }
        }
    }

    /// <summary>
    /// This method calculates the avg of a list of numbers.
    /// </summary>
    /// <param name="nums"> The numbers to average </param>
    /// <returns> The average value </returns>
    public double CalcAvg(List<double?> nums)
    {
        double sum = 0;
        int count = 0;
        foreach (double? num in nums)
        {
            if (num != null)
            {
                sum += (double)num;
                count++;
            }
        }
        return sum / count;
    }

    /// <summary>
    /// This method calculates the median of a list of numbers.
    /// </summary>
    /// <param name="nums"> The numbers to find the median of </param>
    /// <returns> The median value  </returns>
    public double CalcMedian(List<double?> nums)
    {
        List<double> temp = new List<double>();
        foreach (double? num in nums)
        {
            if (num != null)
            {
                temp.Add((double)num);
            }
        }
        temp.Sort();
        double median;
        if (temp.Count % 2 == 1)
        {
            median = temp[temp.Count / 2];
        }
        else
        {
            double m1 = temp[(temp.Count / 2) - 1];
            double m2 = temp[temp.Count / 2];
            median = (m1 + m2) / 2;
        }
        return median;
    }

    /// <summary>
    /// This method calculates the mode of a list of numbers.
    /// </summary>
    /// <param name="nums"> The numbers to find the mode of </param>
    /// <returns> The mode value </returns>
    public double CalcMode(List<double?> nums)
    {
        Dictionary<double,int> temp = new Dictionary<double, int>();
        foreach (double? num in nums)
        {
            if (num != null)
            {
                if (temp.ContainsKey((double)num))
                {
                    temp[(double)num]++;
                }
                else
                {
                    temp.Add((double)num, 1);
                }
            }
        }
        return temp.Aggregate((x, y) => 
            x.Value > y.Value ? x : y).Key;
    }

    /// <summary>
    /// This method calculates the mode of a list of strings.
    /// </summary>
    /// <param name="elements"> The strings to find the mode of </param>
    /// <returns> The mode string </returns>
    public string CalcMode(List<string> elements)
    {
        Dictionary<string, int> temp = new Dictionary<string, int>();
        foreach (string element in elements)
        {
            if (element != null)
            {
                if (temp.ContainsKey(element))
                {
                    temp[element]++;
                }
                else
                {
                    temp.Add(element, 1);
                }
            }
        }
        return temp.Aggregate((x, y) =>
            x.Value > y.Value ? x : y).Key;
    }

    /// <summary>
    /// This method finds the frequency count for all unique int numbers in a list.
    /// </summary>
    /// <param name="nums"> The numbers to find the count of </param>
    /// <returns> A dictionary with elements as keys and counts as values </returns>
    public Dictionary<int, int> FindCount(List<int?> nums)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        foreach (double? num in nums)
        {
            if (num != null)
            {
                if (temp.ContainsKey((int)num))
                {
                    temp[(int)num]++;
                }
                else
                {
                    temp.Add((int)num, 1);
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// This method finds the frequency count for all unique strings in a list.
    /// </summary>
    /// <param name="elements"> The strings to find the count of </param>
    /// <returns> A dictionary with elements as keys and counts as values </returns>
    public Dictionary<string, int> FindCount(List<string> elements)
    {
        Dictionary<string, int> temp = new Dictionary<string, int>();
        foreach (string element in elements)
        {
            if (element != null)
            {
                if (temp.ContainsKey(element))
                {
                    temp[element]++;
                }
                else
                {
                    temp.Add(element, 1);
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// This method finds the frequency count for all unique double numbers in a list.
    /// </summary>
    /// <param name="nums"> The numbers to find the count of </param>
    /// <returns> A dictionary with elements as keys and counts as values </returns>
    public Dictionary<double, int> FindCount(List<double?> nums)
    {
        Dictionary<double, int> temp = new Dictionary<double, int>();
        foreach (double? num in nums)
        {
            if (num != null)
            {
                if (temp.ContainsKey((double)num))
                {
                    temp[(double)num]++;
                }
                else
                {
                    temp.Add((double)num, 1);
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// This method gets the average body weight.
    /// </summary>
    /// <returns> The average body weight.</returns>
    public double GetAvgBodyWeight()
    {
        return CalcAvg(GetFieldsMap().GetBodyWeight());
    }

    /// <summary>
    /// This method gets the average display size.
    /// </summary>
    /// <returns> The average display size </returns>
    public double GetAvgDisplaySize()
    {
        return CalcAvg(GetFieldsMap().GetDisplaySize());
    }

    /// <summary>
    /// This method gets the average features sensors count.
    /// </summary>
    /// <returns> The average features sensors count </returns>
    public double GetAvgFeaturesSensorsCount()
    {
        List<double?> temp = GetFieldsMap().GetFeaturesSensorsCount().ConvertAll(x => (double?)x);
        return CalcAvg(temp);
    }

    /// <summary>
    /// This method gets the median body weight.
    /// </summary>
    /// <returns> The median body weight.</returns>
    public double GetMedianBodyWeight()
    {
        return CalcMedian(GetFieldsMap().GetBodyWeight());
    }

    /// <summary>
    /// This method gets the median display size.
    /// </summary>
    /// <returns> The median display size </returns>
    public double GetMedianDisplaySize()
    {
        return CalcMedian(GetFieldsMap().GetDisplaySize());
    }

    /// <summary>
    /// This method gets the median features sensors count.
    /// </summary>
    /// <returns> The median features sensors count </returns>
    public double GetMedianFeaturesSensorsCount()
    {
        List<double?> temp = GetFieldsMap().GetFeaturesSensorsCount().ConvertAll(x => (double?)x);
        return CalcMedian(temp);
    }

    /// <summary>
    /// This method gets the mode of the body weight.
    /// </summary>
    /// <returns> The mode of the body weight </returns>
    public double GetModeBodyWeight()
    {
        return CalcMode(GetFieldsMap().GetBodyWeight());
    }

    /// <summary>
    /// This method gets the mode of the display size.
    /// </summary>
    /// <returns> The mode of the display size </returns>
    public double GetModeDisplaySize()
    {
        return CalcMode(GetFieldsMap().GetDisplaySize());
    }

    /// <summary>
    /// This method gets the mode of the features sensors count.
    /// </summary>
    /// <returns> The mode of the features sensors count </returns>
    public double GetModeFeaturesSensorsCount()
    {
        List<double?> temp = GetFieldsMap().GetFeaturesSensorsCount().ConvertAll(x => (double?)x);
        return CalcMode(temp);
    }

    /// <summary>
    /// This method gets the mode of launch announced.
    /// </summary>
    /// <returns> The mode of launch announced </returns>
    public double GetModeLaunchAnnounced()
    {
        List<double?> temp = GetFieldsMap().GetLaunchAnnounced().ConvertAll(x => (double?)x);
        return CalcMode(temp);
    }

    /// <summary>
    /// This method gets the mode of the launch status.
    /// </summary>
    /// <returns> The mode of the launch status </returns>
    public string GetModeLaunchStatus()
    {
        return CalcMode(GetFieldsMap().GetLaunchStatus());
    }

    /// <summary>
    /// This method gets the mode of the oem.
    /// </summary>
    /// <returns> The mode of the oem </returns>
    public string GetModeOem()
    {
        return CalcMode(GetFieldsMap().GetOem());
    }

    /// <summary>
    /// This method gets the mode of the model.
    /// </summary>
    /// <returns> The mode of the model </returns>
    public string GetModeModel()
    {
        return CalcMode(GetFieldsMap().GetModel());
    }

    /// <summary>
    /// This method gets the mode of the body dimensions.
    /// </summary>
    /// <returns> The mode of the body dimensions </returns>
    public string GetModeBodyDimensions()
    {
        return CalcMode(GetFieldsMap().GetBodyDimensions());
    }

    /// <summary>
    /// This method gets the mode of the body sim.
    /// </summary>
    /// <returns> The mode of the body sim </returns>
    public string GetModeBodySim()
    {
        return CalcMode(GetFieldsMap().GetBodySim());
    }

    /// <summary>
    /// This method gets the mode of the display type.
    /// </summary>
    /// <returns> The mode of the display type </returns>
    public string GetModeDisplayType()
    {
        return CalcMode(GetFieldsMap().GetDisplayType());
    }

    /// <summary>
    /// This method gets the mode of the display resolution.
    /// </summary>
    /// <returns> The mode of the display resolution </returns>
    public string GetModeDisplayResolution()
    {
        return CalcMode(GetFieldsMap().GetDisplayResolution());
    }

    /// <summary>
    /// This method gets the mode of the platform os.
    /// </summary>
    /// <returns> The mode of the platform os </returns>
    public string GetModePlatformOs()
    {
        return CalcMode(GetFieldsMap().GetPlatformOs());
    }

    /// <summary>
    /// This method gets the count of the given item for body weight.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetBodyWeightElementCount(double item)
    {
        Dictionary<double,int> temp = FindCount(GetFieldsMap().GetBodyWeight());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for display size.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetDisplaySizeElementCount(double item)
    {
        Dictionary<double, int> temp = FindCount(GetFieldsMap().GetDisplaySize());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for the features sensors count.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetFeaturesSensorsCountElementCount(int item)
    {
        Dictionary<int, int> temp = FindCount(GetFieldsMap().GetFeaturesSensorsCount());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for launced announced.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetLaunchAnnouncedElementCount(int item)
    {
        Dictionary<int, int> temp = FindCount(GetFieldsMap().GetLaunchAnnounced());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for launch status.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetLaunchStatusElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetLaunchStatus());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for oem.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetOemElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetOem());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for model.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetModelElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetModel());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for body dimensions.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetBodyDimensionsElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetBodyDimensions());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for body sim.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetBodySimElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetBodySim());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for display type.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetDisplayTypeElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetDisplayType());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for display resolution.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetDisplayResolutionElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetDisplayResolution());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the given item for platform os.
    /// </summary>
    /// <param name="item"> The item to count </param>
    /// <returns> The value of the count </returns>
    public int GetPlatformOsElementCount(string item)
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetPlatformOs());
        return temp[item];
    }

    /// <summary>
    /// This method gets the count of the rows in the table.
    /// </summary>
    /// <returns> The row count </returns>
    public int GetRecordsCount()
    {
        return _recordsMap.GetCellTable().Count;
    }

    /// <summary>
    /// This method gets the count of the columns in the table.
    /// </summary>
    /// <returns> The column count </returns>
    public int GetFieldsCount()
    {
        return _recordsMap.GetFieldTitles().Count;
    }

    /// <summary>
    /// This method gets the count of unique items from body weight.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetBodyWeightUniqueCount()
    {
        Dictionary<double, int> temp = FindCount(GetFieldsMap().GetBodyWeight());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from display size.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetDisplaySizeUniqueCount()
    {
        Dictionary<double, int> temp = FindCount(GetFieldsMap().GetDisplaySize());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from feature sensors.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetFeaturesSensorsUniqueCount()
    {
        Dictionary<int, int> temp = FindCount(GetFieldsMap().GetFeaturesSensorsCount());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from launch announced.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetLaunchAnnouncedUniqueCount()
    {
        Dictionary<int, int> temp = FindCount(GetFieldsMap().GetLaunchAnnounced());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from launch status.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetLaunchStatusUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetLaunchStatus());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from oem.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetOemUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetOem());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from model.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetModelUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetModel());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from body dimensions.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetBodyDimensionsUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetBodyDimensions());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from body sim.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetBodySimUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetBodySim());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from display type.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetDisplayTypeUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetDisplayType());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from display resolution.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetDisplayResolutionUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetDisplayResolution());
        return temp.Count;
    }

    /// <summary>
    /// This method gets the count of unique items from platform os.
    /// </summary>
    /// <returns> The value of the count </returns>
    public int GetPlatformOsUniqueCount()
    {
        Dictionary<string, int> temp = FindCount(GetFieldsMap().GetPlatformOs());
        return temp.Count;
    }

    /// <summary>
    /// This method is used to create a new Copy of an existing CellTable object.
    /// </summary>
    /// <returns> The CellTable Copy </returns>
    public CellTable Copy()
    {
        return new CellTable(this._filePath);  
    }

    /// <summary>
    /// This method is used to create a new (query) table that is filtered based on the given parameters.
    /// </summary>
    /// <param name="filter"> The filter parameters </param>
    /// <returns> A new query table </returns>
    public CellTable CreateQueryTable(FilterParameters filter)
    {
        CellTable temp = Copy();
        temp.updateTableWhere(filter);
        return temp;
    }

    /// <summary>
    /// This helper method updates a table to represent the given filter parameters.
    /// </summary>
    /// <param name="filter"> The filter parameters </param>
    public void updateTableWhere(FilterParameters filter)
    {
        if (filter.GetFilterString().Count != 0)
        {
            foreach (string field in filter.GetFilterString().Keys)
            {
                if (field == "oem")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetOem().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
                else if (field == "model")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetModel().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
                else if (field == "launch_status")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
                else if (field == "body_dimensions")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetBodyDimensions().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
                else if (field == "body_sim")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetBodySim().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
                else if (field == "display_type")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetDisplayType().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
                else if (field == "display_resolution")
                {
                    foreach (int id in GetRecordsMap().GetCellTable().Keys)
                    {
                        bool found = false;
                        foreach (string item in filter.GetFilterString()[field])
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetDisplayResolution().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            DeleteRecord(id);
                        }
                    }
                }
            }
        }
        if (filter.GetFilterInt().Count != 0 || filter.GetFilterIntRange().Count != 0)
        {
            string[] keys;
            if (filter.GetFilterInt().Count > 0)
            {
                keys = filter.GetFilterInt().Keys.ToArray();
            }
            else
            {
                keys = filter.GetFilterIntRange().Keys.ToArray();
            }
            foreach (string field in keys)
            {
                if (field == "id")
                {
                    if (filter.GetFilterInt().Count > 0)
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = false;
                            foreach (int item in filter.GetFilterInt()[field])
                            {
                                if (GetRecordsMap().GetCellTable()[id].GetId().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = GetRecordsMap().GetCellTable()[id].GetId() >= filter.GetFilterIntRange()[field].Key &&
                                         GetRecordsMap().GetCellTable()[id].GetId() <= filter.GetFilterIntRange()[field].Value;
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                }
                else if (field == "launch_announced")
                {
                    if (filter.GetFilterInt().Count > 0)
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = false;
                            foreach (int item in filter.GetFilterInt()[field])
                            {
                                if (GetRecordsMap().GetCellTable()[id].GetLaunchAnnounced().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = GetRecordsMap().GetCellTable()[id].GetLaunchAnnounced() >= filter.GetFilterIntRange()[field].Key &&
                                         GetRecordsMap().GetCellTable()[id].GetLaunchAnnounced() <= filter.GetFilterIntRange()[field].Value;
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                }
                else if (field == "launch_status")
                {
                    if (filter.GetFilterInt().Count > 0)
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals("Discontinued") || GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals("Cancelled"))
                            {
                                DeleteRecord(id);
                            }
                            else
                            {
                                bool found = false;
                                foreach (int item in filter.GetFilterInt()[field])
                                {
                                    if (int.Parse(GetRecordsMap().GetCellTable()[id].GetLaunchStatus()).Equals(item))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    DeleteRecord(id);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            if (GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals("Discontinued") || GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals("Cancelled"))
                            {
                                DeleteRecord(id);
                            }
                            else
                            {
                                bool found = int.Parse(GetRecordsMap().GetCellTable()[id].GetLaunchStatus()) >= filter.GetFilterIntRange()[field].Key &&
                                             int.Parse(GetRecordsMap().GetCellTable()[id].GetLaunchStatus()) <= filter.GetFilterIntRange()[field].Value;
                                if (!found)
                                {
                                    DeleteRecord(id);
                                }
                            }
                        }
                    }
                }
                else if (field == "features_sensors")
                {
                    if (filter.GetFilterInt().Count > 0)
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = false;
                            foreach (int item in filter.GetFilterInt()[field])
                            {
                                if (GetRecordsMap().GetCellTable()[id].GetFeaturesSensors().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = GetRecordsMap().GetCellTable()[id].GetFeaturesSensors() >= filter.GetFilterIntRange()[field].Key &&
                                         GetRecordsMap().GetCellTable()[id].GetFeaturesSensors() <= filter.GetFilterIntRange()[field].Value;
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                }
            }
        }
        if (filter.GetFilterDouble().Count != 0 || filter.GetFilterDoubleRange().Count != 0)
        {
            string[] keys;
            if (filter.GetFilterDouble().Count > 0)
            {
                keys = filter.GetFilterDouble().Keys.ToArray();
            }
            else
            {
                keys = filter.GetFilterDoubleRange().Keys.ToArray();
            }
            foreach (string field in keys)
            {
                if (field == "body_weight")
                {
                    if (filter.GetFilterDouble().Count > 0)
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = false;
                            foreach (int item in filter.GetFilterDouble()[field])
                            {
                                if (GetRecordsMap().GetCellTable()[id].GetBodyWeight().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = GetRecordsMap().GetCellTable()[id].GetBodyWeight() >= filter.GetFilterDoubleRange()[field].Key &&
                                         GetRecordsMap().GetCellTable()[id].GetBodyWeight() <= filter.GetFilterDoubleRange()[field].Value;
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                }
                else if (field == "display_size")
                {
                    if (filter.GetFilterDouble().Count > 0)
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = false;
                            foreach (int item in filter.GetFilterDouble()[field])
                            {
                                if (GetRecordsMap().GetCellTable()[id].GetDisplaySize().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in GetRecordsMap().GetCellTable().Keys)
                        {
                            bool found = GetRecordsMap().GetCellTable()[id].GetDisplaySize() >= filter.GetFilterDoubleRange()[field].Key &&
                                         GetRecordsMap().GetCellTable()[id].GetDisplaySize() <= filter.GetFilterDoubleRange()[field].Value;
                            if (!found)
                            {
                                DeleteRecord(id);
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// This method creates a query table where phones were launched later than they were announced.
    /// </summary>
    /// <returns> The new query table </returns>
    public CellTable GetPhonesLaunchedAfterAnnouncedTable()
    {
        CellTable temp = Copy();
        foreach (int id in GetRecordsMap().GetCellTable().Keys)
        {
            if (GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals("Discontinued") || GetRecordsMap().GetCellTable()[id].GetLaunchStatus().Equals("Cancelled"))
            {
                temp.DeleteRecord(id);
            }
            else if (GetRecordsMap().GetCellTable()[id].GetLaunchAnnounced() >=
                     int.Parse(GetRecordsMap().GetCellTable()[id].GetLaunchStatus()))
            {
                temp.DeleteRecord(id);
            }
        }
        return temp;
    }
}