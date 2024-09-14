//---------------------------------------------------------------
//
// ���O�ύX���N�G�X�g�N���X [ NameChangeRequest.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/14
// Update:2024/09/14
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameChangeRequest
{
    /// �쐬���[�U�[ID
    [JsonProperty("user_id")]
    public int UserID { get; set; }

    /// ���[�U�[��
    [JsonProperty("name")]
    public string Name { get; set; }
}
