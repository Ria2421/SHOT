//---------------------------------------------------------------
//
// �m�[�}���X�e�[�W���X�|���X�N���X [ NormalStageResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/27
// Update:2024/08/27
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageResponse
{
    /// �X�e�[�WID
    [JsonProperty("id")]
    public int StageID { get; set; }

    /// ��V�A�C�e��ID
    [JsonProperty("item_id")]
    public int ItemID { get; set; }

    // ��V��
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}
