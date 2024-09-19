//--------------------------------------------
//
// フォロー情報クラス [ FollowInfo.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/16
// Update:2024/09/16
//
//--------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowInfo
{
    /// ユーザーID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// アイコンID
    [JsonProperty("icon_id")]
    public int IconID { get; set; }

    /// ユーザー名
    [JsonProperty("name")]
    public string Name { get; set; }
}
