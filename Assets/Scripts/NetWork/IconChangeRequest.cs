using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconChangeRequest
{
    /// 作成ユーザーID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// アイコンID
    [JsonProperty("icon_id")]
    public int IconID { get; set; }
}
