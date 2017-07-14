﻿using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using System;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
using Newtonsoft.Json;
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
        Dictionary<string, object> _dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(_result);
        if (_dic.ContainsKey("ping"))
        {
            long _ping = long.Parse(_dic["ping"].ToString());
            Pong _pong = new Pong();
            _pong.pong = _ping;
            string _json = JsonConvert.SerializeObject(_pong);
            webSocket.Send(_json);
        }
        else if(_dic.ContainsKey("tick"))
        {
            TickDetail _Td = JsonConvert.DeserializeObject<TickDetail>(_result);
            if (!string.IsNullOrEmpty(_Td.ch))
            {
                Debug.Log(_Td.tick.bids.Count);
                string _coinName="";
                switch (_Td.ch)
                {
                    case "market.ethcny.depth.step1":
                        _coinName = Coins.ETH;
                        break;
                }
                Depth _dt = new Depth();
                depths[_coinName] = _dt;
                //Debug.Log("卖=="+_Td.tick.asks[0][0]+"**"+ _Td.tick.asks[0][1]+ "买==" + _Td.tick.bids[0][0] + "**" + _Td.tick.bids[0][1]);
            }
        }
    }

    void OnDestroy()
    {
        webSocketBE.Close();
    }
}