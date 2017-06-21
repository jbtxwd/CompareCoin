using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cptool;
public class CoinController : MonoBehaviour
{
    PriceTool ptool = new PriceTool();
    Dictionary<string, string> chance = new Dictionary<string, string>();
    // Use this for initialization
    void Start ()
    {
        ptool.Init();

        var keys = ptool.GetKeys();
        var ps = ptool.GetPrices();
        for (var i = 0; i < ptool.GetPrices().Length; i++)
        {
        }
        for (var i = 0; i < keys.Length; i++)
        {
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Tick();
    }

    void Tick()
    {
        var keys = ptool.GetKeys();

        foreach (var k in new List<string>(chance.Keys))
        {
            chance[k] = "-";
        }

        for (var i = 0; i < keys.Length; i++)
        {
            var ps = ptool.GetPrices();
            var pinfo = ptool.GetInfos(keys[i]);
            for (var j = 0; j < ps.Length; j++)
            {
                var pi = pinfo[j];
                if (pi == null) continue;
                var key = "price" + j;
                //this.dataGridView1.Rows[i].Cells[key].Value = pi.price + "(" + pi.buy + "/" + pi.sell + ")";

                var keyv = "vol" + j;
                //this.dataGridView1.Rows[i].Cells[keyv].Value = pi.vol;
            }
            //差价发现

            for (var x = 0; x < ps.Length; x++)
            {
                for (var y = x + 1; y < ps.Length; y++)
                {
                    if (x == y) continue;

                    if (pinfo[x] == null || pinfo[y] == null) continue;


                    if (pinfo[x].sell * 1.01 < pinfo[y].buy)
                    {
                        var key = pinfo.GetDesc() + ":" + ps[x] + "->" + ps[y];
                        var pb = pinfo[y].buy;
                        var pa = pinfo[x].sell * 1.01;
                        if (pa == 0) continue;
                        var seed = (pb - pa) / pa * 100.0;
                        chance[key] = "机会 " + seed.ToString("0.##") + "% " + pa.ToString("0.####") + "->" + pb.ToString("0.####");
                    }
                    else if (pinfo[x].sell * 1.00 < pinfo[y].buy)
                    {
                        var key = pinfo.GetDesc() + ":" + ps[x] + "->" + ps[y];
                        var pb = pinfo[y].buy;
                        var pa = pinfo[x].sell * 1.01;
                        if (pa == 0) continue;
                        var seed = (pb - pa) / pa * 100.0;
                        chance[key] = "---- " + seed.ToString("0.##") + "% " + pa.ToString("0.####") + "->" + pb.ToString("0.####");
                    }
                    else if (pinfo[x].buy > pinfo[y].sell * 1.01)
                    {
                        var key = pinfo.GetDesc() + ":" + ps[y] + "->" + ps[x];
                        var pb = pinfo[x].buy;
                        var pa = pinfo[y].sell * 1.01;
                        if (pa == 0) continue;
                        var seed = (pb - pa) / pa * 100.0;
                        chance[key] = "机会 " + seed.ToString("0.##") + "% " + pa.ToString("0.####") + "->" + pb.ToString("0.####");
                    }
                    else if (pinfo[x].buy > pinfo[y].sell * 1.00)
                    {
                        var key = pinfo.GetDesc() + ":" + ps[y] + "->" + ps[x];
                        var pb = pinfo[x].buy;
                        var pa = pinfo[y].sell * 1.01;
                        if (pa == 0) continue;
                        var seed = (pb - pa) / pa * 100.0;
                        chance[key] = "---- " + seed.ToString("0.##") + "% " + pa.ToString("0.####") + "->" + pb.ToString("0.####");
                    }
                }
            }

        }


        //整理
        var list = new List<string>(chance.Keys);
        var listout = new List<string>(chance.Keys);
        for (var i = 0; i < chance.Keys.Count; i++)
        {
            var ckey = list[i];
            if (chance[ckey] == "-")
            {
                listout[i] = "-" + ckey + ":" + chance[ckey];
            }
            else if (chance[ckey][0] == '-')
            {
                listout[i] = "=" + ckey + ":" + chance[ckey];
            }
            else
            {
                listout[i] = " " + ckey + ":" + chance[ckey];
            }
        }
        listout.Sort();

        for (int i=0; i < listout.Count; i++)
        {
            Debug.Log(listout[i]);
        }
        //输出
        //while (listBox1.Items.Count < listout.Count)
        //{
        //    listBox1.Items.Add("");
        //}
        //while (listBox1.Items.Count > listout.Count)
        //{
        //    listBox1.Items.RemoveAt(0);
        //}

        //for (var i = 0; i < listout.Count; i++)
        //{

        //    listBox1.Items[i] = listout[i];
        //}

    }
}
