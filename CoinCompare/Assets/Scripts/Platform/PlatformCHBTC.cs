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

public class CHBTCDepthDetail
{
	public string channel;
	public string date;
	public Tick tick;
}

public class CHBTCTick
{
	public List<List<float>> bids;
	public List<List<float>> asks;
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
        }
    }

    void OnWebServerOpen(WebSocket _ws)
    {
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
        Debug.Log(_s);
        //string final = _s.Replace(@"","");
        PlayerPrefs.SetString("fuck",_s);

        string _fuck =_s.Replace(@"\\\",@"\");
        Debug.Log(_fuck);
        var _json = JsonConvert.DeserializeObject(_fuck) as Dictionary<string, object>;
        Debug.Log(_json.Keys.Count);
        /*if(_json.ContainsKey("channel")&& _json["channel"].ToString().Contains("depth"))
        {
            Debug.Log("---------");
            GetDepth(_s);
        }*/
    }

    void GetDepth(string _s)
    {
        CHBTCDepthDetail _chdd = JsonConvert.DeserializeObject<CHBTCDepthDetail>(_s);
        Debug.Log(_chdd.tick.asks.Count);
    }

	void OnWebServerError(WebSocket _ws, System.Exception _ex)
	{
        Debug.Log("onerror" + _ex.ToString());
	}

	private void OnWebSocketClosed(WebSocket _ws, System.UInt16 _code, string _message)
	{
		Debug.Log("OnWebSocketClosed");
	}
}