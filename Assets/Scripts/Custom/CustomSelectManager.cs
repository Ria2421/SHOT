//---------------------------------------------------------------
//
// カスタムセレクトマネージャー [ CustomSelectManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/26
// Update:2024/09/11
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSelectManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// ステージ情報格納プレハブ
    /// </summary>
    [SerializeField] private GameObject stageInfoPrefab;

    /// <summary>
    /// データなし表示プレハブ
    /// </summary>
    [SerializeField] private GameObject noDataPrefab;

    /// <summary>
    /// リスト切り替えボタン
    /// </summary>
    [SerializeField] private List<GameObject> listButtons;

    /// <summary>
    /// 切り替えボタンImageリスト
    /// </summary>
    [SerializeField] private List<Image> buttonColors;

    /// <summary>
    /// スクロールリスト    [0:フォロー 1:イイネ 2:共有]
    /// </summary>
    [SerializeField] private List<GameObject> scrollList;

    /// <summary>
    /// スクロールコンテンツ
    /// </summary>
    [SerializeField] private List<GameObject> scrolltContents;

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

        if (stageDataObject != null)
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

        // カスタムリストの取得
        StartCoroutine(NetworkManager.Instance.GetCustomList(
            result =>
            {
                for(int i = 0; i < 3; i++)
                {
                    if (result[i].Count == 0)
                    {   // データ無し表示
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[i].transform);
                        continue;
                    }

                    foreach (var data in result[i])
                    {
                        // ステージ一覧の生成
                        GameObject info = Instantiate(stageInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[i].transform);
                        // ステージ情報代入
                        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ID:" + data.ID.ToString();   // ID
                        info.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.Name;                    // ステージ名
                        info.transform.GetChild(2).gameObject.GetComponent<Text>().text = data.UserName;                // ユーザー名
                        info.transform.GetChild(3).gameObject.GetComponent<Text>().text = data.GoodVol.ToString();      // イイネ数
                        // クリック時ステージ遷移
                        info.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            StartCoroutine(NetworkManager.Instance.GetIDCreate(
                                data.ID,
                                result =>
                                {
                                    // JSONデシリアライズ
                                    var resultData = JsonConvert.DeserializeObject<List<GimmickData>>(result.GimmickPos);
                                    if (resultData == null) { return; }  // nullチェック
                                    stageDataObject.GetComponent<StageDataObject>().SetData(data.ID, resultData, data.GoodVol);
                                    Debug.Log("ステージデータ取得");

                                    /* フェード処理 (白)  
                                        ( "シーン名",フェードの色, 速さ);  */
                                    Initiate.DoneFading();
                                    Initiate.Fade("CustomGameScene", Color.white, 2.5f);
                                }));
                        });
                    }
                }

                Debug.Log("リスト生成完了");

                foreach(GameObject button in listButtons)
                {   // ボタンの有効化
                    button.GetComponent<Button>().interactable = true;
                }

                SetList(0); // フォローリストの有効化
            }));
    }

    /// <summary>
    /// リスト・ボタンの表示初期化
    /// </summary>
    private void ResetList()
    {
        // ボタン・スクロールウィンドウ初期化
        for (int i = 0; i < listButtons.Count; i++)
        {
            scrollList[i].SetActive(false);         // 全リストを非表示
            buttonColors[i].color = Color.white;    // カラー白
        }
    }

    /// <summary>
    /// 指定Noのリストを表示
    /// </summary>
    /// <param name="num"></param>
    public void SetList(int no)
    {
        ResetList();   // 初期化処理
        scrollList[no].SetActive(true); 
        buttonColors[no].color = Color.gray;   
    }

    /// <summary>
    /// ホーム遷移処理
    /// </summary>
    public void PushBackButton()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.white, 2.5f);
    }
}
