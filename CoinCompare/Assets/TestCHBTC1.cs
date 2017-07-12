using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCHBTC1 : MonoBehaviour {
    CoinCHBTC chbtc;
	// Use this for initialization
	void Start ()
    {
        chbtc = new CoinCHBTC();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("k"))
        {
            chbtc.UpdateDepths();
        }
		
	}
}
