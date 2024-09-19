//---------------------------------------------------------------
//
// ノーマルステージレスポンスクラス [ NormalStageResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/27
// Update:2024/08/27
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageResponse
{
    /// ステージID
    [JsonProperty("id")]
    public int StageID { get; set; }

    /// 報酬アイテムID
    [JsonProperty("item_id")]
    public int ItemID { get; set; }

    // 報酬数
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}
