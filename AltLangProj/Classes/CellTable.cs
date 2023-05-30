﻿using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;

namespace AltLangProj.Classes;

public class CellTable
{
    private CellRecords recordsMap;
    private CellFields fieldsMap;


    public CellTable(string filePath)
    {
        ParseCsvFile parser = new ParseCsvFile(@"Input\cells.csv");
        CleanCellData cleanData = new CleanCellData(parser.getColumnData(), parser.getRowData()[0]);
        CellFields fields = new CellFields(cleanData.getCleanColumnData(), parser.getRowData());
        CellRecords records = new CellRecords(fields);
        this.recordsMap = records;
        this.fieldsMap = fields;
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





}