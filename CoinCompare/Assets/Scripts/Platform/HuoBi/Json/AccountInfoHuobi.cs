using System.Collections.Generic;

public class List
{
    public string currency { get; set; }
    public string type { get; set; }
    public string balance { get; set; }
}

public class Data
{
    public int id { get; set; }
    public string type { get; set; }
    public string state { get; set; }
    public List<List> list { get; set; }
}

public class AccountInfoHuobi
{
    public string status { get; set; }
    public Data data { get; set; }
}