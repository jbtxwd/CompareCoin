using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class TaxData : CSingleton<TaxData>
{
    TaxData()
    {
        Inital();
    }

    public List<Tax> data;
    Tax _defaultTax;
    float _defaultBuyTax = 0.001f;
	float _defaultSellTax = 0.001f;
    void Inital()
    {
        string _js = CommonTool.ReadFile(Application.persistentDataPath, "TaxSettings.json");
        data =  JsonConvert.DeserializeObject<List<Tax>>(_js);
        Debug.Log(data.Count);
        _defaultTax = new Tax();
        _defaultTax.buyTax = _defaultBuyTax;
        _defaultTax.sellTax = _defaultSellTax;
            
    }

    public Tax GetTax(string _platform,string _coin)
    {
        Tax _t = data.Find(p => p.platformName == _platform && p.coinName == _coin);
        if (_t != null)
            return _t;
        return _defaultTax;
    }
}