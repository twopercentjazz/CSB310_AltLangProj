namespace AltLangProj.Classes;

public class CellTable
{
    private CellRecords recordsMap;
    private CellFields fieldsMap;
    private int nextId;
    private string filePath;


    public CellTable(string filePath)
    {
        ParseCsvFile parser = new ParseCsvFile(filePath);
        if (File.Exists(filePath) && new FileInfo(filePath).Length != 0)
        {
            CleanCellData cleanData = new CleanCellData(parser.getColumnData(), parser.getRowData()[0]);
            CellFields fields = new CellFields(cleanData.getCleanColumnData(), parser.getRowData());
            CellRecords records = new CellRecords(fields);
            this.recordsMap = records;
            this.fieldsMap = fields;
            this.nextId = getRecordsMap().get_cell_table().Count + 1;
            this.filePath = filePath;
        }
    }

    public CellTable(CellRecords records, CellFields fields, int nextId)
    {
        this.recordsMap = records;
        this.fieldsMap = fields;
        this.nextId = nextId;
    }


    public CellRecords getRecordsMap()
    {
        return this.recordsMap;
    }

    public void setRecordsMap(CellRecords recordsMap)
    {
        this.recordsMap = recordsMap;
    }

    public CellFields getFieldsMap()
    {
        return this.fieldsMap;
    }

    public void setFieldsMap(CellFields fieldsMap)
    {
        this.fieldsMap = fieldsMap;
    }

    public int getNextId()
    {
        return this.nextId;
    }

    public void setNextId(int nextId)
    {
        this.nextId = nextId;
    }

    public void incrementNextId()
    {
        this.nextId++;
    }

    /////////////////////////////////////////////////////////////////////////////////////////

    public void printCellTable()
    {
        Console.WriteLine(toString());
    }

    public void printCellTable(string field)
    {
        printMultipleRecords(getIdListSorted(field));
    }

    public void printRecord(int id)
    {
        Console.WriteLine(recordString(getRecordsMap().get_cell_table()[id]));
    }

    public void printMultipleRecords(int[] idList)
    {
        Cell[] temp = new Cell[idList.Length];
        int i = 0;
        foreach (int id in idList)
        {
            temp[i] = getRecordsMap().get_cell_table()[id];
            i++;
        }
        Console.WriteLine(multiRecordString(temp));
    }

    public void printCustomString(int id, string[] field)
    {
        Console.WriteLine(customRecordString(getRecordsMap().get_cell_table()[id], field));
    }

    public void printCustomMultipleRecords(int[] idList, string[] fields)
    {
        Cell[] temp = new Cell[idList.Length];
        int i = 0;
        foreach (int id in idList)
        {
            temp[i] = getRecordsMap().get_cell_table()[id];
            i++;
        }
        Console.WriteLine(customMultiRecordString(temp, fields));
    }

    public void printFeatureSensorList(int id)
    {
        int index = getFieldsMap().GetId().IndexOf(id);
        Console.WriteLine("Record " + id + " has the following feature sensors: " + getFieldsMap().GetFeaturesSensors()[index]);
    }

