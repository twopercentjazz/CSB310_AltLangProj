namespace AltLangProj.Classes;

public class FilterParameters
{
    private Dictionary<string, string[]> filterString;
    private Dictionary<string, int[]> filterInt;
    private Dictionary<string, double[]> filterDouble;
    private Dictionary<string, KeyValuePair<int, int>> filterIntRange;
    private Dictionary<string, KeyValuePair<double, double>> filterDoubleRange;

    public FilterParameters(Dictionary<string, string[]> filterString, Dictionary<string, int[]> filterInt, Dictionary<string, double[]> filterDouble, 
        Dictionary<string, KeyValuePair<int, int>> filterIntRange, Dictionary<string, KeyValuePair<double, double>> filterDoubleRange) 
    {
        this.filterString = filterString;
        this.filterInt = filterInt;
        this.filterDouble = filterDouble;
        this.filterIntRange = filterIntRange;
        this.filterDoubleRange = filterDoubleRange;
    }

    public FilterParameters()
    {
        this.filterString = new Dictionary<string, string[]>();
        this.filterInt = new Dictionary<string, int[]>();
        this.filterDouble = new Dictionary<string, double[]>();
        this.filterIntRange = new Dictionary<string, KeyValuePair<int, int>>();
        this.filterDoubleRange = new Dictionary<string, KeyValuePair<double, double>>();
    }

    public Dictionary<string, string[]> getFilterString()
    {
        return this.filterString;
    }

    public Dictionary<string, int[]> getFilterInt()
    {
        return this.filterInt;
    }

    public Dictionary<string, double[]> getFilterDouble()
    {
        return this.filterDouble;
    }

    public Dictionary<string, KeyValuePair<int, int>> getFilterIntRange()
    {
        return this.filterIntRange;
    }

    public Dictionary<string, KeyValuePair<double, double>> getFilterDoubleRange()
    {
        return this.filterDoubleRange;
    }

}