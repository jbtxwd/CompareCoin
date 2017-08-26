using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class TaxEditor
{
    [MenuItem("Tool/TaxSetting")]
    // Use this for initialization
    static void Initial()
    {
        List<Tax> _psList = new List<Tax>();

        Tax _tax = new Tax();
        _tax.platformName = Platforms.p19800;
        _tax.coinName = Coins.BTC;
        _tax.sellTax = 0.001f;
        _tax.buyTax = 0.001f;
        _psList.Add(_tax);

		Tax _tax1 = new Tax();
		_tax1.platformName = Platforms.p19800;
        _tax1.coinName = Coins.ETH;
		_tax1.sellTax = 0.001f;
		_tax1.buyTax = 0.001f;
		_psList.Add(_tax1);

        string _json = JsonConvert.SerializeObject(_psList);
        Debug.Log(_json);
        CommonTool.CreateFile(Application.persistentDataPath, "TaxSettings.json", _json);
        Debug.Log(CommonTool.ReadFile(Application.persistentDataPath, "TaxSettings.json"));

    }
}