//---------------------------------------------------------------
//
// グッド数更新リクエストクラス [ UpdateGoodRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/15
// Update:2024/09/15
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGoodRequest
{
    /// ステージID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// グッド数
    [JsonProperty("good_vol")]
    public int GoodVol { get; set; }
}
