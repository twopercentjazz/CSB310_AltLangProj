namespace AltLangProj.Classes;

public class CellRecords
{
    private Dictionary<int, Cell> _cellTable;
    private List<string> _fieldTitles;

    public CellRecords(CellFields cellTable)
    {
        this._fieldTitles = cellTable.GetFieldTitles();
        this._cellTable = CreateCellTable(cellTable);
    }

    public CellRecords(Dictionary<int, Cell> cellTable, List<string> fieldTitles)
    {
        this._fieldTitles = fieldTitles;
        this._cellTable = cellTable;
    }

    private Dictionary<int, Cell> CreateCellTable(CellFields cellTable)
    {
        Dictionary<int, Cell> temp = new Dictionary<int, Cell>();
        for (int i = 0; i < cellTable.GetId().Count; i++)
        {
            Cell record = new(cellTable.GetId()[i], cellTable.GetOem()[i], cellTable.GetModel()[i],
                cellTable.GetLaunchAnnounced()[i], cellTable.GetLaunchStatus()[i],
                cellTable.GetBodyDimensions()[i],
                cellTable.GetBodyWeight()[i], cellTable.GetBodySim()[i], cellTable.GetDisplayType()[i],
                cellTable.GetDisplaySize()[i], cellTable.GetDisplayResolution()[i],
                cellTable.GetFeaturesSensorsCount()[i],
                cellTable.GetPlatformOs()[i], cellTable.GetFieldTitles());
            temp.Add(i + 1, record);
        }
        return temp;
    }

    /// <summary>
    /// This method gets the Cell Table (organized by record).
    /// </summary>
    /// <returns> The Cell Table </returns>
    public Dictionary<int, Cell> GetCellTable()
    {
        return this._cellTable;
    }

    /// <summary>
    /// This method sets the Cell Table (organized by record).
    /// </summary>
    /// <param name="cellTable"> The new Cell Table </param>
    public void SetCellTable(Dictionary<int, Cell> cellTable)
    {
        this._cellTable = cellTable;
    }

    /// <summary>
    /// This method gets the list of field (column) titles for the table.
    /// </summary>
    /// <returns> The list of field titles </returns>
    public List<string> GetFieldTitles()
    {
        return this._fieldTitles;
    }

    /// <summary>
    /// This method sets the list of field (column) titles for the table.
    /// </summary>
    /// <param name="headers"> The new list of field titles </param>
    public void SetFieldTitles(List<string> headers)
    {
        this._fieldTitles = headers;
    }

    public string HeadersToString()
    {
        string temp = "";
        foreach (string title in GetFieldTitles())
        {
            if (title != "year_of_launch" && title != "features_sensors_count")
            {
                temp += TitleString(title);
            }
        }
        return temp;
    }

    public string CustomHeadersToString(string[] headersList)
    {
        string temp = "";
        foreach (string header in headersList)
        {
            temp += TitleString(header);
        }
        return temp;
    }

    public string TitleString(string title)
    {
        string temp = "";
        if (title == GetFieldTitles()[0])
        {
            temp += String.Format("{0,-5}", title);
        }
        if (title == GetFieldTitles()[1])
        {
            temp += String.Format("{0,-14}", title);
        }
        if (title == GetFieldTitles()[2])
        {
            temp += String.Format("{0,-26}", title);
        }
        if (title == GetFieldTitles()[3])
        {
            temp += String.Format("{0,-17}", title);
        }
        if (title == GetFieldTitles()[4])
        {
            temp += String.Format("{0,-14}", title);
        }
        if (title == GetFieldTitles()[5])
        {
            temp += String.Format("{0,-25}", title);
        }
        if (title == GetFieldTitles()[6])
        {
            temp += String.Format("{0,-12}", title);
        }
        if (title == GetFieldTitles()[7])
        {
            temp += String.Format("{0,-17}", title);
        }
        if (title == GetFieldTitles()[8])
        {
            temp += String.Format("{0,-25}", title);
        }
        if (title == GetFieldTitles()[9])
        {
            temp += String.Format("{0,-13}", title);
        }
        if (title == GetFieldTitles()[10])
        {
            temp += String.Format("{0,-19}", title);
        }
        if (title == GetFieldTitles()[11])
        {
            temp += String.Format("{0,-17}", title);
        }
        if (title == GetFieldTitles()[12])
        {
            temp += String.Format("{0,-32}", title);
        }
        return temp;
    }

    public string TableBorder(string s)
    {
        string temp = "";
        for (int i = 0; i < s.Length; i++)
        {
            temp += "-";
        }

        return temp;
    }

    /// <summary>
    /// This method is used to create a new Copy of an existing CellRecords object.
    /// </summary>
    /// <returns> The CellRecords Copy </returns>
    public CellRecords Copy()
    {
        return new CellRecords(_cellTable, _fieldTitles);
    }
}