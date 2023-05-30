using System.Collections;

namespace AltLangProj.Classes;

public class Cell
{
    private int id;
    private string oem;
    private string model;
    private int? launch_announced;
    private string launch_status;
    private string body_dimensions;
    private double? body_weight;
    private string body_sim;
    private string display_type;
    private double? display_size;
    private string display_resolution;
    private int? features_sensors;
    private string platform_os;
    private List<string> field_titles;
    private Hashtable cell_map;

    public  Cell(int id, string oem, string model, int? launch_announced, string launch_status, string body_dimensions,
        double? body_weight, string body_sim, string display_type, double? display_size, string display_resolution,
        int? features_sensors, string platform_os, List<string> field_titles)
    {
        this.field_titles = field_titles;
        this.oem = oem;
        this.model = model;
        this.launch_announced = launch_announced;
        this.launch_status = launch_status;
        this.body_dimensions = body_dimensions;
        this.body_weight = body_weight;
        this.body_sim = body_sim;
        this.display_type = display_type;
        this.display_size = display_size;
        this.display_resolution = display_resolution;
        this.features_sensors = features_sensors;
        this.platform_os = platform_os;
        this.id = id;
        this.field_titles = field_titles;
        this.cell_map = create_cell_map();
    }


    private Hashtable create_cell_map()
    {
        Hashtable temp = new Hashtable();
        temp.Add(get_field_titles()[0], get_id());
        temp.Add(get_field_titles()[1], get_oem());
        temp.Add(get_field_titles()[2], get_model());
        temp.Add(get_field_titles()[3], get_launch_announced());
        temp.Add(get_field_titles()[4], get_launch_status());
        temp.Add(get_field_titles()[5], get_body_dimensions());
        temp.Add(get_field_titles()[6], get_body_weight());
        temp.Add(get_field_titles()[7], get_body_sim());
        temp.Add(get_field_titles()[8], get_display_type());
        temp.Add(get_field_titles()[9], get_display_size());
        temp.Add(get_field_titles()[10], get_display_resolution());
        temp.Add(get_field_titles()[11], get_features_sensors());
        temp.Add(get_field_titles()[12], get_platform_os());
        return temp;
    }

    public int get_id()
    {
        return this.id;
    }

    public void set_id(int key)
    {
        this.id = key;
    }

    public string get_oem()
    {
        return this.oem;
    }

    public void set_oem(string item)
    {
        this.oem = item;
    }

    public string get_model()
    {
        return this.model;
    }

    public void set_model(string item)
    {
        this.model = item;
    }

    public int? get_launch_announced()
    {
        return this.launch_announced;
    }

    public void set_launch_announced(int? item)
    {
        this.launch_announced = item;
    }

    public string get_launch_status()
    {
        return this.launch_status;
    }

    public void set_launch_status(string item)
    {
        this.launch_status = item;
    }

    public string get_body_dimensions()
    {
        return this.body_dimensions;
    }

    public void set_body_dimensions(string item)
    {
        this.body_dimensions = item;
    }

    public double? get_body_weight()
    {
        return this.body_weight;
    }

    public void set_body_weight(double? item)
    {
        this.body_weight = item;
    }

    public string get_body_sim()
    {
        return this.body_sim;
    }

    public void set_body_sim(string item)
    {
        this.body_sim = item;
    }

    public string get_display_type()
    {
        return this.display_type;
    }

    public void set_display_type(string item)
    {
        this.display_type = item;
    }

    public double? get_display_size()
    {
        return this.display_size;
    }

    public void set_display_size(double? item)
    {
        this.display_size = item;
    }

    public string get_display_resolution()
    {
        return this.display_resolution;
    }

    public void set_display_resolution(string item)
    {
        this.display_resolution = item;
    }

    public int? get_features_sensors()
    {
        return this.features_sensors;
    }

    public void set_features_sensors(int? item)
    {
        this.features_sensors = item;
    }

    public string get_platform_os()
    {
        return this.platform_os;
    }

    public void set_platform_os(string item)
    {
        this.platform_os = item;
    }

    public List<string> get_field_titles()
    {
        return this.field_titles;
    }

    public void set_field_titles(List<string> headers)
    {
        this.field_titles = headers;
    }

    public Hashtable get_cell_map()
    {
        return this.cell_map;
    }

    public void set_cell_map(Hashtable table)
    {
        this.cell_map = table;
    }

    
}