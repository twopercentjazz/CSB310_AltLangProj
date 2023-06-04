namespace AltLangProj.Classes;

/// <summary>
/// This class represents a set of query parameters used to create a new query table (by filtering a copy of the original
/// table by the desired parameters added to this object). This works similarly to a 'where' clause in an SQL query.
///
/// Note: you can filter results by providing individual elements or if the data is numeric you can filter results by providing
/// a range of values. This is implemented with dictionaries where the keys represent the fields you want to limit.
///
/// For Example: to create a new query table where "oem" is "Sony" or "Google", and "launch_announced" is 2019, and
/// "display_size" is between 6 and 7, you need to add three things to this FilterParameter object (after its initialized).
/// If we have a FilterParameter object called temp, the three adds would be...
/// 1. temp.GetFilterString().Add("oem", new []{"Google", "Sony"});
/// 2. temp.GetFilterInt().Add("launch_announced", new []{2019});
/// 3. temp.GetFilterDoubleRange().Add("display_size", new KeyValuePair<double,double>(6.0, 7.0));
/// </summary>
public class FilterParameters
{
    private Dictionary<string, string[]> _filterString;
    private Dictionary<string, int[]> _filterInt;
    private Dictionary<string, double[]> _filterDouble;
    private Dictionary<string, KeyValuePair<int, int>> _filterIntRange;
    private Dictionary<string, KeyValuePair<double, double>> _filterDoubleRange;

    /// <summary>
    /// This constructs an empty FilterParameters object.
    /// </summary>
    public FilterParameters()
    {
        this._filterString = new Dictionary<string, string[]>();
        this._filterInt = new Dictionary<string, int[]>();
        this._filterDouble = new Dictionary<string, double[]>();
        this._filterIntRange = new Dictionary<string, KeyValuePair<int, int>>();
        this._filterDoubleRange = new Dictionary<string, KeyValuePair<double, double>>();
    }

    /// <summary>
    /// This method gets the string dictionary for filtering the table.
    /// </summary>
    /// <returns> The filter string dictionary </returns>
    public Dictionary<string, string[]> GetFilterString()
    {
        return this._filterString;
    }

    /// <summary>
    /// This method sets the string dictionary for filtering the table.
    /// </summary>
    /// <param name="newFilter"> The new filter string dictionary </param>
    public void SetFilterString(Dictionary<string, string[]> newFilter)
    {
        this._filterString = newFilter;
    }

    /// <summary>
    /// This method gets the int dictionary for filtering the table.
    /// </summary>
    /// <returns> The filter int dictionary </returns>
    public Dictionary<string, int[]> GetFilterInt()
    {
        return this._filterInt;
    }

    /// <summary>
    /// This method sets the int dictionary for filtering the table.
    /// </summary>
    /// <param name="newFilter"> The new filter int dictionary </param>
    public void SetFilterInt(Dictionary<string, int[]> newFilter)
    {
        this._filterInt = newFilter;
    }

    /// <summary>
    /// This method gets the double dictionary for filtering the table.
    /// </summary>
    /// <returns> The filter double dictionary </returns>
    public Dictionary<string, double[]> GetFilterDouble()
    {
        return this._filterDouble;
    }

    /// <summary>
    /// This method sets the double dictionary for filtering the table.
    /// </summary>
    /// <param name="newFilter"> The new filter double dictionary </param>
    public void SetFilterDouble(Dictionary<string, double[]> newFilter)
    {
        this._filterDouble = newFilter;
    }

    /// <summary>
    /// This method gets the int range dictionary for filtering the table.
    /// </summary>
    /// <returns> The filter int range dictionary </returns>
    public Dictionary<string, KeyValuePair<int, int>> GetFilterIntRange()
    {
        return this._filterIntRange;
    }

    /// <summary>
    /// This method sets the int range dictionary for filtering the table.
    /// </summary>
    /// <param name="newFilter"> The new filter int range dictionary </param>
    public void SetFilterIntRange(Dictionary<string, KeyValuePair<int, int>> newFilter)
    {
        this._filterIntRange = newFilter;
    }

    /// <summary>
    /// This method gets the double range dictionary for filtering the table.
    /// </summary>
    /// <returns> The filter double range dictionary </returns>
    public Dictionary<string, KeyValuePair<double, double>> GetFilterDoubleRange()
    {
        return this._filterDoubleRange;
    }

    /// <summary>
    /// This method sets the double range dictionary for filtering the table.
    /// </summary>
    /// <param name="newFilter"> The new filter double range dictionary</param>
    public void SetFilterDoubleRange(Dictionary<string, KeyValuePair<double, double>> newFilter)
    {
        this._filterDoubleRange = newFilter;
    }
}