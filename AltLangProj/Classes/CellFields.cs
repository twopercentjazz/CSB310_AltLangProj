using System.Collections;

namespace AltLangProj.Classes
{
    /// <summary>
    /// This class represents a column oriented version of the cell phone table. This class is used to build one of 
    /// the two table representations used in the CellTable class.
    /// 
    /// Note: This is an additional representation of the cell phone table (other than the row oriented table described in the
    /// project specifications). In our AD350, when talking about physical design it was mentioned that row based storage was
    /// useful for transactional applications and column based storage was useful for analytical applications. Since, some of
    /// my additional methods are analytical I wanted to explore implementing this alternative column based representation.
    /// </summary>
    public class CellFields
    {
        private List<int> _id;
        private List<string> _oem;
        private List<string> _model;
        private List<int?> _launchAnnounced;
        private List<string> _launchStatus;
        private List<string> _bodyDimensions;
        private List<double?> _bodyWeight;
        private List<string> _bodySim;
        private List<string> _displayType;
        private List<double?> _displaySize;
        private List<string> _displayResolution;
        private List<string> _featuresSensors;
        private List<string> _platformOs;
        private List<string> _fieldTitles;
        private List<int?> _yearOfLaunch;
        private List<int?> _featuresSensorsCount;
        private Hashtable _cellTable;

        /// <summary>
        /// This Constructs a CellFields object.
        /// </summary>
        /// <param name="columnData"> All table columns </param>
        /// <param name="rowData"> All table rows </param>
        public CellFields(List<List<string>> columnData, List<List<string>> rowData)
        {
            this._oem = columnData[0];
            this._model = columnData[1];
            this._launchAnnounced = columnData[2].Select(s => int.TryParse(s, out var n) ? n : (int?)null).ToList();
            this._launchStatus = columnData[3];
            this._bodyDimensions = columnData[4];
            this._bodyWeight = columnData[5].Select(s => double.TryParse(s, out var n) ? n : (double?)null).ToList(); 
            this._bodySim= columnData[6];
            this._displayType = columnData[7];
            this._displaySize = columnData[8].Select(s => double.TryParse(s, out var n) ? n : (double?)null).ToList();
            this._displayResolution = columnData[9];
            this._featuresSensors = columnData[10];
            this._platformOs = columnData[11];
            this._id = CreateCellIdColumn();
            this._yearOfLaunch = CreateYearOfLaunchList();
            this._featuresSensorsCount = CreateFeaturesSensorsCountList();
            this._fieldTitles = rowData[0];
            this._fieldTitles.Insert(0, "id");
            this._fieldTitles.Add("year_of_launch");
            this._fieldTitles.Add("features_sensors_count");
            this._cellTable = CreateCellTable();
        }

        /// <summary>
        /// This Constructs a CellFields object, and is used to construct a new Copy of an existing CellFields object.
        /// </summary>
        /// <param name="id"> The primary key </param>
        /// <param name="oem"> The company name </param>
        /// <param name="model"> The _model of the phone</param>
        /// <param name="launchAnnounced"> The year the phone was announced </param>
        /// <param name="launchStatus"> The year the phone was released (string) </param>
        /// <param name="bodyDimensions"> The dimensions of the phone </param>
        /// <param name="bodyWeight"> The weight of the phone </param>
        /// <param name="bodySim"> The type of sim the phone uses </param>
        /// <param name="displayType"> The type of display the phone uses </param>
        /// <param name="displaySize"> The size of the phones display </param>
        /// <param name="displayResolution"> The resolution of the phones display </param>
        /// <param name="featuresSensors"> The sensors the phone uses </param>
        /// <param name="platformOs"> The operating system the phone uses </param>
        /// <param name="fieldTitles"> List of all the column titles </param>
        /// <param name="yearOfLaunch"> The year the phone was released (int) </param>
        /// <param name="featuresSensorsCount"> The number of sensors the phone uses </param>
        /// <param name="cellTable"> A table with column titles as keys and the lists of column elements as values </param>
        public CellFields(List<int> id, List<string> oem, List<string> model, List<int?> launchAnnounced, List<string> launchStatus, List<string> bodyDimensions, 
            List<double?> bodyWeight, List<string> bodySim, List<string> displayType, List<double?> displaySize, List<string> displayResolution, List<string> featuresSensors, 
            List<string> platformOs, List<string> fieldTitles, List<int?> yearOfLaunch, List<int?> featuresSensorsCount, Hashtable cellTable)
        {
            this._oem = oem;
            this._model = model;
            this._launchAnnounced = launchAnnounced;
            this._launchStatus = launchStatus;
            this._bodyDimensions = bodyDimensions;
            this._bodyWeight = bodyWeight;
            this._bodySim = bodySim;
            this._displayType = displayType;
            this._displaySize = displaySize;
            this._displayResolution = displayResolution;
            this._featuresSensors = featuresSensors;
            this._platformOs = platformOs;
            this._id = id;
            this._yearOfLaunch = yearOfLaunch;
            this._featuresSensorsCount = featuresSensorsCount;
            this._fieldTitles = fieldTitles;
            this._cellTable = cellTable;
        }

