using System.Collections;

namespace AltLangProj.Classes;

/// <summary>
/// This class represents a single record from the Cell Table.
/// Note: for my implementation I included a primary key column called '_id' to uniquely identify
/// each record. I also made one additional design decision and modified the '_featuresSensors'
/// data type from string to int?, so the column displays the number of feature sensors a cell phone has
/// instead of the (comma separated) string that lists the included feature sensors. I did this because
/// I wanted to be able to display all fields in one line of console output when printing a record and
/// to do that I needed to shorten some of the elements lengths (in a meaningful way). But it's worth
/// mentioning that I also didn't discard the original list of feature sensors (see CellFields class).
/// My program can easily recall this data as needed (using a method called printFeaturesSensorsList).
/// </summary>
public class Cell
{
    private int _id;
    private string _oem;
    private string _model;
    private int? _launchAnnounced;
    private string _launchStatus;
    private string _bodyDimensions;
    private double? _bodyWeight;
    private string _bodySim;
    private string _displayType;
    private double? _displaySize;
    private string _displayResolution;
    private int? _featuresSensors;
    private string _platformOs;
    private List<string> _fieldTitles;
    private Hashtable _cellMap;

    /// <summary>
    /// This Constructs a Cell object.
    /// </summary>
    /// <param name="id"> The primary key </param>
    /// <param name="oem"> The company name </param>
    /// <param name="model"> The _model of the phone</param>
    /// <param name="launchAnnounced"> The year the phone was announced </param>
    /// <param name="launchStatus"> The year the phone was released </param>
    /// <param name="bodyDimensions"> The dimensions of the phone </param>
    /// <param name="bodyWeight"> The weight of the phone </param>
    /// <param name="bodySim"> The type of sim the phone uses </param>
    /// <param name="displayType"> The type of display the phone uses </param>
    /// <param name="displaySize"> The size of the phones display </param>
    /// <param name="displayResolution"> The resolution of the phones display </param>
    /// <param name="featuresSensors"> The (number of) sensors the phone uses </param>
    /// <param name="platformOs"> The operating system the phone uses </param>
    /// <param name="fieldTitles"> List of all the column titles </param>
    public  Cell(int id, string oem, string model, int? launchAnnounced, string launchStatus, string bodyDimensions,
        double? bodyWeight, string bodySim, string displayType, double? displaySize, string displayResolution,
        int? featuresSensors, string platformOs, List<string> fieldTitles)
    {
        this._fieldTitles = fieldTitles;
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
        this._fieldTitles = fieldTitles;
        this._cellMap = CreateCellMap();
    }

    /// <summary>
    /// This Constructs a Cell object, and is used to construct a new Copy of an existing Cell object.
    /// </summary>
    /// <param name="id"> The primary key </param>
    /// <param name="oem"> The company name </param>
    /// <param name="model"> The _model of the phone</param>
    /// <param name="launchAnnounced"> The year the phone was announced </param>
    /// <param name="launchStatus"> The year the phone was released </param>
    /// <param name="bodyDimensions"> The dimensions of the phone </param>
    /// <param name="bodyWeight"> The weight of the phone </param>
    /// <param name="bodySim"> The type of sim the phone uses </param>
    /// <param name="displayType"> The type of display the phone uses </param>
    /// <param name="displaySize"> The size of the phones display </param>
    /// <param name="displayResolution"> The resolution of the phones display </param>
    /// <param name="featuresSensors"> The (number of) sensors the phone uses </param>
    /// <param name="platformOs"> The operating system the phone uses </param>
    /// <param name="fieldTitles"> List of all the column titles </param>
    /// <param name="cellMap"> A map with column titles as keys and Cell elements as values </param>
    public Cell(int id, string oem, string model, int? launchAnnounced, string launchStatus, string bodyDimensions,
        double? bodyWeight, string bodySim, string displayType, double? displaySize, string displayResolution,
        int? featuresSensors, string platformOs, List<string> fieldTitles, Hashtable cellMap)
    {
        this._fieldTitles = fieldTitles;
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
        this._fieldTitles = fieldTitles;
        this._cellMap = cellMap;
    }

