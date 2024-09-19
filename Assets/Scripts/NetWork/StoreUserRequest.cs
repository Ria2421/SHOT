//---------------------------------------------------------------
//
// ユーザー登録リクエストクラス [ StoreUserRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/26
// Update:2024/08/26
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUserRepuest
{
    [JsonProperty("name")]
    public string Name {  get; set; }
}
