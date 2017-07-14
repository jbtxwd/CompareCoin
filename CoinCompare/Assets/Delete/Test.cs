using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Test : MonoBehaviour {

    string amount = "1";
    string price = "1";
    string type = "buy";
    string nonce = "565";
    public string coin = "btc";
    public string _privateKey = "v*zF%-CmbKz-epx}W-E(H(4-AxU{6-yCkFz-~}uyv";
    public string pub_key= "hcdia-zq5y2-s9462-rzpf3-3tf4x-gnfpq-k73yr";
    // Use this for initialization
    void Start () {
        string msg = amount + "&" + price + "&" + type + "&" + nonce + "&" + _privateKey;
        Debug.Log(msg);
        string digest = Token.GetSignature(_privateKey, msg);
        Debug.Log(digest);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log(Time.time);
            Debug.Log(Trade_List(coin));
        }
	}

    public string Trade_List(string coin, string type = "all", double since = 0)
    {
        Dictionary<string, string> datas = new Dictionary<string, string>();
        datas.Add("since", since.ToString());
        datas.Add("coin", coin);
        datas.Add("type", type);
        return UserRequest("trade_list", datas);
    }

    public string Trade_Add(double amount, double price, string type, string coin)
    {
        //return "{\"result\":\"true\",\"id\":\"11111\"}";
        Dictionary<string, string> datas = new Dictionary<string, string>();
        datas.Add("amount", amount.ToString());
        datas.Add("price", price.ToString());
        datas.Add("type", type);
        datas.Add("coin", coin);
        return UserRequest("trade_add", datas);
    }

    public string UserRequest(string api, Dictionary<string, string> datas = null)
    {
        if (datas == null)
            datas = new Dictionary<string, string>();

        string nonce = Token.GetNonce();

        datas.Add("nonce", nonce);
        datas.Add("key", pub_key);

        int i = 0;
        string msg = "";
        foreach (KeyValuePair<string, string> data in datas)
        {
            if (i == 0)
                msg += data.Key + "=" + data.Value;
            else
                msg += "&" + data.Key + "=" + data.Value;
            i++;
        }

        string md5 =Token.GetMD5(_privateKey);//ok
        string signature =Token.CreateToken(msg, md5).ToLower();
        datas.Add("signature", signature);
        Post(api, datas);
        System.Net.WebClient wc = new System.Net.WebClient();
        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        var url = "https://www.jubi.com/api/v1/" + api;
        string param = msg + "&signature=" + signature;
        try
        {

            var info = wc.UploadString(url, param);
            Debug.Log(Time.time);
            return info;
        }
        catch
        {
            return "{\"error\":\"api error\"}";
        }
    }

    public void Post(string api, Dictionary<string, string> data, Action<WWW> result=null)
    {
        WWWForm form = new WWWForm();
        foreach (string key in data.Keys)
        {
            form.AddField(key, data[key].ToString());
        }
        var url = "https://www.jubi.com/api/v1/" + api;
        StartCoroutine(PostWWW(url, form, result));
    }

    IEnumerator PostWWW(string url, WWWForm form, Action<WWW> result)
    {
        var www = new WWW(url, form);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            if (result != null)
                result(www);
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("post error+" + www.error.ToString() + "==url==" + url);
        }
    }
}
