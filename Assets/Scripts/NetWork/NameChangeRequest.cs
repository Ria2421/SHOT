//---------------------------------------------------------------
//
// 名前変更リクエストクラス [ NameChangeRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/14
// Update:2024/09/14
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameChangeRequest
{
    /// 作成ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// ユーザー名
    [JsonProperty("name")]
    public string Name { get; set; }
}
