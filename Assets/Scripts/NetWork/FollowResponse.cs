//-------------------------------------------------
//
// フォローレスポンスクラス [ FollowResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/16
// Update:2024/09/16
//
//-------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowResponse
{
    /// フォロー情報
    [JsonProperty("follow")]
    public List<FollowInfo> Follow { get; set; }

    /// フォロワー情報
    [JsonProperty("follower")]
    public List<FollowInfo> Follower { get; set; }
}
