//---------------------------------------------------------------
//
// ユーザー登録レスポンスクラス [ StoreUserResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/26
// Update:2024/08/26
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUserResponse
{
    [JsonProperty("user_id")]
    public int UserID { get; set; }
}