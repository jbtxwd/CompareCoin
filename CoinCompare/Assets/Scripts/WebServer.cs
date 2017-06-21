using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;

namespace Common
{
    public class WebServer : MonoBehaviour
    {
        private static WebServer m_instance;
        public static WebServer instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject go = new GameObject("WebServer");
                    m_instance = go.AddComponent<WebServer>();
                }
                return m_instance;
            }
        }
        private static string AddressAssetsUrl = "http://localhost:9000";
        //private static string AddressAssetsUrl = "http://123.206.210.77:9000";

        public void Post(string page, string method, Hashtable data, Action<WWW> result)
        {
            WWWForm form = new WWWForm();
            foreach (string key in data.Keys)
            {
                form.AddField(key, data[key].ToString());
            }
            string url = AddressAssetsUrl + (String.IsNullOrEmpty(page) ? "" : "/" + page) + (String.IsNullOrEmpty(method) ? "" : "/" + method);
            StartCoroutine(PostWWW(url, form, result));
        }

        public void PostFile(string page, string method, Hashtable data, byte[] file, Action<WWW> result)
        {
            WWWForm form = new WWWForm();
            foreach (string key in data.Keys)
            {
                form.AddField(key, data[key].ToString());
            }
            form.AddBinaryData("file", file);
            string url = AddressAssetsUrl + (String.IsNullOrEmpty(page) ? "" : "/" + page) + (String.IsNullOrEmpty(method) ? "" : "/" + method);
            StartCoroutine(PostWWW(url, form, result));
        }

        IEnumerator PostWWW(string url, WWWForm form, Action<WWW> result)
        {
            var www = new WWW(url, form);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                if (result != null)
                    result(www);
            }
            else
            {
                Debug.Log("post error+" + www.error.ToString() + "==url==" + url);
            }
        }

        public void Get(string page, string method, Action<WWW> result)
        {
            string url = AddressAssetsUrl + (page != "" ? "/" + page : "") + (method != "" ? "/" + method + ".aspx" : "");
            StartCoroutine(GetWWWW(url, result));
        }

        IEnumerator GetWWWW(string url, Action<WWW> result)
        {
            var wwww = new WWW(url);
            yield return wwww;
            if (string.IsNullOrEmpty(wwww.error))
            {
                if (result != null)
                    result(wwww);
            }
            else
            {
                Debug.Log("get error+" + wwww.error.ToString() + "==url==" + url);
            }
        }
    }
}