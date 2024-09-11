//---------------------------------------------------------------
//
// クリエイトステージレスポンスクラス [ CreateStageResponse.cs ]
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
    // ギミック座標
    [JsonProperty("gimmick_pos")]
    public string GimmickPos { get; set; }
}
