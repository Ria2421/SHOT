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
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class LookCreateStageManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// 情報プレハブ
    /// </summary>
    [SerializeField] private GameObject infoPrefab;

    /// <summary>
    /// 親オブジェ
    /// </summary>
    [SerializeField] private GameObject parentObj;

    /// <summary>
    /// ネットワークマネージャー格納用
    /// </summary>
    private NetworkManager networkManager;

    //-------------------------------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // ネットワークマネージャーの取得
        networkManager = NetworkManager.Instance;

        // ユーザーデータが保存されていない場合は登録
        StartCoroutine(NetworkManager.Instance.GetPlayerCreateStage(
            result =>
            {
                if (result != null)
                {
                    // データ取得官僚
                    Debug.Log("ステージ一覧取得");
                    foreach (var item in result)
                    {
                        // ステージ一覧の生成
                        GameObject info = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, parentObj.transform);
                        // 情報代入
                        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ID:" + item.ID.ToString();   // ID
                        info.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.Name;                    // ステージ名
                        info.transform.GetChild(2).gameObject.GetComponent<Text>().text = networkManager.GetUserName(); // ユーザー名
                        info.transform.GetChild(3).gameObject.GetComponent<Text>().text = item.GoodVol.ToString();      // イイネ数
                        // クリック時ステージ遷移
                        info.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            // データ保管用オブジェクトの生成
                            GameObject stageDataObject = new GameObject("StageDataObject");
                            stageDataObject.AddComponent<StageDataObject>();
                            DontDestroyOnLoad(stageDataObject);    // Scene遷移で破棄されなようにする

                            StartCoroutine(NetworkManager.Instance.GetIDCreate(
                                item.ID,
                                result =>
                                {
                                    Debug.Log("ステージデータ取得");
                                }));

                        });
                    }
                }
                else
                {
                    Debug.Log("取得失敗");
                }
            }));
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
