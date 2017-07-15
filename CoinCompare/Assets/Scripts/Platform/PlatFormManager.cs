using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormManager : MonoBehaviour
{
    private static PlatFormManager _instance;
    public static PlatFormManager instance
    {
        get { return _instance; }
    }
    public List<PlatForm> platForms = new List<PlatForm>();
    private void Awake()
    {
        _instance = this;
    }

    void Start ()
    {
        PlatformCHBTC _platFormCHBTC = new PlatformCHBTC();
        _platFormCHBTC.Initial();

        PlatformHuobi _platformHuobi = new PlatformHuobi();
        _platformHuobi.Initial();
    }

    public void GetChance(string _coinName)
    {

    }
}
