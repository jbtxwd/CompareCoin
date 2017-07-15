using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatForm
{
	public Dictionary<string, Depth> depths = new Dictionary<string, Depth>();//深度数据
	public List<string> concerCoins = new List<string>();
    protected bool isConcer = true;
    public string platFormName = "";
    public virtual void Initial()
    {
        
    }

    public virtual void AddChannel(string _coin)
    {
        if (!concerCoins.Contains(_coin))
            concerCoins.Add(_coin);
    }

    public virtual void RemoveChannel(string _coin)
    {
        concerCoins.Remove(_coin);
    }

    public virtual void SetConcer(bool _isConcer)
    {
        isConcer = _isConcer;
    }

    public virtual string GetCoinName(string _par)
    {
        if (_par.Contains(Coins.BTC.ToLower()))
            return Coins.BTC;
        if (_par.Contains(Coins.BTS.ToLower()))
            return Coins.BTS;
        if (_par.Contains(Coins.LTC.ToLower()))
            return Coins.LTC;
        if (_par.Contains(Coins.ETC.ToLower()))
            return Coins.ETC;
        if (_par.Contains(Coins.ETH.ToLower()))
            return Coins.ETH;
        return "";
    }
}