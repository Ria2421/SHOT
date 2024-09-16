//-----------------------------------------------------------------------
//
// クリエイトステージ情報レスポンスクラス [ CreateStageInfoResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/11
// Update:2024/09/11
//
//-----------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageInfoResponse
{
    /// ステージID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// ステージ名
    [JsonProperty("name")]
    public string Name { get; set; }

    /// ステージ名
    [JsonProperty("icon_id")]
    public int IconID { get; set; }

    /// 作成ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// 作成ユーザー名
    [JsonProperty("user_name")]
    public string UserName { get; set; }

    // イイネ数
    [JsonProperty("good_vol")]
    public int GoodVol { get; set; }
}
