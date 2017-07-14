using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Price
{
    public float price;
    public float count;
}

public class Depth
{
    public long time;
    public List<Price> asks = new List<Price>();
    public List<Price> bids = new List<Price>();
}
