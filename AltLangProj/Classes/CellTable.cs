using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;

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



}