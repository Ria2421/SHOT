//---------------------------------------------------------------
//
// クリエイトホームマネージャー [ CreateHomeManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHomeManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// シーン移動処理
    /// </summary>
    public void PushButton(string sceneName)
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade(sceneName, Color.black, 1.5f);
    }
}
