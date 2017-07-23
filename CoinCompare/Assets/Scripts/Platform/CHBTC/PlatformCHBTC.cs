using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using Newtonsoft.Json;
public class PlatFormCHBTCDepthRequest
{
    public string fucker;
	public string channel;
}

public class DepthCHBTC
{
    public List<List<float>> asks { get; set; }
    public List<List<float>> bids { get; set; }
    public string channel { get; set; }
    public string date { get; set; }
    public int no { get; set; }
}

public class PlatformCHBTC : PlatForm, ICoin
{
    const string socketURL = "wss://api.chbtc.com:9999/websocket";
    WebSocket webSocket;

    public override void Initial()
    {
        if (isConcer)
        {
            webSocket = new WebSocket(new System.Uri(socketURL));
            webSocket.OnOpen += OnWebServerOpen;
            webSocket.OnError += OnWebServerError;
            webSocket.OnClosed += OnWebSocketClosed;
            webSocket.OnMessage += OnMessageReceived;
            webSocket.Open();
            this.concerCoins.Add(Coins.BTC);
            this.concerCoins.Add(Coins.BTS);

            this.concerCoins.Add(Coins.ETH);
            this.concerCoins.Add(Coins.ETC);
            GetAccountInfo();
        }
        this.platFormName = "中国比特币";
    }

    void OnWebServerOpen(WebSocket _ws)
    {
        Debug.Log(this.platFormName + "连接成功");
        for (int i = 0; i < concerCoins.Count; i++)
        {
            AddChannel(concerCoins[i]);
        }
    }

    public override void AddChannel(string _coin)
    {
        base.AddChannel(_coin);
        PlatFormCHBTCDepthRequest _ccr = new PlatFormCHBTCDepthRequest();
        _ccr.fucker = "addChannel";
        switch (_coin)
        {
            case Coins.BTC:
                _ccr.channel = "btc_cny_depth";
                break;
            case Coins.LTC:
                _ccr.channel = "ltc_cny_depth";
                break;
            case Coins.ETH:
                _ccr.channel = "eth_cny_depth";
                break;
            case Coins.ETC:
                _ccr.channel = "etc_cny_depth";
                break;
            case Coins.BTS:
                _ccr.channel = "bts_cny_depth";
                break;
        }
        string _json = JsonConvert.SerializeObject(_ccr);
        string _final = _json.Replace("fucker", "event");
        webSocket.Send(_final);
    }

    public override void RemoveChannel(string _coin)
    {
        base.AddChannel(_coin);
    }

    void OnMessageReceived(WebSocket _ws, string _s)
    {
        if (_s.Contains("asks") && _s.Contains("bids"))
            GetDepth(_s);
    }

    void GetDepth(string _s)
    {
        DepthCHBTC _json = JsonConvert.DeserializeObject<DepthCHBTC>(_s);
        Depth _dt = new Depth();
        _dt.time = long.Parse(_json.date);
        string _coinName = GetCoinName(_json.channel);
        List<Price> _bidList = new List<Price>();
        for (int i = 0; i < _json.bids.Count; i++)
        {
            Price _p = new Price();
            _p.price = _json.bids[i][0];
            _p.count = _json.bids[i][1];
            _bidList.Add(_p);
        }
        _dt.bids = _bidList;
        List<Price> _askList = new List<Price>();
        for (int i = 0; i < _json.asks.Count; i++)
        {
            Price _p = new Price();
            _p.price = _json.asks[i][0];
            _p.count = _json.asks[i][1];
            _askList.Add(_p);
        }
        _dt.asks = _askList;
        _dt.Sort();
        depths[_coinName] = _dt;
    }


    void OnWebServerError(WebSocket _ws, System.Exception _ex)
    {
        Debug.Log("onerror" + _ex.ToString());
    }

    private void OnWebSocketClosed(WebSocket _ws, System.UInt16 _code, string _message)
    {
        Debug.Log("OnWebSocketClosed");
    }

    //string accesskey = "10823b7c-bf93-4dd0-b5af-83dc487a9f7e";
    //string secretkey = "2ffdfa1e-ba2c-42c5-98fc-61679b8c30d4";
    //string baseURL = "https://trade.chbtc.com/api/";
    public void GetAccountInfo()
    {
        string _url = SignCHBTC.testGetAccountInfo();
        new BestHTTP.HTTPRequest(new System.Uri(_url), (_request, _response) =>
         {
             AccountInfoCHBTC _ai = JsonConvert.DeserializeObject<AccountInfoCHBTC>(_response.DataAsText);
             Debug.Log(_response.DataAsText);
             Debug.Log(_ai.result.totalAssets);
         }).Send();
    }
}