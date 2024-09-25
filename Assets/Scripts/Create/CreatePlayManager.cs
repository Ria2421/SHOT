//---------------------------------------------------------------
//
// クリエイトプレイマネージャー [ CreatePlayManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/10
// Update:2024/09/10
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatePlayManager : MonoBehaviour
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
    /// 初期処理
    /// </summary>
    void Start()
    {
        // ステージデータの受け取り・配置
        var stageDatas = GameObject.Find("StageDataObject").GetComponent<StageDataObject>().GetStageData();
        foreach (GimmickData data in stageDatas)
        {
            // Resourcesフォルダからギミックのオブジェクトを取得・生成
            GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
            Instantiate(obj, new Vector3(data.X, data.Y, 0), Quaternion.Euler(0,0,data.D));
        }
    }

    /// <summary>
    /// セレクトボタン押下処理
    /// </summary>
    public void PushSelectButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("LookCreateStageScene", Color.white, 2.5f);
    }

    /// <summary>
    /// リプレイ処理
    /// </summary>
    public void PushGameReplay()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // シーンの再読み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ホーム遷移処理
    /// </summary>
    public void PushComplete()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("ConfCreateScene", Color.white, 2.5f);
    }

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
    /// バックボタン押下時
    /// </summary>
    public void PushBackButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateMainScene", Color.white, 1.5f);
    }
}
