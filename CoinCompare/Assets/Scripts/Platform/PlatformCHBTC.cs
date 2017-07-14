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
    }

	void OnWebServerError(WebSocket _ws, System.Exception _ex)
	{
		Debug.Log("onerror");
	}

	private void OnWebSocketClosed(WebSocket _ws, System.UInt16 _code, string _message)
	{
		Debug.Log("OnWebSocketClosed");
	}
}