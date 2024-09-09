//---------------------------------------------------------------
//
// 完成確認画面マネージャー [ ConfCreateManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/06
// Update:2024/09/09
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ConfCreateManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// 完成ボタンコンポーネント保存用
    /// </summary>
    [SerializeField] private Button compBtn;

    /// <summary>
    /// 戻るボタン
    /// </summary>
    [SerializeField] private Button backBtn;

    /// <summary>
    /// ステージ名入力フィールド
    /// </summary>
    [SerializeField] private InputField inputField;

    /// <summary>
    /// ステージデータオブジェ
    /// </summary>
    private StageDataObject stageDataObject;

    //-------------------------------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // ステージデータオブジェクトの入手
        stageDataObject = GameObject.Find("StageDataObject").GetComponent<StageDataObject>();
    }

    /// <summary>
    /// 完成処理
    /// </summary>
    public void PushCompButton()
    {
        // UIを無効化
        compBtn.interactable = false;
        backBtn.interactable = false;
        inputField.interactable = false;

        // ステージデータをjsonシリアライズ
        var data = stageDataObject.GetData();
        string json = JsonConvert.SerializeObject(data);

        // ユーザーデータが保存されていない場合は登録
        StartCoroutine(NetworkManager.Instance.StoreCreateStage(
            inputField.text,    // ユーザー名
            json,               // ステージデータ
            result =>
            {
                if (result)
                {
                    // 登録完了表示。表示内にホームへ戻るボタンを配置
                    Debug.Log("登録完了");
                    // ステージデータオブジェを破棄
                    Destroy(GameObject.Find("StageDataObject"));
                }
                else
                {
                    Debug.Log("登録失敗");
                }
            }));
    }

    /// <summary>
    /// 試遊モード遷移処理
    /// </summary>
    public void PushBackButton()
    {
        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("TestPlayScene", Color.white, 2.5f);
    }

    /// <summary>
    /// ホーム遷移処理
    /// </summary>
    public void PushHomeButton()
    {
        // ステージデータオブジェを破棄
        Destroy(GameObject.Find("StageDataObject"));

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.white, 2.5f);
    }
}
