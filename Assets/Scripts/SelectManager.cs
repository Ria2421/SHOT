//---------------------------------------------------------------
//
// ステージセレクトマネージャー [ SelectManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/07/18
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
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
    /// ステージ選択処理
    /// </summary>
    public void PushStageSelect(string buttonNum)
    {
        // ログにてステージNoを表示
        Debug.Log(buttonNum);

        /* フェード処理 (黒)  
                         ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade( "Stage" + buttonNum +"Scene", Color.gray, 2.5f);
    }
}
