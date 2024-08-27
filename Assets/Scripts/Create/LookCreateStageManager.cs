//---------------------------------------------------------------
//
// 作成ステージ一覧 [ LookCreateStageManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/08
// Update:2024/08/08
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCreateStageManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド


    //-------------------------------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// 戻るボタン押下処理
    /// </summary>
    public void PushBackButton()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.gray, 1.5f);
    }
}
