using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepReqDictionary
/// </summary>
public class DepReqDictionary
{
    public Dictionary<string, int> accumulator { get; set; }
    public Dictionary<string, List<int>> dictionary { get; set; }
    public HashSet<string> keys { get; set; }
    public DepReqDictionary()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}