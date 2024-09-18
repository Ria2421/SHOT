//-----------------------------------------------------
//
// �v���C���O���N�G�X�g�N���X [ PlayLogRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/18
// Update:2024/09/18
//
//-----------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLogRequest
{
    /// ���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// �X�e�[�WID
    [JsonProperty("stage_id")]
    public int StageID { get; set; }

    // �X�e�[�W�^�C�v [1:�m�[�}�� 2:�N���G�C�g]
    [JsonProperty("stage_type")]
    public int StageType { get; set; }

    // �N���A����
    [JsonProperty("clear_flag")]
    public bool ClearFlag { get; set; }
}
