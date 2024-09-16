//---------------------------------------------------------------
//
// �X�e�[�W���L���N�G�X�g�N���X [ CreateStageRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/15
// Update:2024/09/15
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareStageRequest
{
    /// ���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// �X�e�[�WID
    [JsonProperty("stage_id")]
    public int StageID { get; set; }
}
