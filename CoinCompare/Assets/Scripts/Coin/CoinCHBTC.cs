using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using cptool;
using System.Linq;
using Common;
using BestHTTP;
public class CoinCHBTC
{
    const string baseUrl = "http://api.chbtc.com/";
    Dictionary<string, Info> infos = new Dictionary<string, Info>();
    Dictionary<string, Depth> depths = new Dictionary<string, Depth>();//深度数据
    List<string> keys = new List<string>();
    public CoinCHBTC()
    {
        keys.Add("btc_cny");
        keys.Add("ltc_cny");
        keys.Add("eth_cny");
        keys.Add("etc_cny");
        keys.Add("bts_cny");
        keys.Add("eos_cny");
    }

    string DepthUrl(string _coinName)
    {
        return baseUrl + "data/v1/depth?currency=" + _coinName + "&size=3&merge=0.1";
    }

    public void UpdateDepths()
    {
        List<string> _keys = GetKeys();
        for (int i = 0; i < _keys.Count; i++)
        {
            GetCoinDepth(_keys[i]);
        }
    }

    public void GetCoinDepth(string _coinName)
    {
        string _url = DepthUrl(_coinName);
        new HTTPRequest(new Uri(_url), (reqest, response) =>
        {
            var _jsonData = SimpleJSON.JSON.Parse(response.DataAsText);
            var _asks = _jsonData["asks"];
            var _bids = _jsonData["bids"];
            Debug.Log(_asks);
            Depth _depth = new Depth();
            List<Price> _asksList = new List<Price>();
            for (int i = 0; i < _asks.Count; i++)
            {
                Price _price = new Price();
                _price.price = float.Parse(_asks[i][0].Value.ToString());
                _price.count = float.Parse(_asks[i][1].Value.ToString());
                _asksList.Add(_price);
            }
            _depth.asks = _asksList;

            List<Price> _bidsList = new List<Price>();
            for (int i = 0; i < _bids.Count; i++)
            {
                Price _price = new Price();
                _price.price = float.Parse(_bids[i][0].Value.ToString());
                _price.count = float.Parse(_bids[i][1].Value.ToString());
                _bidsList.Add(_price);
            }
            _depth.bids = _bidsList;
            Debug.Log(_coinName);
            depths[_coinName] = _depth;
        }).Send();
    }

    public List<string> GetKeys()
    {
        return keys;
    }

    public Info GetInfo(string _s)
    {
        return null;
    }

    public Depth GetDepth(string _coinName)
    {
        return depths[_coinName];
    }
}
