//---------------------------------------------------------------
//
// カスタムゲームマネージャー [ CustomGameManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/05
// Update:2024/08/05
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomGameManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    //--------------------------------------------
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
    /// リプレイ処理
    /// </summary>
    public void gameReplay()
    {
        // シーンの再読み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ホーム遷移処理
    /// </summary>
    public void transitionHome()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }

    /// <summary>
    /// ステージ選択遷移処理
    /// </summary>
    public void transitionSelect()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CustumStageSelect", Color.gray, 2.5f);
    }
}
