//---------------------------------------------------------------
//
// タイトルマネージャー [ TitleManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/07/18
//
//---------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// ボタンコンポーネント保存用
    /// </summary>
    [SerializeField] private Button btn;

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
    ///  ステージ選択画面移行処理
    /// </summary>
    public void TransSelectScene()
    {
        // 一度押された場合はボタンを無効にする
        btn.interactable = false;

        // ユーザーデータの読込処理・結果を取得
        bool isSuccess = NetworkManager.Instance.LoadUserData();

        if (!isSuccess)
        {
            // ユーザーデータが保存されていない場合は登録
            StartCoroutine(NetworkManager.Instance.StoreUser(
                Guid.NewGuid().ToString(),  // ユーザー名
                result =>
                {
                    /* 画面遷移処理 */
                    Initiate.DoneFading();
                    Initiate.Fade("HomeScene", Color.black, 1.5f);
                }));
        }
        else {
            /* 画面遷移処理 */
            Initiate.DoneFading();
            Initiate.Fade("HomeScene", Color.black, 1.5f);
        }   
    }
}
