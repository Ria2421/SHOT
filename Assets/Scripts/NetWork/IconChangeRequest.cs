using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconChangeRequest
{
    /// �쐬���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// �A�C�R��ID
    [JsonProperty("icon_id")]
    public int IconID { get; set; }
}
