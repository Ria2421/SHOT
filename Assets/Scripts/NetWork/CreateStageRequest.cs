//---------------------------------------------------------------
//
// �N���G�C�g�X�e�[�W���N�G�X�g�N���X [ CreateStageRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/06
// Update:2024/09/06
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageRequest
{
    /// �X�e�[�W��
    [JsonProperty("name")]
    public string Name { get; set; }

    /// �쐬���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    // �M�~�b�N���W
    [JsonProperty("gimmick_pos")]
    public string GimmickPos { get; set; }
}
