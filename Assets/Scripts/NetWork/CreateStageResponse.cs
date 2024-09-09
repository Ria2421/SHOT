//---------------------------------------------------------------
//
// �N���G�C�g�X�e�[�W���X�|���X�N���X [ CreateStageResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/09
// Update:2024/09/09
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageResponse
{
    /// �X�e�[�WID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// �X�e�[�W��
    [JsonProperty("name")]
    public string Name { get; set; }

    /// �쐬���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    // �M�~�b�N���W
    [JsonProperty("gimmick_pos")]
    public string GimmickPos { get; set; }

    // �C�C�l��
    [JsonProperty("good_vol")]
    public int GoodVol { get; set; }
}
