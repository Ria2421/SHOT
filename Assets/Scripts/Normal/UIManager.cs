//---------------------------------------------------------------
//
// UIマネージャー [ UIManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/29
// Update:2024/09/04
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using KanKikuchi.AudioManager;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// シーン名
    /// </summary>
    [SerializeField] private string sceneName = "";

    /// <summary>
    /// メニューパネル
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// ネクストボタン
    /// </summary>
    [SerializeField] private Button nextButton;

    private NetworkManager networkManager;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 起動処理
    /// </summary>
    void Awake()
    {
        // NetworkManager取得
        networkManager = NetworkManager.Instance;

        if(networkManager.PlayStageNo == networkManager.LastStageNo)
        {   // ラストステージの時はネクストボタンを無効化
            nextButton.interactable = false;
        }

#if UNITY_EDITOR
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Addressables.LoadScene("Stage" + networkManager.PlayStageNo.ToString(), LoadSceneMode.Additive);
#else
        // UIシーンの追加
        Addressables.LoadScene("Stage" + networkManager.PlayStageNo.ToString(), LoadSceneMode.Additive);
#endif
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
    /// 戻る押下処理
    /// </summary>
    public void PushBackButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("StageSelectScene", Color.white, 2.5f);
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
    /// ネクストボタン押下処理
    /// </summary>
    public void PushNextButton()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // 次のステージ名を取得後、移動
        networkManager.PlayStageNo = GameObject.Find("GameManager").GetComponent<GameManager>().GetStageNo() + 1;
        Initiate.Fade("UIScene", Color.white, 2.5f);
    }
}
