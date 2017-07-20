using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
public class TestHuobi : MonoBehaviour
{
    SignHuobi _signhuobi;
	void Start () 
    {
        _signhuobi = new SignHuobi(); ;
	}

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Debug.Log(_signhuobi.Accounts());
           HTTPRequest _hr= new HTTPRequest(new System.Uri(_signhuobi.Accounts()), (requset, response) =>
             {
                 Debug.Log(response.DataAsText);
             }).Send();
        }
    }
}