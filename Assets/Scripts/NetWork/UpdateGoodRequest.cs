//---------------------------------------------------------------
//
// �O�b�h���X�V���N�G�X�g�N���X [ UpdateGoodRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/15
// Update:2024/09/15
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGoodRequest
{
    /// �X�e�[�WID
    [JsonProperty("id")]
    public int ID { get; set; }

    /// �O�b�h��
    [JsonProperty("good_vol")]
    public int GoodVol { get; set; }
}
