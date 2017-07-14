using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHuobi : MonoBehaviour
{
    CoinHuobi coinHuobi;
	void Start () 
    {
        coinHuobi = new CoinHuobi();
        coinHuobi.Initial();
	}

    private void OnDestroy()
    {
        coinHuobi.Close();
    }
}