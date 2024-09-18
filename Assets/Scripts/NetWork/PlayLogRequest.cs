//-----------------------------------------------------
//
// プレイログリクエストクラス [ PlayLogRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/18
// Update:2024/09/18
//
//-----------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLogRequest
{
    /// ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// ステージID
    [JsonProperty("stage_id")]
    public int StageID { get; set; }

    // ステージタイプ [1:ノーマル 2:クリエイト]
    [JsonProperty("stage_type")]
    public int StageType { get; set; }

    // クリア判定
    [JsonProperty("clear_flag")]
    public bool ClearFlag { get; set; }
}
