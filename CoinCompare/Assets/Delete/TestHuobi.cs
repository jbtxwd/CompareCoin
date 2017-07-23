using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using Newtonsoft.Json;
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
            //Debug.Log(_signhuobi.AccountBalance());
           HTTPRequest _hr= new HTTPRequest(new System.Uri(_signhuobi.AccountBalance("361951")), (requset, response) =>
             {
                 Debug.Log(response.DataAsText);
                 AccountInfoHuobi _aic = JsonConvert.DeserializeObject<AccountInfoHuobi>(response.DataAsText);
                 Debug.Log(_aic.data.list[0].currency);
             }).Send();
        }
    }
}