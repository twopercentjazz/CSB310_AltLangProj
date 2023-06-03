namespace AltLangProj.Classes;

public class CellRecords
{
    private Dictionary<int, Cell> cell_table;
    private List<string> field_titles;

    public CellRecords(CellFields cell_table)
    {
        this.field_titles = cell_table.GetFieldTitles();
        this.cell_table = create_cell_table(cell_table);
    }

    public CellRecords(Dictionary<int, Cell> cell_table, List<string> field_titles)
    {
        this.field_titles = field_titles;
        this.cell_table = cell_table;
    }

    private Dictionary<int, Cell> create_cell_table(CellFields cell_table)
    {
        Dictionary<int, Cell> temp = new Dictionary<int, Cell>();
        for (int i = 0; i < cell_table.GetId().Count; i++)
        {
            Cell record = new Cell(cell_table.GetId()[i], cell_table.GetOem()[i], cell_table.GetModel()[i],
                cell_table.GetLaunchAnnounced()[i], cell_table.GetLaunchStatus()[i],
                cell_table.GetBodyDimensions()[i],
                cell_table.GetBodyWeight()[i], cell_table.GetBodySim()[i], cell_table.GetDisplayType()[i],
                cell_table.GetDisplaySize()[i], cell_table.GetDisplayResolution()[i],
                cell_table.GetFeaturesSensorsCount()[i],
                cell_table.GetPlatformOs()[i], cell_table.GetFieldTitles());
            temp.Add(i + 1, record);
        }
        return temp;
    }


    public Dictionary<int, Cell> get_cell_table()
    {
        return this.cell_table;
    }

    public void set_cell_table(Dictionary<int, Cell> cell_table)
    {
        this.cell_table = cell_table;
    }

    public List<string> get_field_titles()
    {
        return this.field_titles;
    }

    public void set_field_titles(List<string> headers)
    {
        this.field_titles = headers;
    }

    public string headersToString()
    {
        string temp = "";
        foreach (string title in get_field_titles())
        {
            if (title != "year_of_launch" && title != "features_sensors_count")
            {
                temp += titleString(title);
            }
        }
        return temp;
    }

    public string customHeadersToString(string[] headersList)
    {
        string temp = "";
        foreach (string header in headersList)
        {
            temp += titleString(header);
        }
        return temp;
    }

    public string titleString(string title)
    {
        string temp = "";
        if (title == get_field_titles()[0])
        {
            temp += String.Format("{0,-5}", title);
        }
        if (title == get_field_titles()[1])
        {
            temp += String.Format("{0,-14}", title);
        }
        if (title == get_field_titles()[2])
        {
            temp += String.Format("{0,-26}", title);
        }
        if (title == get_field_titles()[3])
        {
            temp += String.Format("{0,-17}", title);
        }
        if (title == get_field_titles()[4])
        {
            temp += String.Format("{0,-14}", title);
        }
        if (title == get_field_titles()[5])
        {
            temp += String.Format("{0,-25}", title);
        }
        if (title == get_field_titles()[6])
        {
            temp += String.Format("{0,-12}", title);
        }
        if (title == get_field_titles()[7])
        {
            temp += String.Format("{0,-17}", title);
        }
        if (title == get_field_titles()[8])
        {
            temp += String.Format("{0,-25}", title);
        }
        if (title == get_field_titles()[9])
        {
            temp += String.Format("{0,-13}", title);
        }
        if (title == get_field_titles()[10])
        {
            temp += String.Format("{0,-19}", title);
        }
        if (title == get_field_titles()[11])
        {
            temp += String.Format("{0,-17}", title);
        }
        if (title == get_field_titles()[12])
        {
            temp += String.Format("{0,-32}", title);
        }
        return temp;
    }

    public string tableBorder(string s)
    {
        string temp = "";
        for (int i = 0; i < s.Length; i++)
        {
            temp += "-";
        }

        return temp;
    }

    public CellRecords copy()
    {
        return new CellRecords(cell_table, field_titles);
    }
}