    public void printFrequencyTable(string field)
    {
        if (field == "oem")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetOem());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "model")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetModel());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "launch_announced")
        {
            Dictionary<int, int> temp = findCount(getFieldsMap().GetLaunchAnnounced());
            printFrequencyTable(convertToString(temp.Keys.ToList(), null), temp.Values.ToList(), field);
        }
        else if (field == "launch_status")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetLaunchStatus());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "body_dimensions")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetBodyDimensions());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "body_weight")
        {
            Dictionary<double, int> temp = findCount(getFieldsMap().GetBodyWeight());
            printFrequencyTable(convertToString(null, temp.Keys.ToList()), temp.Values.ToList(), field);
        }
        else if (field == "body_sim")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetBodySim());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "display_type")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetDisplayType());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "display_size")
        {
            Dictionary<double, int> temp = findCount(getFieldsMap().GetDisplaySize());
            printFrequencyTable(convertToString(null, temp.Keys.ToList()), temp.Values.ToList(), field);
        }
        else if (field == "display_resolution")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetDisplayResolution());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "features_sensors")
        {
            Dictionary<int, int> temp = findCount(getFieldsMap().GetFeaturesSensorsCount());
            printFrequencyTable(convertToString(temp.Keys.ToList(), null), temp.Values.ToList(), field);

        }
        else if (field == "platform_os")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().GetPlatformOs());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
    }

    private void printFrequencyTable(List<string> element, List<int> count, string field)
    {
        string header = String.Format("{0,-38}{1}", "unique_" + field + "_elements", "element_count  ");
        string border = getRecordsMap().tableBorder(header);
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

    private void printAvgTable(Dictionary<string, double> values, string sortField, string avgField)
    {
        string header = String.Format("{0,-38}{1}", "unique_" + sortField + "_elements", avgField + "_avg  ");
        string border = getRecordsMap().tableBorder(header);
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

    private List<string> convertToString(List<int> intList, List<double> doubleList)
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

    public string printAvgPerOem(string avgField)
    {
        
        List<string> unique = findCount(getFieldsMap().GetOem()).Keys.ToList();

        Dictionary<string, double> avg = new Dictionary<string, double>();

        foreach (string oem in unique)
        {
            FilterParameters company = new FilterParameters();
            company.getFilterString().Add("oem", new []{oem});
            CellTable temp = createQueryTable(company);
            if (avgField == "body_weight")
            {
                avg.Add(oem, temp.getAvgBodyWeight());
            }
            else if (avgField == "display_size")
            {
                avg.Add(oem, temp.getAvgDisplaySize());
            }
            
        }

        printAvgTable(avg, "oem", avgField);
        return findHighestAvgOem(avg);

    }

    public string findHighestAvgOem(Dictionary<string, double> temp)
    {
        return temp.Aggregate((x, y) =>
            x.Value > y.Value ? x : y).Key;
    }

    public void printTableSize()
    {
        Console.WriteLine("\nThe table includes " + getRecordsCount() + " records (rows)");
        Console.WriteLine("\nThe table includes " + (getFieldsCount() - 2) + " fields (columns)");
    }

    public void printTableStats()
    {
        string avg = "Average: ";
        string med = "Median: ";
        string mode = "Mode: ";
        string count = " (count: ";
        string unique = "Unique Elements: ";
        string header = "Table Statistics";
        string border1 = getRecordsMap().tableBorder(header);
        Console.WriteLine("\n" + header + "\n" + border1);
        printTableSize();
        Console.WriteLine();
        for (int i = 1; i < getRecordsMap().get_field_titles().Count - 2; i++)
        {
            string columnHeader = "Stats for " + getRecordsMap().get_field_titles()[i] + " field (Data Type: " + getType(getRecordsMap().get_field_titles()[i]) + ")";
            string border = getRecordsMap().tableBorder(columnHeader);
            Console.WriteLine("\n" + columnHeader + "\n" + border);
            if (getRecordsMap().get_field_titles()[i] == "oem")
            {
                Console.WriteLine(mode + getModeOem() + count + getOemElementCount(getModeOem()) + ")" );
                Console.WriteLine(unique + getOemUniqueCount());

            }
            else if (getRecordsMap().get_field_titles()[i] == "model")
            {
                Console.WriteLine(mode + getModeModel() + count + getModelElementCount(getModeModel()) + ")");
                Console.WriteLine(unique + getModelUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "launch_announced")
            {
                Console.WriteLine(mode + getModeLaunchAnnounced() + count + getLaunchAnnouncedElementCount((int)getModeLaunchAnnounced()) + ")");
                Console.WriteLine(unique + getOemUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "launch_status")
            {
                Console.WriteLine(mode + getModeLaunchStatus() + count + getLaunchStatusElementCount(getModeLaunchStatus()) + ")");
                Console.WriteLine(unique + getLaunchStatusUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "body_dimensions")
            {
                Console.WriteLine(mode + getModeBodyDimensions() + count + getBodyDimensionsElementCount(getModeBodyDimensions()) + ")");
                Console.WriteLine(unique + getBodyDimensionsUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "body_weight")
            {
                Console.WriteLine(avg + getAvgBodyWeight());
                Console.WriteLine(med + getMedianBodyWeight());
                Console.WriteLine(mode + getModeBodyWeight() + count + getBodyWeightElementCount(getModeBodyWeight()) + ")");
                Console.WriteLine(unique + getBodyWeightUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "body_sim")
            {
                Console.WriteLine(mode + getModeBodySim() + count + getBodySimElementCount(getModeBodySim()) + ")");
                Console.WriteLine(unique + getBodySimUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "display_type")
            {
                Console.WriteLine(mode + getModeDisplayType() + count + getDisplayTypeElementCount(getModeDisplayType()) + ")");
                Console.WriteLine(unique + getDisplayTypeUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "display_size")
            {
                Console.WriteLine(avg + getAvgDisplaySize());
                Console.WriteLine(med + getMedianDisplaySize());
                Console.WriteLine(mode + getModeDisplaySize() + count + getDisplaySizeElementCount(getModeDisplaySize()) + ")");
                Console.WriteLine(unique + getDisplaySizeUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "display_resolution")
            {
                Console.WriteLine(mode + getModeDisplayResolution() + count + getDisplayResolutionElementCount(getModeDisplayResolution()) + ")");
                Console.WriteLine(unique + getDisplayResolutionUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "features_sensors")
            {
                Console.WriteLine(avg + getAvgFeaturesSensorsCount());
                Console.WriteLine(med + getMedianFeaturesSensorsCount());
                Console.WriteLine(mode + getModeFeaturesSensorsCount() + count + getFeaturesSensorsCountElementCount((int)getModeFeaturesSensorsCount()) + ")");
                Console.WriteLine(unique + getFeaturesSensorsUniqueCount());
            }
            else if (getRecordsMap().get_field_titles()[i] == "platform_os")
            {
                Console.WriteLine(mode + getModePlatformOs() + count + getPlatformOsElementCount(getModePlatformOs()) + ")");
                Console.WriteLine(unique + getPlatformOsUniqueCount());
            }
            Console.WriteLine();
        }

        
    }

    public string getType(string field)
    {
        string type;
        string temp = getFieldsMap().GetCellTable()[field].ToString();
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

    public String toString()
    {
        string temp = defaultFieldsString();
        foreach (Cell record in this.recordsMap.get_cell_table().Values)
        {
            for (int i = 0; i < recordsMap.get_field_titles().Count - 2; i++)
            {
                if (recordsMap.get_field_titles()[i] != "year_of_launch" && recordsMap.get_field_titles()[i] != "features_sensors_count")
                {
                    temp += elementString(i, record);
                }
            }
            temp += "\n";
            temp += getRecordsMap().tableBorder(getRecordsMap().headersToString());
        }
        return temp;
    }

    public string recordString(Cell record)
    {
        string temp = defaultFieldsString();
        for (int i = 0; i < recordsMap.get_field_titles().Count - 2; i++)
        {
            if (recordsMap.get_field_titles()[i] != "year_of_launch" && recordsMap.get_field_titles()[i] != "features_sensors_count")
            {
                temp += elementString(i, record);
            }
        }
        temp += "\n";
        temp += getRecordsMap().tableBorder(getRecordsMap().headersToString());
        return temp;
    }

    public string customRecordString(Cell record, string[] fields)
    {
        string temp = "";
        string border = getRecordsMap().tableBorder(getRecordsMap().customHeadersToString(fields));
        string headers = getRecordsMap().customHeadersToString(fields);
        temp += border + "\n";
        temp += headers + "\n";
        temp += border + "\n";
        foreach (string field in fields)
        {
            temp += elementString(getRecordsMap().get_field_titles().IndexOf(field), record);
        }
        temp += "\n";
        temp += border;
        return temp;
    }

    public string customMultiRecordString(Cell[] records, string[] fields)
    {
        string temp = "";
        string border = getRecordsMap().tableBorder(getRecordsMap().customHeadersToString(fields));
        string headers = getRecordsMap().customHeadersToString(fields);
        temp += border + "\n";
        temp += headers + "\n";
        temp += border + "\n";
        foreach (Cell record in records)
        {
            foreach (string field in fields)
            {
                temp += elementString(getRecordsMap().get_field_titles().IndexOf(field), record);
                
            }
            temp += "\n";
            temp += border + "\n";
        }
        return temp;
    }

    public string multiRecordString(Cell[] records)
    {
        string temp = defaultFieldsString();
        foreach (Cell record in records)
        {
            for (int i = 0; i < recordsMap.get_field_titles().Count - 2; i++)
            {
                if (recordsMap.get_field_titles()[i] != "year_of_launch" && recordsMap.get_field_titles()[i] != "features_sensors_count")
                {
                    temp += elementString(i, record);
                }
            }
            temp += "\n";
            temp += getRecordsMap().tableBorder(getRecordsMap().headersToString());
        }
        return temp;
    }

    public string defaultFieldsString()
    {
        string temp = "";
        string border = getRecordsMap().tableBorder(getRecordsMap().headersToString());
        string headers = getRecordsMap().headersToString();
        temp += border;
        temp += headers + "\n";
        temp += border;
        return temp;
    }

    public string elementString(int column, Cell row)
    {
        string temp = "";
        if (column == 0)
        {

            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-5}", "null");
            }
            else
            {
                temp += String.Format("{0,-5}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 1)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-14}", "null");
            }
            else
            {
                temp += String.Format("{0,-14}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 2)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-26}", "null");
            }
            else
            {
                temp += String.Format("{0,-26}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 3)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 4)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-14}", "null");
            }
            else
            {
                temp += String.Format("{0,-14}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 5)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-25}", "null");
            }
            else
            {
                temp += String.Format("{0,-25}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 6)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-12}", "null");
            }
            else
            {
                temp += String.Format("{0,-12}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 7)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 8)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-25}", "null");
            }
            else
            {
                temp += String.Format("{0,-25}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 9)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-13}", "null");
            }
            else
            {
                temp += String.Format("{0,-13}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 10)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-19}", "null");
            }
            else
            {
                temp += String.Format("{0,-19}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 11)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 12)
        {
            if (row.GetCellMap()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-32}", "null");
            }
            else
            {
                temp += String.Format("{0,-32}", row.GetCellMap()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        return temp;
    }


    public int[] getIdListSorted(string field)
    {
        if (field == "id")
        {
            List<int> temp = (List<int>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, int> temp2 = new Dictionary<int, int>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<int?> temp = (List<int?>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, int?> temp2 = new Dictionary<int, int?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int? item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<double?> temp = (List<double?>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, double?> temp2 = new Dictionary<int, double?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (double? item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<double?> temp = (List<double?>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, double?> temp2 = new Dictionary<int, double?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (double? item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<int?> temp = (List<int?>)getFieldsMap().GetCellTable()["features_sensors_count"];
            Dictionary<int, int?> temp2 = new Dictionary<int, int?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int? item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().GetCellTable()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().GetId()[i], item);
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

    

    ///////////////////////////////////////////////////////////////////////////////////


    public void deleteRecord(int id)
    {
        deleteRecordFromRecordMap(id);
        deleteRecordFromFieldMap(id);
    }

    private void deleteRecordFromRecordMap(int id)
    {
        getRecordsMap().get_cell_table().Remove(id);
    }

    private void deleteRecordFromFieldMap(int id)
    {
        int idIndex = getFieldsMap().GetId().IndexOf(id);
        foreach (string title in getFieldsMap().GetFieldTitles())
        {
            if (title == "id")
            {
                List<int> temp = (List<int>)getFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "launch_announced" || title == "year_of_launch" || title == "features_sensors_count")
            {
                List<int?> temp = (List<int?>)getFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_weight" || title == "display_size")
            {
                List<double?> temp = (List<double?>)getFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().GetCellTable()[title] = temp;
            }
        }
    }

    public void addRecord(string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
        double? body_weight, string body_sim, string display_type, double? display_size, string display_resolution,
        string features_sensors, string platform_os)
    {
        addRecordToRecordMap(oem, model, launch_announced, launch_status, body_dimensions,
            body_weight, body_sim, display_type, display_size, display_resolution, features_sensors, platform_os);
        addRecordToFieldMap(oem, model, launch_announced, launch_status, body_dimensions,
            body_weight, body_sim, display_type, display_size, display_resolution, features_sensors, platform_os);
        incrementNextId();
    }

    private void addRecordToRecordMap(string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
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
        getRecordsMap().get_cell_table().Add(getNextId(), new Cell(getNextId(),oem,model,launch_announced,launch_status,body_dimensions,
            body_weight,body_sim,display_type,display_size,display_resolution,features_sensors_count,platform_os,getRecordsMap().get_field_titles()));
    }

    private void addRecordToFieldMap(string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
        double? body_weight, string body_sim, string display_type, double? display_size, string display_resolution,
        string features_sensors, string platform_os)
    {
        foreach (string title in getFieldsMap().GetFieldTitles())
        {
            if (title == "id")
            {
                List<int> temp = (List<int>)getFieldsMap().GetCellTable()[title];
                temp.Add(getNextId());
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "oem")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(oem);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "model")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(model);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "launch_announced")
            {
                List<int?> temp = (List<int?>)getFieldsMap().GetCellTable()[title];
                temp.Add(launch_announced);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "launch_status")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(launch_status);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_dimensions")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(body_dimensions);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_weight")
            {
                List<double?> temp = (List<double?>)getFieldsMap().GetCellTable()[title];
                temp.Add(body_weight);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "body_sim")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(body_sim);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "display_type")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(display_type);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "display_size")
            {
                List<double?> temp = (List<double?>)getFieldsMap().GetCellTable()[title];
                temp.Add(display_size);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "display_resolution")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(display_resolution);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "features_sensors")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(features_sensors);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "platform_os")
            {
                List<string> temp = (List<string>)getFieldsMap().GetCellTable()[title];
                temp.Add(platform_os);
                getFieldsMap().GetCellTable()[title] = temp;
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
                List<int?> temp = (List<int?>)getFieldsMap().GetCellTable()[title];
                temp.Add(features_sensors_count);
                getFieldsMap().GetCellTable()[title] = temp;
            }
            else if (title == "year_of_launch")
            {
                List<int?> temp = (List<int?>)getFieldsMap().GetCellTable()[title];
                int i = 0;
                if (int.TryParse(launch_status,out i))
                {
                    temp.Add(int.Parse(launch_status));
                }
                else
                {
                    temp.Add(null);
                }
                getFieldsMap().GetCellTable()[title] = temp;
            }
        }
    }



    
    //////////////////////////////////////////////////////////////////////////////////////////
    



    private double calcAvg(List<double?> nums)
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

    private double calcMedian(List<double?> nums)
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

    private double calcMode(List<double?> nums)
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

    private string calcMode(List<string> elements)
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

    private Dictionary<int, int> findCount(List<int?> nums)
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

    private Dictionary<string, int> findCount(List<string> elements)
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

    private Dictionary<double, int> findCount(List<double?> nums)
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



    public double getAvgBodyWeight()
    {
        return calcAvg(getFieldsMap().GetBodyWeight());
    }

    public double getAvgDisplaySize()
    {
        return calcAvg(getFieldsMap().GetDisplaySize());
    }

    public double getAvgFeaturesSensorsCount()
    {
        List<double?> temp = getFieldsMap().GetFeaturesSensorsCount().ConvertAll(x => (double?)x);
        return calcAvg(temp);
    }

    public double getMedianBodyWeight()
    {
        return calcMedian(getFieldsMap().GetBodyWeight());
    }

    public double getMedianDisplaySize()
    {
        return calcMedian(getFieldsMap().GetDisplaySize());
    }

    public double getMedianFeaturesSensorsCount()
    {
        List<double?> temp = getFieldsMap().GetFeaturesSensorsCount().ConvertAll(x => (double?)x);
        return calcMedian(temp);
    }

    public double getModeBodyWeight()
    {
        return calcMode(getFieldsMap().GetBodyWeight());
    }

    public double getModeDisplaySize()
    {
        return calcMode(getFieldsMap().GetDisplaySize());
    }

    public double getModeFeaturesSensorsCount()
    {
        List<double?> temp = getFieldsMap().GetFeaturesSensorsCount().ConvertAll(x => (double?)x);
        return calcMode(temp);
    }

    public double getModeLaunchAnnounced()
    {
        List<double?> temp = getFieldsMap().GetLaunchAnnounced().ConvertAll(x => (double?)x);
        return calcMode(temp);
    }

    public string getModeLaunchStatus()
    {
        //List<double?> temp = getFieldsMap().GetYearOfLaunch().ConvertAll(x => (double?)x);
        //return calcMode(temp);
        return calcMode(getFieldsMap().GetLaunchStatus());
    }

    public string getModeOem()
    {
        return calcMode(getFieldsMap().GetOem());
    }

    public string getModeModel()
    {
        return calcMode(getFieldsMap().GetModel());
    }

    public string getModeBodyDimensions()
    {
        return calcMode(getFieldsMap().GetBodyDimensions());
    }

    public string getModeBodySim()
    {
        return calcMode(getFieldsMap().GetBodySim());
    }

    public string getModeDisplayType()
    {
        return calcMode(getFieldsMap().GetDisplayType());
    }

    public string getModeDisplayResolution()
    {
        return calcMode(getFieldsMap().GetDisplayResolution());
    }

    public string getModePlatformOs()
    {
        return calcMode(getFieldsMap().GetPlatformOs());
    }

    public int getBodyWeightElementCount(double item)
    {
        Dictionary<double,int> temp = findCount(getFieldsMap().GetBodyWeight());
        return temp[item];
    }

    public int getDisplaySizeElementCount(double item)
    {
        Dictionary<double, int> temp = findCount(getFieldsMap().GetDisplaySize());
        return temp[item];
    }

    public int getFeaturesSensorsCountElementCount(int item)
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().GetFeaturesSensorsCount());
        return temp[item];
    }

    public int getLaunchAnnouncedElementCount(int item)
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().GetLaunchAnnounced());
        return temp[item];
    }

    public int getLaunchStatusElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetLaunchStatus());
        return temp[item];
    }

    public int getOemElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetOem());
        return temp[item];
    }

    public int getModelElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetModel());
        return temp[item];
    }

    public int getBodyDimensionsElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetBodyDimensions());
        return temp[item];
    }

    public int getBodySimElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetBodySim());
        return temp[item];
    }

    public int getDisplayTypeElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetDisplayType());
        return temp[item];
    }

    public int getDisplayResolutionElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetDisplayResolution());
        return temp[item];
    }

    public int getPlatformOsElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetPlatformOs());
        return temp[item];
    }



    public int getRecordsCount()
    {
        return recordsMap.get_cell_table().Count;
    }

    public int getFieldsCount()
    {
        return recordsMap.get_field_titles().Count;
    }


    public int getBodyWeightUniqueCount()
    {
        Dictionary<double, int> temp = findCount(getFieldsMap().GetBodyWeight());
        return temp.Count;
    }

    public int getDisplaySizeUniqueCount()
    {
        Dictionary<double, int> temp = findCount(getFieldsMap().GetDisplaySize());
        return temp.Count;
    }

    public int getFeaturesSensorsUniqueCount()
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().GetFeaturesSensorsCount());
        return temp.Count;
    }

    public int getLaunchAnnouncedUniqueCount()
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().GetLaunchAnnounced());
        return temp.Count;
    }

    public int getLaunchStatusUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetLaunchStatus());
        return temp.Count;
    }

    public int getOemUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetOem());
        return temp.Count;
    }

    public int getModelUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetModel());
        return temp.Count;
    }

    public int getBodyDimensionsUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetBodyDimensions());
        return temp.Count;
    }

    public int getBodySimUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetBodySim());
        return temp.Count;
    }

    public int getDisplayTypeUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetDisplayType());
        return temp.Count;
    }

    public int getDisplayResolutionUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetDisplayResolution());
        return temp.Count;
    }

    public int getPlatformOsUniqueCount()
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().GetPlatformOs());
        return temp.Count;
    }






    ///////////////////////////////////////////////////////////////////////////////////////

    public CellTable Copy(int flag)
    {
        return new CellTable(this.recordsMap.copy(), this.fieldsMap.Copy(), this.nextId);
    }

    public CellTable Copy()
    {
        return new CellTable(this.filePath);  
    }

    public CellTable createQueryTable(FilterParameters filter)
    {
        CellTable temp = Copy();
        temp.updateTableWhere(filter);
        return temp;
    }

    public void updateTableWhere(FilterParameters filter)
    {
        if (filter.getFilterString().Count != 0)
        {
            foreach (string field in filter.getFilterString().Keys)
            {
                if (field == "oem")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetOem().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }
                }


                else if (field == "model")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetModel().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }


                }

                else if (field == "launch_status")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }


                }
                else if (field == "body_dimensions")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetBodyDimensions().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }


                }

                else if (field == "body_sim")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetBodySim().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }


                }
                else if (field == "display_type")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetDisplayType().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }


                }
                else if (field == "display_resolution")
                {
                    foreach (int id in getRecordsMap().get_cell_table().Keys)
                    {
                        Boolean found = false;
                        foreach (string item in filter.getFilterString()[field])
                        {
                            if (getRecordsMap().get_cell_table()[id].GetDisplayResolution().Equals(item))
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            deleteRecord(id);
                        }
                    }

                }
            }


        }

        if (filter.getFilterInt().Count != 0 || filter.getFilterIntRange().Count != 0)
        {
            string[] keys;
            if (filter.getFilterInt().Count > 0)
            {
                keys = filter.getFilterInt().Keys.ToArray();
            }
            else
            {
                keys = filter.getFilterIntRange().Keys.ToArray();
            }
            foreach (string field in keys)
            {

                if (field == "id")
                {
                    if (filter.getFilterInt().Count > 0)
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;
                            foreach (int item in filter.getFilterInt()[field])
                            {
                                if (getRecordsMap().get_cell_table()[id].GetId().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;

                            if (getRecordsMap().get_cell_table()[id].GetId() >= filter.getFilterIntRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].GetId() <= filter.getFilterIntRange()[field].Value)
                            {
                                found = true;
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }

                        }
                    }
                }
                else if (field == "launch_announced")
                {
                    if (filter.getFilterInt().Count > 0)
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;
                            foreach (int item in filter.getFilterInt()[field])
                            {
                                if (getRecordsMap().get_cell_table()[id].GetLaunchAnnounced().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;

                            if (getRecordsMap().get_cell_table()[id].GetLaunchAnnounced() >= filter.getFilterIntRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].GetLaunchAnnounced() <= filter.getFilterIntRange()[field].Value)
                            {
                                found = true;
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }

                        }
                    }

                }
                else if (field == "launch_status")
                {
                    if (filter.getFilterInt().Count > 0)
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            
                            if (getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals("Discontinued") || getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals("Cancelled"))
                            {
                                deleteRecord(id);
                            }
                            else
                            {
                                Boolean found = false;
                                foreach (int item in filter.getFilterInt()[field])
                                {
                                    if (int.Parse(getRecordsMap().get_cell_table()[id].GetLaunchStatus()).Equals(item))
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    deleteRecord(id);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            
                            if (getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals("Discontinued") || getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals("Cancelled"))
                            {
                                deleteRecord(id);
                            }
                            else
                            {
                                Boolean found = false;

                                if (int.Parse(getRecordsMap().get_cell_table()[id].GetLaunchStatus()) >= filter.getFilterIntRange()[field].Key &&
                                    int.Parse(getRecordsMap().get_cell_table()[id].GetLaunchStatus()) <= filter.getFilterIntRange()[field].Value)
                                {
                                    found = true;
                                }
                                if (!found)
                                {
                                    deleteRecord(id);
                                }
                            }
                        }
                    }
                }
                else if (field == "features_sensors")
                {
                    if (filter.getFilterInt().Count > 0)
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;
                            foreach (int item in filter.getFilterInt()[field])
                            {
                                if (getRecordsMap().get_cell_table()[id].GetFeaturesSensors().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;

                            if (getRecordsMap().get_cell_table()[id].GetFeaturesSensors() >= filter.getFilterIntRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].GetFeaturesSensors() <= filter.getFilterIntRange()[field].Value)
                            {
                                found = true;
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }

                        }
                    }


                }

            }


        }

        if (filter.getFilterDouble().Count != 0 || filter.getFilterDoubleRange().Count != 0)
        {
            string[] keys;
            if (filter.getFilterDouble().Count > 0)
            {
                keys = filter.getFilterDouble().Keys.ToArray();
            }
            else
            {
                keys = filter.getFilterDoubleRange().Keys.ToArray();
            }

            foreach (string field in keys)
            {
                if (field == "body_weight")
                {
                    if (filter.getFilterDouble().Count > 0)
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;
                            foreach (int item in filter.getFilterDouble()[field])
                            {
                                if (getRecordsMap().get_cell_table()[id].GetBodyWeight().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;

                            if (getRecordsMap().get_cell_table()[id].GetBodyWeight() >= filter.getFilterDoubleRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].GetBodyWeight() <= filter.getFilterDoubleRange()[field].Value)
                            {
                                found = true;
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }

                        }
                    }


                }

                else if (field == "display_size")
                {
                    if (filter.getFilterDouble().Count > 0)
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;
                            foreach (int item in filter.getFilterDouble()[field])
                            {
                                if (getRecordsMap().get_cell_table()[id].GetDisplaySize().Equals(item))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }
                        }
                    }
                    else
                    {
                        foreach (int id in getRecordsMap().get_cell_table().Keys)
                        {
                            Boolean found = false;

                            if (getRecordsMap().get_cell_table()[id].GetDisplaySize() >= filter.getFilterDoubleRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].GetDisplaySize() <= filter.getFilterDoubleRange()[field].Value)
                            {
                                found = true;
                            }
                            if (!found)
                            {
                                deleteRecord(id);
                            }

                        }
                    }


                }
            }

        }

    }

    public CellTable getPhonesLaunchedAfterAnnouncedTable()
    {
        CellTable temp = Copy();
        foreach (int id in getRecordsMap().get_cell_table().Keys)
        {
            if (getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals("Discontinued") || getRecordsMap().get_cell_table()[id].GetLaunchStatus().Equals("Cancelled"))
            {
                temp.deleteRecord(id);
            }
            else if (getRecordsMap().get_cell_table()[id].GetLaunchAnnounced() >=
                     int.Parse(getRecordsMap().get_cell_table()[id].GetLaunchStatus()))
            {
                temp.deleteRecord(id);
            }
        }
        return temp;
    }





}