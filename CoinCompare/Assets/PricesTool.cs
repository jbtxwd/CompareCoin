using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace cptool
{
    public class PricesTool
    {
        public Dictionary<string,IPrice> prices = new Dictionary<string,IPrice>();
        Dictionary<string, int> cnt = new Dictionary<string, int>();

        public void Init()
        {
            Coin19800 coin19800 = new Coin19800();
            //coin19800.Init();
            prices["19800"] = coin19800;
            CoinJubi coinJubi = new CoinJubi();
            //coinJubi.Init();
            prices["聚币"] = coinJubi;
            foreach (var p in prices.Values)
            {
                foreach (var k in p.GetKeys())
                {
                    if (cnt.ContainsKey(k)) cnt[k]++;
                    else
                        cnt[k] = 1;
                }
            }

            var vs = new List<string>(cnt.Keys);
            foreach (var v in vs)
            {
                if (cnt[v] < 2)
                    cnt.Remove(v);
            }

            foreach (var p in prices.Values)
            {
                p.Init(vs);
            }
        }
        public string[] GetKeys()
        {
            return cnt.Keys.ToArray();
        }
        public string[] GetPrices()
        {
            return prices.Keys.ToArray();
        }
        public Info GetInfo(string price, string key)
        {
            return prices[price].GetInfo(key);
        }
        public InfoPack GetInfos(string key)
        {
            var p = GetPrices();
            InfoPack iss = new InfoPack(p.Length);
            for (var i = 0; i < iss.Count; i++)
            {
                iss[i] = prices[p[i]].GetInfo(key);
            }
            return iss;
        }
    }
}
