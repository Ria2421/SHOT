//--------------------------------------------------------------
//
// �v���t�B�[����񃌃X�|���X�N���X [ ProfileInfoResponse.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/13
// Update:2024/09/13
//
//--------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileInfoResponse
{
    /// ���v���C��
    [JsonProperty("icon_id")]
    public int IconID { get; set; }

    /// ���v���C��
    [JsonProperty("play_cnt")]
    public int PlayCnt { get; set; }

    /// �N���A��
    [JsonProperty("clear_cnt")]
    public int ClearCnt { get; set; }

    /// �X�e�[�W�쐬��
    [JsonProperty("create_cnt")]
    public int CreateCnt { get; set; }

    /// �t�H���[��
    [JsonProperty("follow_cnt")]
    public int FollowCnt { get; set; }

    // �t�H�����[��
    [JsonProperty("follower_cnt")]
    public int FollowerCnt { get; set; }
}
