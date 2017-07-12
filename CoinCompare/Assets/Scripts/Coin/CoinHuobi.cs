using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.WebSocket;
using System;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.Text;
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

public class CoinHuobi
{

    const string socketBE = "wss://be.huobi.com/ws";//ETH、ETC Websocket行情请求地址
    const string socketAPI = "wss://api.huobi.com/ws";//BTC、LTC Websocket
    Dictionary<string, Depth> depths = new Dictionary<string, Depth>();//深度数据
    List<string> keys = new List<string>();

    public CoinHuobi()
    {

    }

    public void Initial()
    {
        var _webSocketBE = new WebSocket(new Uri(socketBE));
        _webSocketBE.OnOpen += OnWebSocketBEOpen;
        _webSocketBE.OnMessage += OnMessageReceived;
        _webSocketBE.OnBinary += OnBinaryMessageReceived;

        _webSocketBE.Open();

    }

    void OnWebSocketBEOpen(WebSocket _ws)
    {
        SubModel _sm = new SubModel("market.ethcny.kline.1min", 10086);
        string _json = JsonUtility.ToJson(_sm);
        _ws.Send(_json);
        Debug.Log(_json);
    }

    private void OnMessageReceived(WebSocket webSocket, string message)
    {
        Debug.Log("Text Message received from server: " + message);
    }

    private void OnBinaryMessageReceived(WebSocket webSocket, byte[] message)
    {
        Debug.Log("Binary Message received from server. Length: " + message.Length);

        GZipInputStream gzi = new GZipInputStream(new MemoryStream(message));

        MemoryStream re = new MemoryStream();
        int count = 0;
        byte[] data = new byte[4096];
        while ((count = gzi.Read(data, 0, data.Length)) != 0)
        {
            re.Write(data, 0, count);
        }
        byte[] depress = re.ToArray();
        Debug.Log(Encoding.UTF8.GetString(depress));
    }
}
