using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace AltLangProj.Classes;

public class CellTable
{
    private CellRecords recordsMap;
    private CellFields fieldsMap;
    private int nextId;


    public CellTable(string filePath)
    {
        ParseCsvFile parser = new ParseCsvFile(@"Input\cells.csv");
        CleanCellData cleanData = new CleanCellData(parser.getColumnData(), parser.getRowData()[0]);
        CellFields fields = new CellFields(cleanData.getCleanColumnData(), parser.getRowData());
        CellRecords records = new CellRecords(fields);
        this.recordsMap = records;
        this.fieldsMap = fields;
        this.nextId = getRecordsMap().get_cell_table().Count + 1;
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

    public void printFrequencyTable(string field)
    {
        if (field == "oem")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_oem());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "model")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_model());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "launch_announced")
        {
            Dictionary<int, int> temp = findCount(getFieldsMap().get_launch_announced());
            printFrequencyTable(convertToString(temp.Keys.ToList(), null), temp.Values.ToList(), field);
        }
        else if (field == "launch_status")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_launch_status());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "body_dimensions")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_body_dimensions());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "body_weight")
        {
            Dictionary<double, int> temp = findCount(getFieldsMap().get_body_weight());
            printFrequencyTable(convertToString(null, temp.Keys.ToList()), temp.Values.ToList(), field);
        }
        else if (field == "body_sim")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_body_sim());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "display_type")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_display_type());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "display_size")
        {
            Dictionary<double, int> temp = findCount(getFieldsMap().get_display_size());
            printFrequencyTable(convertToString(null, temp.Keys.ToList()), temp.Values.ToList(), field);
        }
        else if (field == "display_resolution")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_display_resolution());
            printFrequencyTable(temp.Keys.ToList(), temp.Values.ToList(), field);
        }
        else if (field == "features_sensors")
        {
            Dictionary<int, int> temp = findCount(getFieldsMap().get_features_sensors_count());
            printFrequencyTable(convertToString(temp.Keys.ToList(), null), temp.Values.ToList(), field);

        }
        else if (field == "platform_os")
        {
            Dictionary<string, int> temp = findCount(getFieldsMap().get_platform_os());
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
        
        List<string> unique = findCount(getFieldsMap().get_oem()).Keys.ToList();

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

            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-5}", "null");
            }
            else
            {
                temp += String.Format("{0,-5}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 1)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-14}", "null");
            }
            else
            {
                temp += String.Format("{0,-14}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 2)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-26}", "null");
            }
            else
            {
                temp += String.Format("{0,-26}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 3)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 4)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-14}", "null");
            }
            else
            {
                temp += String.Format("{0,-14}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 5)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-25}", "null");
            }
            else
            {
                temp += String.Format("{0,-25}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 6)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-12}", "null");
            }
            else
            {
                temp += String.Format("{0,-12}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 7)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 8)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-25}", "null");
            }
            else
            {
                temp += String.Format("{0,-25}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 9)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-13}", "null");
            }
            else
            {
                temp += String.Format("{0,-13}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 10)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-19}", "null");
            }
            else
            {
                temp += String.Format("{0,-19}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 11)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-17}", "null");
            }
            else
            {
                temp += String.Format("{0,-17}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        if (column == 12)
        {
            if (row.get_cell_map()[recordsMap.get_field_titles()[column]] == null)
            {
                temp += String.Format("{0,-32}", "null");
            }
            else
            {
                temp += String.Format("{0,-32}", row.get_cell_map()[recordsMap.get_field_titles()[column]].ToString());
            }
        }
        return temp;
    }


    public int[] getIdListSorted(string field)
    {
        if (field == "id")
        {
            List<int> temp = (List<int>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, int> temp2 = new Dictionary<int, int>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<int?> temp = (List<int?>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, int?> temp2 = new Dictionary<int, int?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int? item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<double?> temp = (List<double?>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, double?> temp2 = new Dictionary<int, double?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (double? item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<double?> temp = (List<double?>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, double?> temp2 = new Dictionary<int, double?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (double? item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<int?> temp = (List<int?>)getFieldsMap().get_cell_table()["features_sensors_count"];
            Dictionary<int, int?> temp2 = new Dictionary<int, int?>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (int? item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
            List<string> temp = (List<string>)getFieldsMap().get_cell_table()[field];
            Dictionary<int, string> temp2 = new Dictionary<int, string>();
            List<int> temp3 = new List<int>();
            int i = 0;
            foreach (string item in temp)
            {
                temp2.Add(getFieldsMap().get_id()[i], item);
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
        int idIndex = getFieldsMap().get_id().IndexOf(id);
        foreach (string title in getFieldsMap().get_field_titles())
        {
            if (title == "id")
            {
                List<int> temp = (List<int>)getFieldsMap().get_cell_table()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "launch_announced" || title == "year_of_launch" || title == "features_sensors_count")
            {
                List<int?> temp = (List<int?>)getFieldsMap().get_cell_table()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "body_weight" || title == "display_size")
            {
                List<double?> temp = (List<double?>)getFieldsMap().get_cell_table()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.RemoveAt(idIndex);
                getFieldsMap().get_cell_table()[title] = temp;
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
        foreach (string title in getFieldsMap().get_field_titles())
        {
            if (title == "id")
            {
                List<int> temp = (List<int>)getFieldsMap().get_cell_table()[title];
                temp.Add(getNextId());
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "oem")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(oem);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "model")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(model);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "launch_announced")
            {
                List<int?> temp = (List<int?>)getFieldsMap().get_cell_table()[title];
                temp.Add(launch_announced);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "launch_status")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(launch_status);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "body_dimensions")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(body_dimensions);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "body_weight")
            {
                List<double?> temp = (List<double?>)getFieldsMap().get_cell_table()[title];
                temp.Add(body_weight);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "body_sim")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(body_sim);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "display_type")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(display_type);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "display_size")
            {
                List<double?> temp = (List<double?>)getFieldsMap().get_cell_table()[title];
                temp.Add(display_size);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "display_resolution")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(display_resolution);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "features_sensors")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(features_sensors);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "platform_os")
            {
                List<string> temp = (List<string>)getFieldsMap().get_cell_table()[title];
                temp.Add(platform_os);
                getFieldsMap().get_cell_table()[title] = temp;
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
                List<int?> temp = (List<int?>)getFieldsMap().get_cell_table()[title];
                temp.Add(features_sensors_count);
                getFieldsMap().get_cell_table()[title] = temp;
            }
            else if (title == "year_of_launch")
            {
                List<int?> temp = (List<int?>)getFieldsMap().get_cell_table()[title];
                int i = 0;
                if (int.TryParse(launch_status,out i))
                {
                    temp.Add(int.Parse(launch_status));
                }
                else
                {
                    temp.Add(null);
                }
                getFieldsMap().get_cell_table()[title] = temp;
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
        return calcAvg(getFieldsMap().get_body_weight());
    }

    public double getAvgDisplaySize()
    {
        return calcAvg(getFieldsMap().get_display_size());
    }

    public double getAvgFeaturesSensorsCount()
    {
        List<double?> temp = getFieldsMap().get_features_sensors_count().ConvertAll(x => (double?)x);
        return calcAvg(temp);
    }

    public double getMedianBodyWeight()
    {
        return calcMedian(getFieldsMap().get_body_weight());
    }

    public double getMedianDisplaySize()
    {
        return calcMedian(getFieldsMap().get_display_size());
    }

    public double getMedianFeaturesSensorsCount()
    {
        List<double?> temp = getFieldsMap().get_features_sensors_count().ConvertAll(x => (double?)x);
        return calcMedian(temp);
    }

    public double getModeBodyWeight()
    {
        return calcMode(getFieldsMap().get_body_weight());
    }

    public double getModeDisplaySize()
    {
        return calcMode(getFieldsMap().get_display_size());
    }

    public double getModeFeaturesSensorsCount()
    {
        List<double?> temp = getFieldsMap().get_features_sensors_count().ConvertAll(x => (double?)x);
        return calcMode(temp);
    }

    public double getModeLaunchAnnounced()
    {
        List<double?> temp = getFieldsMap().get_launch_announced().ConvertAll(x => (double?)x);
        return calcMode(temp);
    }

    public double getModeLaunchStatus()
    {
        List<double?> temp = getFieldsMap().get_year_of_launch().ConvertAll(x => (double?)x);
        return calcMode(temp);
    }

    public string getModeOem()
    {
        return calcMode(getFieldsMap().get_oem());
    }

    public string getModeModel()
    {
        return calcMode(getFieldsMap().get_model());
    }

    public string getModeBodyDimensions()
    {
        return calcMode(getFieldsMap().get_body_dimensions());
    }

    public string getModeBodySim()
    {
        return calcMode(getFieldsMap().get_body_sim());
    }

    public string getModeDisplayType()
    {
        return calcMode(getFieldsMap().get_display_type());
    }

    public string getModeDisplayResolution()
    {
        return calcMode(getFieldsMap().get_display_resolution());
    }

    public string getModePlatformOs()
    {
        return calcMode(getFieldsMap().get_platform_os());
    }

    public int getBodyWeightElementCount(double item)
    {
        Dictionary<double,int> temp = findCount(getFieldsMap().get_body_weight());
        return temp[item];
    }

    public int getDisplaySizeElementCount(double item)
    {
        Dictionary<double, int> temp = findCount(getFieldsMap().get_display_size());
        return temp[item];
    }

    public int getFeaturesSensorsCountElementCount(int item)
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().get_features_sensors_count());
        return temp[item];
    }

    public int getLaunchAnnouncedElementCount(int item)
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().get_launch_announced());
        return temp[item];
    }

    public int getLaunchStatusElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_launch_status());
        return temp[item];
    }

    public int getOemElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_oem());
        return temp[item];
    }

    public int getModelElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_model());
        return temp[item];
    }

    public int getBodyDimensionsElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_body_dimensions());
        return temp[item];
    }

    public int getBodySimElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_body_sim());
        return temp[item];
    }

    public int getDisplayTypeElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_display_type());
        return temp[item];
    }

    public int getDisplayResolutionElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_display_resolution());
        return temp[item];
    }

    public int getPlatformOsElementCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_platform_os());
        return temp[item];
    }



    public int getRecordsCount()
    {
        return recordsMap.get_cell_table().Count;
    }

    public int getFieldsCount()
    {
        return fieldsMap.get_cell_table().Count;
    }


    public int getBodyWeightUniqueCount(double item)
    {
        Dictionary<double, int> temp = findCount(getFieldsMap().get_body_weight());
        return temp.Count;
    }

    public int getDisplaySizeUniqueCount(double item)
    {
        Dictionary<double, int> temp = findCount(getFieldsMap().get_display_size());
        return temp.Count;
    }

    public int getFeaturesSensorsUniqueCount(int item)
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().get_features_sensors_count());
        return temp.Count;
    }

    public int getLaunchAnnouncedUniqueCount(int item)
    {
        Dictionary<int, int> temp = findCount(getFieldsMap().get_launch_announced());
        return temp.Count;
    }

    public int getLaunchStatusUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_launch_status());
        return temp.Count;
    }

    public int getOemUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_oem());
        return temp.Count;
    }

    public int getModelUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_model());
        return temp.Count;
    }

    public int getBodyDimensionsUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_body_dimensions());
        return temp.Count;
    }

    public int getBodySimUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_body_sim());
        return temp.Count;
    }

    public int getDisplayTypeUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_display_type());
        return temp.Count;
    }

    public int getDisplayResolutionUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_display_resolution());
        return temp.Count;
    }

    public int getPlatformOsUniqueCount(string item)
    {
        Dictionary<string, int> temp = findCount(getFieldsMap().get_platform_os());
        return temp.Count;
    }






    ///////////////////////////////////////////////////////////////////////////////////////

    public CellTable Copy(int flag)
    {
        return new CellTable(this.recordsMap.copy(), this.fieldsMap.copy(), this.nextId);
    }

    public CellTable Copy()
    {
        return new CellTable(@"Input\cells.csv");
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
                            if (getRecordsMap().get_cell_table()[id].get_oem().Equals(item))
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
                            if (getRecordsMap().get_cell_table()[id].get_model().Equals(item))
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
                            if (getRecordsMap().get_cell_table()[id].get_launch_status().Equals(item))
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
                            if (getRecordsMap().get_cell_table()[id].get_body_dimensions().Equals(item))
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
                            if (getRecordsMap().get_cell_table()[id].get_body_sim().Equals(item))
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
                            if (getRecordsMap().get_cell_table()[id].get_display_type().Equals(item))
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
                            if (getRecordsMap().get_cell_table()[id].get_display_resolution().Equals(item))
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
                                if (getRecordsMap().get_cell_table()[id].get_id().Equals(item))
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

                            if (getRecordsMap().get_cell_table()[id].get_id() >= filter.getFilterIntRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].get_id() <= filter.getFilterIntRange()[field].Value)
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
                                if (getRecordsMap().get_cell_table()[id].get_launch_announced().Equals(item))
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

                            if (getRecordsMap().get_cell_table()[id].get_launch_announced() >= filter.getFilterIntRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].get_launch_announced() <= filter.getFilterIntRange()[field].Value)
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
                            
                            if (getRecordsMap().get_cell_table()[id].get_launch_status().Equals("Discontinued") || getRecordsMap().get_cell_table()[id].get_launch_status().Equals("Cancelled"))
                            {
                                deleteRecord(id);
                            }
                            else
                            {
                                Boolean found = false;
                                foreach (int item in filter.getFilterInt()[field])
                                {
                                    if (int.Parse(getRecordsMap().get_cell_table()[id].get_launch_status()).Equals(item))
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
                            
                            if (getRecordsMap().get_cell_table()[id].get_launch_status().Equals("Discontinued") || getRecordsMap().get_cell_table()[id].get_launch_status().Equals("Cancelled"))
                            {
                                deleteRecord(id);
                            }
                            else
                            {
                                Boolean found = false;

                                if (int.Parse(getRecordsMap().get_cell_table()[id].get_launch_status()) >= filter.getFilterIntRange()[field].Key &&
                                    int.Parse(getRecordsMap().get_cell_table()[id].get_launch_status()) <= filter.getFilterIntRange()[field].Value)
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
                                if (getRecordsMap().get_cell_table()[id].get_features_sensors().Equals(item))
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

                            if (getRecordsMap().get_cell_table()[id].get_features_sensors() >= filter.getFilterIntRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].get_features_sensors() <= filter.getFilterIntRange()[field].Value)
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
                                if (getRecordsMap().get_cell_table()[id].get_body_weight().Equals(item))
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

                            if (getRecordsMap().get_cell_table()[id].get_body_weight() >= filter.getFilterDoubleRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].get_body_weight() <= filter.getFilterDoubleRange()[field].Value)
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
                                if (getRecordsMap().get_cell_table()[id].get_display_size().Equals(item))
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

                            if (getRecordsMap().get_cell_table()[id].get_display_size() >= filter.getFilterDoubleRange()[field].Key &&
                                getRecordsMap().get_cell_table()[id].get_display_size() <= filter.getFilterDoubleRange()[field].Value)
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





}