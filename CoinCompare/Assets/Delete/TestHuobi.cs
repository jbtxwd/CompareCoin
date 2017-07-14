using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHuobi : MonoBehaviour
{
    PlatformHuobi coinHuobi;
	void Start () 
    {
        coinHuobi = new PlatformHuobi();
        coinHuobi.Initial();
	}

    private void OnDestroy()
    {
        if(coinHuobi!=null)
            coinHuobi.Close();
    }
}