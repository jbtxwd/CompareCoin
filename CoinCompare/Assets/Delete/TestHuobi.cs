using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHuobi : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CoinHuobi ch = new CoinHuobi();
        ch.Initial();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
