//---------------------------------------------------------------
//
// カスタムゲームマネージャー [ CustomGameManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/05
// Update:2024/09/11
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //--------------------------------------------
    // メソッド

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

    //++ イイネボタン処理

    //++ 共有ボタン処理

    // ホームボタン押下処理
    public void PushHomeButton()
    {
        /* フェード処理 (白)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.white, 2.5f);
    }
}
