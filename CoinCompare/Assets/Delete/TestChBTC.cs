using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using chbtc;
using System;
using Common;
public class TestChBTC : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k"))
        {
            string s = chbtc_api.testGetAccountInfo();
            CoroutineManager.instance.StartCoroutine(PostWWW(s, null));
            Debug.Log(s);
        }
	}

    IEnumerator PostWWW(string url, Action<WWW> result)
    {
        var www = new WWW(url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
            if (result != null)
                result(www);
        }
        else
        {
            Debug.Log("post error+" + www.error.ToString() + "==url==" + url);
        }
    }
}
