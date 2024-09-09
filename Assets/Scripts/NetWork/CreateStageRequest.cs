//---------------------------------------------------------------
//
// クリエイトステージリクエストクラス [ CreateStageRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/06
// Update:2024/09/06
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageRequest
{
    /// ステージ名
    [JsonProperty("name")]
    public string Name { get; set; }

    /// 作成ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    // ギミック座標
    [JsonProperty("gimmick_pos")]
    public string GimmickPos { get; set; }
}
