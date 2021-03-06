﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Text;
public class PlatformSettingEditor 
{
    [MenuItem("Tool/PlatformSetting")]
    // Use this for initialization
    static void InitialPlatformSetting () 
    {
        List<PlatformSetting> _psList = new List<PlatformSetting>();

        PlatformSetting _ps19800 = new PlatformSetting();
        _ps19800.platformName = "19800";
        _ps19800.key = "1";
        _ps19800.security = "4";
        _psList.Add(_ps19800);

		PlatformSetting _psjubi = new PlatformSetting();
		_psjubi.platformName = "jubi";
		_psjubi.key = "1";
		_psjubi.security = "4";
		_psList.Add(_psjubi);

        string _json= JsonConvert.SerializeObject(_psList);
        Debug.Log(_json);
        CommonTool.CreateFile(Application.persistentDataPath,"platformSettings.json",_json);
        Debug.Log(CommonTool.ReadFile(Application.persistentDataPath,"platformSettings.json"));

	}
}
