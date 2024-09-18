//---------------------------------------------------------------
//
// フォローリクエストクラス [ FollowRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/17
// Update:2024/09/17
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRequest
{
    /// ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// フォローID
    [JsonProperty("follow_id")]
    public int FollowID { get; set; }
}
