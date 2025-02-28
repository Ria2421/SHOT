//---------------------------------------------------------------
//
// カスタムゲームマネージャー [ CustomGameManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/05
// Update:2024/09/11
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
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
    /// フォローボタン
    /// </summary>
    [SerializeField] private GameObject followButton;

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
            Instantiate(obj, new Vector3(data.X, data.Y, 0), Quaternion.Euler(0, 0, data.D));
        }
    }


    //================================
    // メニュー処理

    /// <summary>
    /// メニュー押下時
    /// </summary>
    public void PushMenuButton()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // メニューパネルを表示
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// 閉じるボタン押下時
    /// </summary>
    public void PushCloseButton()
    {
        SEManager.Instance.Play(SEPath.CANCEL);

        // メニューパネルを非表示
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// 戻る押下処理
    /// </summary>
    public void PushBackButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CustumSelectScene", Color.white, 2.5f);
    }

    /// <summary>
    /// リプレイ押下処理
    /// </summary>
    public void PushReplayButton()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // シーンの再読み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ホームボタン押下処理
    /// </summary>
    public void PushHomeButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("Raning", Color.white, 2.5f);
    }

    /// <summary>
    /// イイネボタン処理
    /// </summary>
    public void PushGoodButton()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

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
        SEManager.Instance.Play(SEPath.MENU_SELECT);

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

    /// <summary>
    /// フォローボタン処理
    /// </summary>
    public void PushFollowButton()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        followButton.GetComponent<Button>().interactable = false;    // ボタン無効化

        StartCoroutine(NetworkManager.Instance.RegistFollow(
            stageDataObject.GetCreatorID(),
            result =>
            {
                switch (result)
                {
                    case "200": // 登録成功
                        Debug.Log("登録完了");
                        followButton.GetComponent<Image>().color = Color.green;
                        break;

                    case "400": // 登録済
                        Debug.Log("登録済");
                        followButton.GetComponent<Image>().color = Color.green;
                        break;

                    case "404": // 指定IDが存在しない
                        Debug.Log("IDが存在しない");
                        followButton.GetComponent<Image>().color = Color.red;
                        break;

                    default:
                        followButton.GetComponent<Image>().color = Color.red;
                        break;
                }
            }));
    }
}
