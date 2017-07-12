using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using cptool;
using System.Linq;
using Common;
public class CoinJubi : IPrice
{
    private string baseUrl = "https://www.jubi.com/api/v1/ticker/?coin=";
    Dictionary<string, Info> infos = new Dictionary<string, Info>();
    public CoinJubi()
    {
        infos["PGC "] = new Info("PGC ", "乐园通  ");
        infos["RSS "] = new Info("RSS ", "红贝壳  ");
        infos["FZ  "] = new Info("FZ  ", "冰河币  ");
        infos["VRC "] = new Info("VRC ", "维理币  ");
        infos["MAX "] = new Info("MAX ", "最大币  ");
        infos["TFC "] = new Info("TFC ", "传送币  ");
        infos["ZET "] = new Info("ZET ", "泽塔币  ");
        infos["QEC "] = new Info("QEC ", "企鹅币  ");
        infos["RIO "] = new Info("RIO ", "里约币  ");
        infos["XSGS"] = new Info("XSGS", "雪山古树");
        infos["YTC "] = new Info("YTC ", "一号币  ");
        infos["MTC "] = new Info("MTC ", "猴宝币  ");
        infos["GOOC"] = new Info("GOOC", "谷壳币  ");
        infos["ZCC "] = new Info("ZCC ", "招财币  ");
        infos["SKT "] = new Info("SKT ", "鲨之信  ");
        infos["LKC "] = new Info("LKC ", "幸运币  ");
        infos["MET "] = new Info("MET ", "美通币  ");
        infos["ETC "] = new Info("ETC ", "以太经典");
        infos["HLB "] = new Info("HLB ", "活力币  ");
        infos["DNC "] = new Info("DNC ", "暗网币  ");
        infos["XAS "] = new Info("XAS ", "阿希币  ");
        infos["MRYC"] = new Info("MRYC", "美人鱼币");
        infos["JBC "] = new Info("JBC ", "聚宝币  ");
        infos["PPC "] = new Info("PPC ", "点点币  ");
        infos["PLC "] = new Info("PLC ", "保罗币  ");
        infos["XPM "] = new Info("XPM ", "质数币  ");
        infos["DOGE"] = new Info("DOGE", "狗狗币  ");
        infos["EAC "] = new Info("EAC ", "地球币  ");
        infos["VTC "] = new Info("VTC ", "绿币    ");
        infos["WDC "] = new Info("WDC ", "世界币  ");
        infos["GAME"] = new Info("GAME", "游戏点  ");
        infos["IFC "] = new Info("IFC ", "无限币  ");
        infos["BTC "] = new Info("BTC ", "比特币  ");
        infos["LTC "] = new Info("LTC ", "莱特币  ");
        infos["ETH "] = new Info("ETH ", "以太坊  ");
        infos["KTC "] = new Info("KTC ", "肯特币  ");
        infos["ANS "] = new Info("ANS ", "小蚁股  ");
        infos["XRP "] = new Info("XRP ", "瑞波币  ");
        infos["PEB "] = new Info("PEB ", "普银    ");
        infos["BLK "] = new Info("BLK ", "黑币    ");
        infos["LSK "] = new Info("LSK ", "LISK    ");
        infos["NXT "] = new Info("NXT ", "未来币  ");
        infos["BTS "] = new Info("BTS ", "比特股  ");
    }

    public void Init(List<string> usei)
    {
        CoroutineManager.instance.StartCoroutine(YieldTick());
    }

    void Tick()
    {
        foreach (var c in infos)
        {
            Get(c.Key.ToLower().Replace(" ", ""), (_result) =>
            {
                var _json = MyJson.Parse(_result.text) as MyJson.JsonNode_Object;
                this.infos[c.Key].price = double.Parse(_json.GetDictItem("last").AsString());
                this.infos[c.Key].sell = double.Parse(_json.GetDictItem("sell").AsString());
                this.infos[c.Key].buy = double.Parse(_json.GetDictItem("buy").AsString());
                this.infos[c.Key].vol = double.Parse(_json.GetDictItem("vol").ToString());
                this.infos[c.Key].updatetime = DateTime.Now;
                this.infos[c.Key].change = true;
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
