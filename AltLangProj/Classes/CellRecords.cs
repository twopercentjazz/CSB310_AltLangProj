namespace AltLangProj.Classes;

/// <summary>
/// This class represents a row oriented version of the cell phone table. This class is used to build one of 
/// the two table representations used in the CellTable class.
/// 
/// Note: This class contains additional methods used to display properly aligned header titles and formatted
/// borders for printing console output. 
/// </summary>
public class CellRecords
{
    private Dictionary<int, Cell> _cellTable;
    private List<string> _fieldTitles;

    /// <summary>
    /// This Constructs a CellRecords object.
    /// </summary>
    /// <param name="cellTable"> A table with column titles as keys and the lists of column elements as values </param>
    public CellRecords(CellFields cellTable)
    {
        this._fieldTitles = cellTable.GetFieldTitles();
        this._cellTable = CreateCellTable(cellTable);
    }

    /// <summary>
    /// This Constructs a CellRecords object, and is used to construct a new Copy of an existing CellRecords object.
    /// </summary>
    /// <param name="cellTable"> A table with column titles as keys and the lists of column elements as values </param>
    /// <param name="fieldTitles"> List of all the column titles</param>
    public CellRecords(Dictionary<int, Cell> cellTable, List<string> fieldTitles)
    {
        this._fieldTitles = fieldTitles;
        this._cellTable = cellTable;
    }

    /// <summary>
    /// This method creates a table with id numbers as keys and records (Cell objects) as values.
    /// </summary>
    /// <param name="cellTable"></param>
    /// <returns> The Cell Table (for referencing each record by id number) </returns>
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

    /// <summary>
    /// This method creates a string of all the column titles (for displaying the table).
    /// </summary>
    /// <returns> The column titles </returns>
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

    /// <summary>
    /// This method creates a string of specific given column titles (for displaying the table).
    /// </summary>
    /// <param name="headersList"> A list of column titles to display </param>
    /// <returns> The column titles </returns>
    public string CustomHeadersToString(string[] headersList)
    {
        string temp = "";
        foreach (string header in headersList)
        {
            temp += TitleString(header);
        }
        return temp;
    }

    /// <summary>
    /// This method formats a column title name with specific alignment requirements (for displaying the table).
    /// </summary>
    /// <param name="title"> The column title to format </param>
    /// <returns> The formatted column title name </returns>
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

    /// <summary>
    /// This method creates a border with variable length (for displaying the table).
    /// </summary>
    /// <param name="s"> The string used to set the border size </param>
    /// <returns> The table border string </returns>
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