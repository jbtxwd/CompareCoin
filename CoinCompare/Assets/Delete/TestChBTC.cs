using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using chbtc;
using System;
using Common;
public class TestChBTC : MonoBehaviour 
{
	void Start ()
    {
        CoinCHBTC cc = new CoinCHBTC();	
        cc.Initial();
	}

    // Update is called once per frame
    void Update()
    {
    }
}