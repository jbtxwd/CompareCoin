﻿using System.Collections;
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
        if (_s.Contains("asks") && _s.Contains("bids"))
            GetDepth(_s);
        /*if(_json.ContainsKey("channel")&& _json["channel"].ToString().Contains("depth"))
        {
            Debug.Log("---------");
            GetDepth(_s);
        }*/
    }

    void GetDepth(string _s)
    {
        var _json = JsonConvert.DeserializeObject<DepthCHBTC>(_s);
        Debug.Log(_json.asks.Count);
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