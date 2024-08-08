//---------------------------------------------------------------
//
// クリエイトマネージャー [ CreateMainManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMainManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// メニューパネル
    /// </summary>
    [SerializeField] private GameObject menuPanel;

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
    /// メニュー押下時
    /// </summary>
    public void PushMenuButton()
    {
        // メニューパネルを表示
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// 閉じるボタン押下時
    /// </summary>
    public void PushCloseButton()
    {
        // メニューパネルを非表示
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// ホームボタン押下時
    /// </summary>
    public void PushHomeButton()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.black, 1.5f);
        
    }
}
