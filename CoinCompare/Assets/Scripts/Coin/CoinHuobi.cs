using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using System;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
public class SubModel
{
    public SubModel(string _sub,long _id)
    {
        sub = _sub;
        id = _id;
    }
    public string sub;
    public long id;
}
public class ReqModel
{
    public ReqModel(string _req, long _id)
    {
        req = _req;
        id = _id;
    }

    public string req;
    public long id;
}

public class Ping
{
    public long ping;
}
public class Pong
{
	public long pong;
}

public class TickDetail
{
    public string ch;
    public string ts;
    public Tick tick;
}

public class Tick
{
    public List<List<float>> bids;
    public List<List<float>> asks;
}


//public class Bids
//{
   
//}

//public class Asks
//{
   
//}


public class CoinHuobi
{

    const string socketBE = "wss://be.huobi.com/ws";//ETH、ETC Websocket行情请求地址
    const string socketAPI = "wss://api.huobi.com/ws";//BTC、LTC Websocket
    Dictionary<string, Depth> depths = new Dictionary<string, Depth>();//深度数据
    List<string> keys = new List<string>();
    WebSocket webSocketBE;
    public CoinHuobi()
    {

    }

    public void Initial()
    {
        webSocketBE = new WebSocket(new Uri(socketBE));
        webSocketBE.OnOpen += OnWebSocketBEOpen;
        webSocketBE.OnBinary += OnBEBinaryMessageReceived;
        webSocketBE.Open();

        //TickDetail _Td = new TickDetail();
        //_Td.ch = "fuck";
        //_Td.ts = "trtr";
        //_Td.tick = new Tick();
        //List<float> l1 = new List<float>();
        //l1.Add(1);
        //l1.Add(2);
        //List<List<float>> l2 = new List<List<float>>();
        //l2.Add(l1);
        //_Td.tick.asks = l2;
        //string final = JsonConvert.SerializeObject(_Td);
        //Debug.Log(final);
        //_Td.tick.asks = new float[][];
        //TradeInfo ti = new TradeInfo();
        //ti.count = 100;
        //ti.price = 90.0f;
        //_Td.tick.asks.Add(ti);
        //_Td.tick.bids.Add(ti);
        //string final = JsonConvert.SerializeObject(_Td);
        //Debug.Log(final);
    }

    void OnWebSocketBEOpen(WebSocket _ws)
    {
        
        SubModel _sm = new SubModel("market.ethcny.depth.step1", 10086);
        string _json = JsonUtility.ToJson(_sm);
        _ws.Send(_json);
    }

    private void OnBEBinaryMessageReceived(WebSocket webSocket, byte[] message)
    {
        GZipInputStream gzi = new GZipInputStream(new MemoryStream(message));
        MemoryStream re = new MemoryStream();
        int count = 0;
        byte[] data = new byte[4096];
        while ((count = gzi.Read(data, 0, data.Length)) != 0)
        {
            re.Write(data, 0, count);
        }
        byte[] depress = re.ToArray();
        string _result = Encoding.UTF8.GetString(depress);
        Debug.Log("time==" + Time.time + _result);
        //string _final = "";
        ///*     string _final = StringToJson(_result); */;

        //Dictionary<string, object> _jsonData = JsonConvert.DeserializeObject(_final) as Dictionary<string, object>;
        //Debug.Log(_jsonData.Count);
        if (_result.Contains("ping"))
        {
            Ping _ping = JsonUtility.FromJson<Ping>(_result);
            Pong _pong = new Pong();
            _pong.pong = _ping.ping;
            string _json = JsonUtility.ToJson(_pong);
            webSocket.Send(_json);
        }
        else
        {
            //TickDetail _Td = JsonConvert.DeserializeObject<TickDetail>(_result); // JsonUtility.FromJson<TickDetail>(_result);
            TickDetail _Td = JsonUtility.FromJson<TickDetail>(_result);
            if (!string.IsNullOrEmpty(_Td.ch))
            {
                Debug.Log(_Td.tick.asks[0][0]+"**"+ _Td.tick.asks[0][1]);
                //Debug.Log(_Td.tick.asks[0][1]);
            }
            //Debug.Log("time==" + Time.time + _result);
        }
    }

    void OnDestroy()
    {
        webSocketBE.Close();
    }
}