//---------------------------------------------------------------
//
// カスタムゲームマネージャー [ CustomGameManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/05
// Update:2024/09/11
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomGameManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// ギミック格納用の親オブジェクト
    /// </summary>
    [SerializeField] private GameObject parentObj;

    /// <summary>
    /// メニューパネル
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// イイネボタン
    /// </summary>
    [SerializeField] private GameObject goodButton;

    /// <summary>
    /// 共有ボタン
    /// </summary>
    [SerializeField] private GameObject shareButton;

    /// <summary>
    /// ネットワークマネージャー
    /// </summary>
    private NetworkManager networkManager;

    /// <summary>
    /// ステージデータオブジェクト
    /// </summary>
    private StageDataObject stageDataObject;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        networkManager = NetworkManager.Instance;
        // ステージデータの受け取り・配置
        stageDataObject = GameObject.Find("StageDataObject").GetComponent<StageDataObject>();
        var stageDatas = stageDataObject.GetStageData();
        foreach (GimmickData data in stageDatas)
        {
            // Resourcesフォルダからギミックのオブジェクトを取得・生成
            GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
            Instantiate(obj, new Vector3(data.X, data.Y, 0), Quaternion.identity);
        }
    }


    //================================
    // メニュー処理

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
    /// 戻る押下処理
    /// </summary>
    public void PushBackButton()
    {
        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CustumStageSelect", Color.white, 2.5f);
    }

    /// <summary>
    /// リプレイ押下処理
    /// </summary>
    public void PushReplayButton()
    {
        // シーンの再読み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //========================
    // リザルト処理

    /// <summary>
    /// イイネボタン処理
    /// </summary>
    public void PushGoodButton()
    {
        goodButton.GetComponent<Button>().interactable = false;    // ボタン無効化

        // イイネ更新処理
        StartCoroutine(NetworkManager.Instance.UpdateGood(
            stageDataObject.GetID(),
            stageDataObject.GetGood() + 1,
            result =>
            {
                if (result)
                {
                    goodButton.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    goodButton.GetComponent<Image>().color = Color.red;
                }
            }));
    }

    /// <summary>
    /// 共有ボタン処理
    /// </summary>
    public void PushShareButton()
    {
        shareButton.GetComponent<Button>().interactable = false;    // ボタン無効化

        // イイネ更新処理
        StartCoroutine(NetworkManager.Instance.ShereStage(
            stageDataObject.GetID(),
            result =>
            {
                if (result)
                {
                    shareButton.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    shareButton.GetComponent<Image>().color = Color.red;
                }
            }));
    }

    // ホームボタン押下処理
    public void PushHomeButton()
    {
        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.white, 2.5f);
    }
}
