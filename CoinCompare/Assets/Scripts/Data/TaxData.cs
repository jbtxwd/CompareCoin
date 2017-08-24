using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxData : CSingleton<TaxData>
{
    TaxData()
    {
        Inital();
    }

    public List<Tax> data;
	
    void Inital()
    {
        
    }
}
