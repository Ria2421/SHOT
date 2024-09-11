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
using UnityEngine.AddressableAssets;


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
                    // カタログ更新チェック
                    StartCoroutine(checkCatalog());
                }));
        }
        else {
            // カタログ更新チェック
            StartCoroutine(checkCatalog());
        }   
    }

    /// <summary>
    /// ステージカタログチェック
    /// </summary>
    /// <returns></returns>
    private IEnumerator checkCatalog()
    {
        // カタログデータが更新されているかをチェック
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var updates = checkHandle.Result;
        Addressables.Release(checkHandle);  // メモリの開放

        if(updates.Count >= 1)
        {   // 更新が1つ以上あった場合はロード画面へ
            Initiate.DoneFading();
            Initiate.Fade("LoadingScene", Color.white, 2.5f);
        }
        else
        {   // ない場合はホーム画面へ
            Initiate.DoneFading();
            Initiate.Fade("HomeScene", Color.white, 2.5f);
        }
    }
}
