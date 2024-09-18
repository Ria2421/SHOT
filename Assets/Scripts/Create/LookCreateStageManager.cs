//---------------------------------------------------------------
//
// 作成ステージ一覧 [ LookCreateStageManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/08
// Update:2024/08/08
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
    /// アイコンスプライト
    /// </summary>
    [SerializeField] private List<Sprite> iconSprits;

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

        // ステージオブジェクトを検索
        GameObject stageDataObject = GameObject.Find("StageDataObject");

        if(stageDataObject != null)
        {
            // ステージデータのリセット
            stageDataObject.GetComponent<StageDataObject>().ResetData();
        }
        else
        {
            // データ保管用オブジェクトの生成
            stageDataObject = new GameObject("StageDataObject");
            stageDataObject.AddComponent<StageDataObject>();
            DontDestroyOnLoad(stageDataObject);    // Scene遷移で破棄されなようにする
        }

        // 自作ステージ一覧の取得
        StartCoroutine(NetworkManager.Instance.GetPlayerCreateStage(
            result =>
            {
                if (result != null)
                {
                    // データ取得完了
                    Debug.Log("ステージ一覧取得");
                    foreach (var data in result)
                    {
                        // ステージ一覧の生成
                        GameObject info = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, parentObj.transform);
                        // ステージ情報代入
                        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ID:" + data.ID.ToString();    // ID
                        info.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.Name;                     // ステージ名
                        info.transform.GetChild(2).gameObject.GetComponent<Text>().text = networkManager.GetUserName();  // ユーザー名
                        info.transform.GetChild(3).gameObject.GetComponent<Text>().text = data.GoodVol.ToString();       // イイネ数
                        info.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = iconSprits[data.IconID - 1];// アイコン設定
                        // クリック時ステージ遷移
                        info.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            StartCoroutine(NetworkManager.Instance.GetIDCreate(
                                data.ID,
                                result =>
                                {
                                    // JSONデシリアライズ
                                    var resultData = JsonConvert.DeserializeObject<List<GimmickData>>(result.GimmickPos);
                                    stageDataObject.GetComponent<StageDataObject>().SetData(data.ID,data.UserID,resultData,data.GoodVol);
                                    Debug.Log("ステージデータ取得");

                                    /* フェード処理 (白)  
                                        ( "シーン名",フェードの色, 速さ);  */
                                    Initiate.DoneFading();
                                    Initiate.Fade("CreatePlayScene", Color.white, 2.5f);
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
        var stageDataObject = GameObject.Find("StageDataObject");
        // ステージデータオブジェの削除
        if(stageDataObject != null)
        {
            Destroy(stageDataObject);
        }

        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.gray, 1.5f);
    }
}
