using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
public class TestChBTC : MonoBehaviour 
{
	void Start ()
    {
        PlatformCHBTC cc = new PlatformCHBTC();	
        cc.Initial();
	}

    // Update is called once per frame
    void Update()
    {
    }
}