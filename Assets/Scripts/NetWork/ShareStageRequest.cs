//---------------------------------------------------------------
//
// ステージ共有リクエストクラス [ CreateStageRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/15
// Update:2024/09/15
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareStageRequest
{
    /// ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// ステージID
    [JsonProperty("stage_id")]
    public int StageID { get; set; }
}
