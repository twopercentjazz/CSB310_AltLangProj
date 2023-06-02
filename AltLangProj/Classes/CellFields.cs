using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AltLangProj.Classes
{
    public class CellFields
    {
        private List<int> id;
        private List<string> oem;
        private List<string> model;
        private List<int?> launch_announced;
        private List<string> launch_status;
        private List<string> body_dimensions;
        private List<double?> body_weight;
        private List<string> body_sim;
        private List<string> display_type;
        private List<double?> display_size;
        private List<string> display_resolution;
        private List<string> features_sensors;
        private List<string> platform_os;
        private List<string> field_titles;
        private List<int?> year_of_launch;
        private List<int?> features_sensors_count;
        private Hashtable cell_table;

        public CellFields(List<List<string>> columnData, List<List<string>> rowData)
        {
            
            this.oem = columnData[0];
            this.model = columnData[1];
            this.launch_announced = columnData[2].Select(s => int.TryParse(s, out var n) ? n : (int?)null).ToList();
            this.launch_status = columnData[3];
            this.body_dimensions = columnData[4];
            this.body_weight = columnData[5].Select(s => double.TryParse(s, out var n) ? n : (double?)null).ToList(); 
            this.body_sim= columnData[6];
            this.display_type = columnData[7];
            this.display_size = columnData[8].Select(s => double.TryParse(s, out var n) ? n : (double?)null).ToList();
            this.display_resolution = columnData[9];
            this.features_sensors = columnData[10];
            this.platform_os = columnData[11];
            this.id = create_cell_id_column();
            this.year_of_launch = create_year_of_launch_list();
            this.features_sensors_count = create_features_sensors_count();
            this.field_titles = rowData[0];
            this.field_titles.Insert(0, "id");
            this.field_titles.Add("year_of_launch");
            this.field_titles.Add("features_sensors_count");
            this.cell_table = create_cell_table();
        }

        public CellFields(List<int> id, List<string> oem, List<string> model, List<int?> launch_announced, List<string> launch_status, List<string> body_dimensions, 
            List<double?> body_weight, List<string> body_sim, List<string> display_type, List<double?> display_size, List<string> display_resolution, List<string> features_sensors, 
            List<string> platform_os, List<string> field_titles, List<int?> year_of_launch, List<int?> features_sensors_count, Hashtable cell_table)
        {
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
            this.year_of_launch = year_of_launch;
            this.features_sensors_count = features_sensors_count;
            this.field_titles = field_titles;
            this.cell_table = cell_table;
        }

        private List<int> create_cell_id_column()
        {
            List<int> temp = new List<int>();
            for (int i = 1; i <= get_oem().Count; i++)
            {
                temp.Add(i);
            }
            return temp;
        }

        private List<int?> create_year_of_launch_list()
        {
            List<int?> temp = new List<int?>();
            foreach (string item in get_launch_status())
            {
                if (item == null || item == "Discontinued" || item == "Cancelled")
                {
                    temp.Add(null);
                }
                else 
                {
                    temp.Add(int.Parse(item));
                }
            }

            return temp;
        }

        private List<int?> create_features_sensors_count()
        {
            List<int?> temp = new List<int?>();
            foreach (string item in get_features_sensors())
            {
                if (item == null)
                {
                    temp.Add(null);
                }
                else
                {
                    temp.Add(item.Split(",").Length);
                }
            }
            return temp;
        }

        private Hashtable create_cell_table()
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
            temp.Add(get_field_titles()[13], get_year_of_launch());
            temp.Add(get_field_titles()[14], get_features_sensors_count());
            return temp;
        }


        public List<int> get_id()
        {
            return this.id;
        }

        public void set_id(List<int> column)
        {
            this.id = column;
        }

        public List<string> get_oem()
        {
            return this.oem;
        }

        public void set_oem(List<string> column)
        {
            this.oem = column;
        }

        public List<string> get_model()
        {
            return this.model;
        }

        public void set_model(List<string> column)
        {
            this.model = column;
        }

        public List<int?> get_launch_announced()
        {
            return this.launch_announced;
        }

        public void set_launch_announced(List<int?> column)
        {
            this.launch_announced = column;
        }

        public List<string> get_launch_status()
        {
            return this.launch_status;
        }

        public void set_launch_status(List<string> column)
        {
            this.launch_status = column;
        }

        public List<string> get_body_dimensions()
        {
            return this.body_dimensions;
        }

        public void set_body_dimensions(List<string> column)
        {
            this.body_dimensions = column;
        }

        public List<double?> get_body_weight()
        {
            return this.body_weight;
        }

        public void set_body_weight(List<double?> column)
        {
            this.body_weight = column;
        }

        public List<string> get_body_sim()
        {
            return this.body_sim;
        }

        public void set_body_sim(List<string> column)
        {
            this.body_sim = column;
        }

        public List<string> get_display_type()
        {
            return this.display_type;
        }

        public void set_display_type(List<string> column)
        {
            this.display_type = column;
        }

        public List<double?> get_display_size()
        {
            return this.display_size;
        }

        public void set_display_size(List<double?> column)
        {
            this.display_size = column;
        }

        public List<string> get_display_resolution()
        {
            return this.display_resolution;
        }

        public void set_display_resolution(List<string> column)
        {
            this.display_resolution = column;
        }

        public List<string> get_features_sensors()
        {
            return this.features_sensors;
        }

        public void set_features_sensors(List<string> column)
        {
            this.features_sensors = column;
        }

        public List<string> get_field_titles()
        {
            return this.field_titles;
        }

        public void set_field_titles(List<string> headers)
        {
            this.field_titles = headers;
        }

        public List<string> get_platform_os()
        {
            return this.platform_os;
        }

        public void set_platform_os(List<string> column)
        {
            this.platform_os = column;
        }

        public Hashtable get_cell_table()
        {
            return this.cell_table;
        }

        public void set_cell_table(Hashtable table)
        {
            this.cell_table = table;
        }

        public List<int?> get_year_of_launch()
        {
            return this.year_of_launch;
        }

        public void set_year_of_launch(List<int?> column)
        {
            this.year_of_launch = column;
        }

        public List<int?> get_features_sensors_count()
        {
            return this.features_sensors_count;
        }

        public void set_features_sensors_count(List<int?> column)
        {
            this.features_sensors_count = column;
        }




        public CellFields copy()
        {
            return new CellFields(id, oem, model, launch_announced, launch_status, body_dimensions,
                body_weight, body_sim, display_type, display_size, display_resolution, features_sensors,
                platform_os, field_titles, year_of_launch, features_sensors_count, cell_table);
        }

    }

}
