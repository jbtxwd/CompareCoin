using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormManager : MonoBehaviour
{
    private static PlatFormManager _instance;
    public static PlatFormManager instance
    {
        get { return _instance; }
    }
    public List<PlatForm> platForms = new List<PlatForm>();
    private void Awake()
    {
        _instance = this;
    }

    void Start ()
    {
        PlatformCHBTC _platFormCHBTC = new PlatformCHBTC();
        _platFormCHBTC.Initial();
     
        PlatformHuobi _platformHuobi = new PlatformHuobi();
        _platformHuobi.Initial();

        platForms.Add(_platFormCHBTC);
        platForms.Add(_platformHuobi);
    }

    private void Update()
    {
        GetChance(Coins.BTC, platForms[0], platForms[1]);
        GetChance(Coins.BTC, platForms[1], platForms[0]);

        GetChance(Coins.BTS, platForms[0], platForms[1]);
        GetChance(Coins.BTS, platForms[1], platForms[0]);

        GetChance(Coins.ETH, platForms[0], platForms[1]);
        GetChance(Coins.ETH, platForms[1], platForms[0]);

        GetChance(Coins.ETC, platForms[0], platForms[1]);
        GetChance(Coins.ETC, platForms[1], platForms[0]);
    }

    public void GetChance(string _coinName, PlatForm _sellPlatForm, PlatForm _buyPlatForm)
    {
        if (_sellPlatForm.depths.ContainsKey(_coinName) && _buyPlatForm.depths.ContainsKey(_coinName))
        {
            List<Price> _sell = _sellPlatForm.depths[_coinName].bids;
            List<Price> _buy = _buyPlatForm.depths[_coinName].asks;
            if (_sell[0].price > _buy[0].price)
            {
                float _count = _sell[0].count > _buy[0].count ? _buy[0].count : _sell[0].count;
                float _earn = (_sell[0].price - _buy[0].price) * _count;
                Debug.Log("虚拟币=" + _coinName + "卖价=" + _sell[0].price + "买价=" + _buy[0].price + "卖方=" + _sellPlatForm.platFormName + "买方=" + _buyPlatForm.platFormName
                + "收益=" + ((_sell[0].price - _buy[0].price) / _buy[0].price)+"预计收益="+ _earn);
            }
        }
        else
        {
            Debug.Log("不兑"+ _coinName);
        }
    }
}
