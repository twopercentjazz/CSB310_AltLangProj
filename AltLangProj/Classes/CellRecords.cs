namespace AltLangProj.Classes;

public class CellRecords
{
    private Dictionary<int, Cell> cell_table;
    private List<string> field_titles;

    public CellRecords(CellFields cell_table)
    {
        this.field_titles = cell_table.get_field_titles();
        this.cell_table = create_cell_table(cell_table);
    }

    private Dictionary<int, Cell> create_cell_table(CellFields cell_table)
    {
        Dictionary<int, Cell> temp = new Dictionary<int, Cell>();
        for (int i = 0; i < cell_table.get_id().Count; i++)
        {
            Cell record = new Cell(cell_table.get_id()[i], cell_table.get_oem()[i], cell_table.get_model()[i],
                cell_table.get_launch_announced()[i], cell_table.get_launch_status()[i],
                cell_table.get_body_dimensions()[i],
                cell_table.get_body_weight()[i], cell_table.get_body_sim()[i], cell_table.get_display_type()[i],
                cell_table.get_display_size()[i], cell_table.get_display_resolution()[i],
                cell_table.get_features_sensors_count()[i],
                cell_table.get_platform_os()[i], cell_table.get_field_titles());
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
            }

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
}