        /// <summary>
        /// This method creates the primary key column called id
        /// </summary>
        /// <returns> The id column </returns>
        private List<int> CreateCellIdColumn()
        {
            List<int> temp = new List<int>();
            for (int i = 1; i <= GetOem().Count; i++)
            {
                temp.Add(i);
            }
            return temp;
        }

        /// <summary>
        /// This method creates an int representation of the launch status column.
        /// </summary>
        /// <returns> The year of launch column </returns>
        private List<int?> CreateYearOfLaunchList()
        {
            List<int?> temp = new List<int?>();
            foreach (string item in GetLaunchStatus())
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

        /// <summary>
        /// This method creates a column comprised of the count of the features sensors column.
        /// </summary>
        /// <returns> The features sensors count column </returns>
        private List<int?> CreateFeaturesSensorsCountList()
        {
            List<int?> temp = new List<int?>();
            foreach (string item in GetFeaturesSensors())
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

        /// <summary>
        /// This method creates a map with column titles as keys and and the lists of column elements as values.
        /// </summary>
        /// <returns> The Cell Table (for referencing each column by field title) </returns>
        private Hashtable CreateCellTable()
        {
            Hashtable temp = new()
            {
                { GetFieldTitles()[0], GetId() },
                { GetFieldTitles()[1], GetOem() },
                { GetFieldTitles()[2], GetModel() },
                { GetFieldTitles()[3], GetLaunchAnnounced() },
                { GetFieldTitles()[4], GetLaunchStatus() },
                { GetFieldTitles()[5], GetBodyDimensions() },
                { GetFieldTitles()[6], GetBodyWeight() },
                { GetFieldTitles()[7], GetBodySim() },
                { GetFieldTitles()[8], GetDisplayType() },
                { GetFieldTitles()[9], GetDisplaySize() },
                { GetFieldTitles()[10], GetDisplayResolution() },
                { GetFieldTitles()[11], GetFeaturesSensors() },
                { GetFieldTitles()[12], GetPlatformOs() },
                { GetFieldTitles()[13], GetYearOfLaunch() },
                { GetFieldTitles()[14], GetFeaturesSensorsCount() }
            };
            return temp;
        }

        /// <summary>
        /// This method gets the id (primary key) column.
        /// </summary>
        /// <returns> The id column </returns>
        public List<int> GetId()
        {
            return this._id;
        }

        /// <summary>
        /// This method sets the id (primary key) column.
        /// </summary>
        /// <param name="column"> The new id column </param>
        public void SetId(List<int> column)
        {
            this._id = column;
        }

        /// <summary>
        /// This method gets the oem name column.
        /// </summary>
        /// <returns> The oem (company) name column </returns>
        public List<string> GetOem()
        {
            return this._oem;
        }

        /// <summary>
        /// This method sets the oem name column.
        /// </summary>
        /// <param name="column"> The new oem name column </param>
        public void SetOem(List<string> column)
        {
            this._oem = column;
        }

        /// <summary>
        /// This method gets the model name column.
        /// </summary>
        /// <returns> The model name column </returns>
        public List<string> GetModel()
        {
            return this._model;
        }

        /// <summary>
        /// This method sets the model name column.
        /// </summary>
        /// <param name="column"> The new model name column </param>
        public void SetModel(List<string> column)
        {
            this._model = column;
        }

        /// <summary>
        /// This method gets launched announced column.
        /// </summary>
        /// <returns> The launched announced column </returns>
        public List<int?> GetLaunchAnnounced()
        {
            return this._launchAnnounced;
        }

        /// <summary>
        /// This method sets launched announced column.
        /// </summary>
        /// <param name="column"> The new launched announced column</param>
        public void SetLaunchAnnounced(List<int?> column)
        {
            this._launchAnnounced = column;
        }

        /// <summary>
        /// This method gets the launch status column.
        /// </summary>
        /// <returns> The launch status column </returns>
        public List<string> GetLaunchStatus()
        {
            return this._launchStatus;
        }

        /// <summary>
        /// This method sets the launch status column.
        /// </summary>
        /// <param name="column"> The new launch status column </param>
        public void SetLaunchStatus(List<string> column)
        {
            this._launchStatus = column;
        }

        /// <summary>
        /// This method gets the body dimensions column.
        /// </summary>
        /// <returns> The body dimensions column </returns>
        public List<string> GetBodyDimensions()
        {
            return this._bodyDimensions;
        }

        /// <summary>
        /// This method sets the body dimensions column.
        /// </summary>
        /// <param name="column"> The new body dimensions column </param>
        public void SetBodyDimensions(List<string> column)
        {
            this._bodyDimensions = column;
        }

        /// <summary>
        /// This method gets the body weight column.
        /// </summary>
        /// <returns> The body weight column </returns>
        public List<double?> GetBodyWeight()
        {
            return this._bodyWeight;
        }

        /// <summary>
        /// This method sets the body weight column.
        /// </summary>
        /// <param name="column"> The new body weight column </param>
        public void SetBodyWeight(List<double?> column)
        {
            this._bodyWeight = column;
        }

        /// <summary>
        /// This method gets the body sim column.
        /// </summary>
        /// <returns> The body sim column </returns>
        public List<string> GetBodySim()
        {
            return this._bodySim;
        }

        /// <summary>
        /// This method sets the body sim column.
        /// </summary>
        /// <param name="column"> The new body sim column </param>
        public void SetBodySim(List<string> column)
        {
            this._bodySim = column;
        }

        /// <summary>
        /// This method gets the display type column.
        /// </summary>
        /// <returns> The display type column </returns>
        public List<string> GetDisplayType()
        {
            return this._displayType;
        }

        /// <summary>
        /// This method sets the display type column.
        /// </summary>
        /// <param name="column"> The new display type column </param>
        public void SetDisplayType(List<string> column)
        {
            this._displayType = column;
        }

        /// <summary>
        /// This method gets the display size column.
        /// </summary>
        /// <returns> The display size column </returns>
        public List<double?> GetDisplaySize()
        {
            return this._displaySize;
        }

        /// <summary>
        /// This method sets the display size column.
        /// </summary>
        /// <param name="column"> The new display size column </param>
        public void SetDisplaySize(List<double?> column)
        {
            this._displaySize = column;
        }

        /// <summary>
        /// This method gets the display resolution column.
        /// </summary>
        /// <returns> The display resolution column </returns>
        public List<string> GetDisplayResolution()
        {
            return this._displayResolution;
        }

        /// <summary>
        /// This method sets the display resolution column.
        /// </summary>
        /// <param name="column"> The new display resolution column </param>
        public void SetDisplayResolution(List<string> column)
        {
            this._displayResolution = column;
        }

        /// <summary>
        /// This method gets the features sensors column.
        /// </summary>
        /// <returns> The features sensors column </returns>
        public List<string> GetFeaturesSensors()
        {
            return this._featuresSensors;
        }

        /// <summary>
        /// This method sets the features sensors column.
        /// </summary>
        /// <param name="column"> The new features sensors column </param>
        public void SetFeaturesSensors(List<string> column)
        {
            this._featuresSensors = column;
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
        /// This method gets the platform os column.
        /// </summary>
        /// <returns> The platform os column </returns>
        public List<string> GetPlatformOs()
        {
            return this._platformOs;
        }

        /// <summary>
        /// This method sets the platform os column.
        /// </summary>
        /// <param name="column"> The platform os column </param>
        public void SetPlatformOs(List<string> column)
        {
            this._platformOs = column;
        }

        /// <summary>
        /// This method gets the Cell Table (organized by field).
        /// </summary>
        /// <returns> The Cell Table </returns>
        public Hashtable GetCellTable()
        {
            return this._cellTable;
        }

        /// <summary>
        /// This method sets the Cell Table (organized by field).
        /// </summary>
        /// <param name="table"> The new Cell Table </param>
        public void SetCellTable(Hashtable table)
        {
            this._cellTable = table;
        }

        /// <summary>
        /// This method gets the year of launch column (int version of launch status column).
        /// </summary>
        /// <returns> The year of launch column </returns>
        public List<int?> GetYearOfLaunch()
        {
            return this._yearOfLaunch;
        }

        /// <summary>
        /// This method sets the year of launch column (int version of launch status column).
        /// </summary>
        /// <param name="column"> The new year of launch column </param>
        public void SetYearOfLaunch(List<int?> column)
        {
            this._yearOfLaunch = column;
        }

        /// <summary>
        /// This method gets the features sensors count column.
        /// This represents the count found using the features sensors column.
        /// </summary>
        /// <returns> The features sensors count column </returns>
        public List<int?> GetFeaturesSensorsCount()
        {
            return this._featuresSensorsCount;
        }

        /// <summary>
        /// This method sets the features sensors count column.
        /// </summary>
        /// <param name="column"> The new features sensors count column </param>
        public void SetFeaturesSensorsCount(List<int?> column)
        {
            this._featuresSensorsCount = column;
        }

        /// <summary>
        /// This method is used to create a new Copy of an existing CellFields object.
        /// </summary>
        /// <returns> The CellFields Copy </returns>
        public CellFields Copy()
        {
            return new CellFields(_id, _oem, _model, _launchAnnounced, _launchStatus, _bodyDimensions,
                _bodyWeight, _bodySim, _displayType, _displaySize, _displayResolution, _featuresSensors,
                _platformOs, _fieldTitles, _yearOfLaunch, _featuresSensorsCount, _cellTable);
        }
    }
}
