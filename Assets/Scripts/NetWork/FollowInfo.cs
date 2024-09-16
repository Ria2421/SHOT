//--------------------------------------------
//
// �t�H���[���N���X [ FollowInfo.cs ]
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
    /// ���[�U�[ID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// �A�C�R��ID
    [JsonProperty("icon_id")]
    public int IconID { get; set; }

    /// ���[�U�[��
    [JsonProperty("name")]
    public string Name { get; set; }
}
