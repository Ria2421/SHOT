//-----------------------------------------------------------------------
//
// �N���G�C�g�X�e�[�W��񃌃X�|���X�N���X [ CreateStageInfoResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/11
// Update:2024/09/11
//
//-----------------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageInfoResponse
{
    /// �X�e�[�WID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// �X�e�[�W��
    [JsonProperty("name")]
    public string Name { get; set; }

    /// �X�e�[�W��
    [JsonProperty("icon_id")]
    public int IconID { get; set; }

    /// �쐬���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// �쐬���[�U�[��
    [JsonProperty("user_name")]
    public string UserName { get; set; }

    // �C�C�l��
    [JsonProperty("good_vol")]
    public int GoodVol { get; set; }
}
