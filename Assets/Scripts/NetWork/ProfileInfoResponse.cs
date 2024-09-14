//--------------------------------------------------------------
//
// プロフィール情報レスポンスクラス [ ProfileInfoResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/13
// Update:2024/09/13
//
//--------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInfoResponse
{
    /// 総プレイ数
    [JsonProperty("icon_id")]
    public int IconID { get; set; }

    /// 総プレイ数
    [JsonProperty("play_cnt")]
    public int PlayCnt { get; set; }

    /// クリア数
    [JsonProperty("clear_cnt")]
    public int ClearCnt { get; set; }

    /// ステージ作成数
    [JsonProperty("create_cnt")]
    public int CreateCnt { get; set; }

    /// フォロー数
    [JsonProperty("follow_cnt")]
    public int FollowCnt { get; set; }

    // フォロワー数
    [JsonProperty("follower_cnt")]
    public int FollowerCnt { get; set; }
}