    /// <summary>
    /// This method creates a map with column titles as keys and Cell elements as values.
    /// </summary>
    /// <returns> The Cell map (for referencing each value by field title) </returns>
    private Hashtable CreateCellMap()
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
            { GetFieldTitles()[12], GetPlatformOs() }
        };
        return temp;
    }

    /// <summary>
    /// This method gets the _id (primary key).
    /// </summary>
    /// <returns> The _id number </returns>
    public int GetId()
    {
        return this._id;
    }

    public void SetId(int key)
    {
        this._id = key;
    }

    /// <summary>
    /// This method gets the _oem.
    /// </summary>
    /// <returns> The _oem (company name) </returns>
    public string GetOem()
    {
        return this._oem;
    }

    public void SetOem(string item)
    {
        this._oem = item;
    }

    /// <summary>
    /// This method gets the _model name.
    /// </summary>
    /// <returns> The _model name  </returns>
    public string GetModel()
    {
        return this._model;
    }

    public void SetModel(string item)
    {
        this._model = item;
    }

    /// <summary>
    /// This method gets the year the phone was announced.
    /// </summary>
    /// <returns> The year the phone was announced </returns>
    public int? GetLaunchAnnounced()
    {
        return this._launchAnnounced;
    }

    public void SetLaunchAnnounced(int? item)
    {
        this._launchAnnounced = item;
    }

    /// <summary>
    /// This method gets the year the phone was launched.
    /// </summary>
    /// <returns> The year the phone was launched </returns>
    public string GetLaunchStatus()
    {
        return this._launchStatus;
    }

    public void SetLaunchStatus(string item)
    {
        this._launchStatus = item;
    }

    /// <summary>
    /// This method gets the dimensions of the phone.
    /// </summary>
    /// <returns> The dimensions of the phone </returns>
    public string GetBodyDimensions()
    {
        return this._bodyDimensions;
    }

    public void SetBodyDimensions(string item)
    {
        this._bodyDimensions = item;
    }

    /// <summary>
    /// This method gets the weight of the phone.
    /// </summary>
    /// <returns> The weight of the phone </returns>
    public double? GetBodyWeight()
    {
        return this._bodyWeight;
    }

    public void SetBodyWeight(double? item)
    {
        this._bodyWeight = item;
    }

    /// <summary>
    /// This method gets the type of sim the phone uses.
    /// </summary>
    /// <returns> The type of sim the phone uses </returns>
    public string GetBodySim()
    {
        return this._bodySim;
    }

    public void SetBodySim(string item)
    {
        this._bodySim = item;
    }

    /// <summary>
    /// This method gets the type of display the phone uses.
    /// </summary>
    /// <returns> The type of display the phone uses </returns>
    public string GetDisplayType()
    {
        return this._displayType;
    }

    public void SetDisplayType(string item)
    {
        this._displayType = item;
    }

    /// <summary>
    /// This method gets the size of the phones display.
    /// </summary>
    /// <returns> The size of the phones display </returns>
    public double? GetDisplaySize()
    {
        return this._displaySize;
    }

    public void SetDisplaySize(double? item)
    {
        this._displaySize = item;
    }

    /// <summary>
    /// This method gets the resolution of the phones display.
    /// </summary>
    /// <returns> The resolution of the phones display </returns>
    public string GetDisplayResolution()
    {
        return this._displayResolution;
    }

    public void SetDisplayResolution(string item)
    {
        this._displayResolution = item;
    }

    /// <summary>
    /// This method gets the number of feature sensors a phone has.
    /// </summary>
    /// <returns> The number of feature sensors a phone has </returns>
    public int? GetFeaturesSensors()
    {
        return this._featuresSensors;
    }

    public void SetFeaturesSensors(int? item)
    {
        this._featuresSensors = item;
    }

    /// <summary>
    /// This method gets the name of the OS the phone uses.
    /// </summary>
    /// <returns> The name of the OS the phone uses </returns>
    public string GetPlatformOs()
    {
        return this._platformOs;
    }

    public void SetPlatformOs(string item)
    {
        this._platformOs = item;
    }

    /// <summary>
    /// This method gets the list of field (column) titles for the table.
    /// </summary>
    /// <returns> The list of field titles </returns>
    public List<string> GetFieldTitles()
    {
        return this._fieldTitles;
    }

    public void SetFieldTitles(List<string> headers)
    {
        this._fieldTitles = headers;
    }

    /// <summary>
    /// This method gets the Cell map for this object.
    /// </summary>
    /// <returns> The Cell map for this object </returns>
    public Hashtable GetCellMap()
    {
        return this._cellMap;
    }

    public void SetCellMap(Hashtable table)
    {
        this._cellMap = table;
    }

    /// <summary>
    /// This method is used to create a new Copy of an existing Cell object.
    /// </summary>
    /// <returns> The Cell Copy </returns>
    public Cell Copy()
    {
        return new Cell(_id, _oem, _model, _launchAnnounced, _launchStatus, _bodyDimensions, _bodyWeight, _bodySim,
            _displayType, _displaySize, _displayResolution, _featuresSensors, _platformOs, _fieldTitles, _cellMap);
    }
}