//---------------------------------------------------------------
//
// クリエイトステージレスポンスクラス [ CreateStageResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/09
// Update:2024/09/09
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageResponse
{
    /// ステージID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// ステージ名
    [JsonProperty("name")]
    public string Name { get; set; }

    /// 作成ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    // ギミック座標
    [JsonProperty("gimmick_pos")]
    public string GimmickPos { get; set; }

    // イイネ数
    [JsonProperty("good_vol")]
    public int GoodVol { get; set; }
}
