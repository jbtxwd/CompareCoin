using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin
{
	public Dictionary<string, Depth> depths = new Dictionary<string, Depth>();//深度数据
	public List<string> concerCoins = new List<string>();
    protected bool isConcer = true;
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
}