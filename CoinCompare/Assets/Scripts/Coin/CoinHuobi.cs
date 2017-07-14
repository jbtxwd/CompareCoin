using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using System;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class SubModel
{
    public SubModel(string _sub, long _id)
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

public class CoinHuobi : Coin, ICoin
{
    const string socketAPI = "wss://api.huobi.com/ws";//BTC、LTC Websocket
    const string socketBE = "wss://be.huobi.com/ws";//ETH、ETC Websocket行情请求地址
    WebSocket webSocketBE;
    WebSocket webSocketAPI;

    public CoinHuobi()
    {

    }

    public void Initial()
    {
        concerCoins.Add(Coins.BTC);
        concerCoins.Add(Coins.LTC);
        concerCoins.Add(Coins.ETH);
        concerCoins.Add(Coins.ETC);
        webSocketAPI = new WebSocket(new Uri(socketAPI));
        webSocketAPI.OnOpen += OnWebSocketAPIOpen;
        webSocketAPI.OnBinary += OnBinaryMessageReceived;
        webSocketAPI.Open();

        webSocketBE = new WebSocket(new Uri(socketBE));
        webSocketBE.OnOpen += OnWebSocketBEOpen;
        webSocketBE.OnBinary += OnBinaryMessageReceived;
        webSocketBE.Open();
    }

    void OnWebSocketAPIOpen(WebSocket _ws)
    {
        if (concerCoins.Contains(Coins.BTC))
            AddCoinDepth(Coins.BTC);
        if (concerCoins.Contains(Coins.LTC))
            AddCoinDepth(Coins.LTC);
    }

    void OnWebSocketBEOpen(WebSocket _ws)
    {
        if (concerCoins.Contains(Coins.ETH))
            AddCoinDepth(Coins.ETH);
        if (concerCoins.Contains(Coins.ETC))
            AddCoinDepth(Coins.ETC);
    }

    public void AddCoinDepth(string _name)
    {
        concerCoins.Add(_name);
        switch (_name)
        {
            case Coins.BTC:
                if (webSocketAPI.IsOpen)
                {
                    SubModel _sm = new SubModel("market.btccny.depth.step1", 10001);
                    string _json = JsonUtility.ToJson(_sm);
                    webSocketAPI.Send(_json);
                }
                else
                {
                    Debug.Log("webSocketAPI not Opened!");
                }
                break;
            case Coins.LTC:
                if (webSocketAPI.IsOpen)
                {
                    SubModel _sm = new SubModel("market.ltccny.depth.step1", 10002);
                    string _json = JsonUtility.ToJson(_sm);
                    webSocketAPI.Send(_json);
                }
                else
                {
                    Debug.Log("webSocketAPI not Opened!");
                }
                break;
            case Coins.ETH:
                if (webSocketBE.IsOpen)
                {
                    SubModel _sm = new SubModel("market.ethcny.depth.step1", 10003);
                    string _json = JsonUtility.ToJson(_sm);
                    webSocketBE.Send(_json);
                }
                else
                {
                    Debug.Log("webSocketBE not Opened!");
                }
                break;
            case Coins.ETC:
                if (webSocketBE.IsOpen)
                {
                    SubModel _sm = new SubModel("market.etccny.depth.step1", 10004);
                    string _json = JsonUtility.ToJson(_sm);
                    webSocketBE.Send(_json);
                }
                else
                {
                    Debug.Log("webSocketBE not Opened!");
                }
                break;
        }
    }

    public void RemoveCoinDepth(string _name)
    {
        concerCoins.Remove(_name);
        switch (_name)
        {
            case Coins.BTC:
                break;
            case Coins.LTC:
                break;
            case Coins.ETC:
                break;
            case Coins.ETH:
                break;
        }
    }
    private void OnBinaryMessageReceived(WebSocket webSocket, byte[] message)
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
        else if (_dic.ContainsKey("tick"))
        {
            TickDetail _td = JsonConvert.DeserializeObject<TickDetail>(_result);
            if (!string.IsNullOrEmpty(_td.ch))
            {
                Debug.Log(_result);
                string _coinName = "";
                switch (_td.ch)
                {
                    case "market.ethcny.depth.step1":
                        _coinName = Coins.ETH;
                        break;
                    case "market.etccny.depth.step1":
                        _coinName = Coins.ETC;
                        break;
                    case "market.ltccny.depth.step1":
                        _coinName = Coins.LTC;
                        break;
                    case "market.btccny.depth.step1":
                        _coinName = Coins.BTC;
                        break;
                }
                Depth _dt = new Depth();
                List<Price> _bidList = new List<Price>();
                for (int i = 0; i < _td.tick.bids.Count; i++)
                {
                    Price _p = new Price();
                    _p.price = _td.tick.bids[i][0];
                    _p.count = _td.tick.bids[i][1];
                    _bidList.Add(_p);
                }
                _dt.bids = _bidList;
                List<Price> _askList = new List<Price>();
                for (int i = 0; i < _td.tick.asks.Count; i++)
                {
                    Price _p = new Price();
                    _p.price = _td.tick.asks[i][0];
                    _p.count = _td.tick.asks[i][1];
                    _askList.Add(_p);
                }
                _dt.asks = _askList;
                _dt.time = long.Parse(_dic["ts"].ToString());
                depths[_coinName] = _dt;
            }
        }
    }

    public void Close()
    {
        webSocketAPI.Close();
        webSocketBE.Close();
    }
}