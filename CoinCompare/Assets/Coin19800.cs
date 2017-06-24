using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using cptool;
using System.Linq;
using Common;
public class Coin19800 :IPrice
{
    private string baseUrl = "https://www.19800.com/api/v1/ticker?market=cny_";
    Dictionary<string, Info> infos = new Dictionary<string, Info>();

    public void Init()
    {
        infos["YTC "] = new Info("YTC ", "一号币  ");
        infos["ETC "] = new Info("ETC ", "以太经典");
        infos["PLC "] = new Info("PLC ", "保罗币  ");
        infos["VTC "] = new Info("VTC ", "绿币    ");
        infos["BTC "] = new Info("BTC ", "比特币  ");
        infos["ETH "] = new Info("ETH ", "以太坊  ");
        infos["ANS "] = new Info("ANS ", "小蚁股  ");
        infos["BTS "] = new Info("BTS ", "比特股  ");
        CoroutineManager.instance.StartCoroutine(YieldTick());
    }

    void Tick()
    {
        foreach (var c in infos)
        {
            Get(c.Key.ToLower().Replace(" ", ""), (_result) =>
            {
                var _json = MyJson.Parse(_result.text) as MyJson.JsonNode_Object;
                var code = _json.GetDictItem("code").AsInt();
                if (code >= 0)
                {
                    var dat = _json.GetDictItem("data").asDict();
                    this.infos[c.Key].price = dat["LastPrice"].AsDouble();
                    this.infos[c.Key].sell = dat["TopAsk"].AsDouble();
                    this.infos[c.Key].buy = dat["TopBid"].AsDouble();
                    this.infos[c.Key].vol = dat["Volume"].AsDouble();
                    this.infos[c.Key].change = true;
                    Debug.Log(dat["LastPrice"].AsDouble()+c.Key);
                }
            });
        }
    }

    IEnumerator YieldTick()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            Tick();
        }
    }

    public string[] GetKeys()
    {
        return infos.Keys.ToArray();
    }
    public Info GetInfo(string key)
    {
        if (infos.ContainsKey(key) == false) return null;
        return infos[key].Clone();
    }

    public void Get(string key, Action<WWW> result)
    {
        int r = UnityEngine.Random.Range(0, 100);
        string url = baseUrl + key+ "&nonce=" + r;
        CoroutineManager.instance.DoCoroutine(PostWWW(url,result));
        //StartCoroutine(PostWWW(url, result));
    }

    IEnumerator PostWWW(string url, Action<WWW> result)
    {
        var www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            if (result != null)
                result(www);
        }
        else
        {
            Debug.Log("post error+" + www.error.ToString() + "==url==" + url);
        }
    }
}
