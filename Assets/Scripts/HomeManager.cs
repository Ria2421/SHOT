//---------------------------------------------------------------
//
// ホームマネージャー [ HomeManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/01
// Update:2024/08/01
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// メニュー表示ボタン
    /// </summary>
    [SerializeField] private GameObject upManuButton;

    /// <summary>
    /// メニュー非表示ボタン
    /// </summary>
    [SerializeField] private GameObject downManuButton;

    /// <summary>
    /// メニュー画面
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// アチーブメント画面
    /// </summary>
    [SerializeField] private GameObject achievementPanel;

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

    //=======================================================
    // シーン遷移

    //=======================================================
    // メニュー関連メソッド

    /// <summary>
    /// メニュー表示処理
    /// </summary>
    public void PushMenuUpButton()
    {
        // メニュー表示ボタンを非アクティブに
        upManuButton.SetActive(false);

        // メニュー非表示ボタンをアクティブに
        downManuButton.SetActive(true);

        // メニューを表示
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// メニュー非表示処理
    /// </summary>
    public void PushMenuDownButton()
    {
        // メニュー表示ボタンをアクティブに
        upManuButton.SetActive(true);

        // メニュー非表示ボタンを非アクティブに
        downManuButton.SetActive(false);

        // メニューを閉じる
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// メニュー閉じる処理
    /// </summary>
    public void PushCloseButton(GameObject menuPanel)
    {
        // 親オブジェクトを非アクティブに
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// 各アイコン押下処理
    /// </summary>
    public void PushMenuIconButton(GameObject iconPanel)
    {
        // アチーブメント画面の表示
        iconPanel.SetActive(true);
    }
}
