//---------------------------------------------------------------
//
// ステージセレクトマネージャー [ SelectManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/07/27
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // フィールド

    /// <summary>
    /// 生成するボタンのプレハブ
    /// </summary>
    [SerializeField] private GameObject buttonPrefub;

    /// <summary>
    /// 親オブジェクト (スクロールビュー)
    /// </summary>
    [SerializeField] private Transform scrollView;

    /// <summary>
    /// スクロールビューオブジェクト
    /// </summary>
    [SerializeField] private GameObject scrollObj;

    /// <summary>
    /// エラー表示オブジェ
    /// </summary>
    [SerializeField] private GameObject errorObj;

    //-------------------------------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // ノーマルステージ情報を取得
        StartCoroutine(NetworkManager.Instance.GetNormalStage(
            result =>
            {
                if (result != null)
                {   // ステージデータがある時

                    scrollObj.SetActive(true);  // スクロールウィンドウ有効化

                    // NetworkManagerを取得
                    NetworkManager networkManager = NetworkManager.Instance;

                    foreach (NormalStageResponse stageData in result)
                    {
                        // プレハブからオブジェクトの生成
                        GameObject selectBtn = Instantiate(buttonPrefub, Vector3.zero, Quaternion.identity, scrollView);

                        // ボタンのテキストにステージIDを反映
                        selectBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = stageData.StageID.ToString();

                        // 生成したボタンにクリック時の処理を追加
                        selectBtn.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            // NetworkManagerにStageNo・Typeを保存
                            networkManager.PlayStageNo = stageData.StageID;
                            networkManager.PlayStageType = 1;

                            /* フェード処理 (黒)  
                                         ( "シーン名",フェードの色, 速さ);  */
                            Initiate.DoneFading();
                            Initiate.Fade("UIScene", Color.gray, 2.5f);
                        });
                    }
                }
                else
                {
                    // エラー表示
                    errorObj.SetActive(true);
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
    /// ステージ選択処理
    /// </summary>
    public void PushStageSelect(string buttonNum)
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // ログにてステージNoを表示
        Debug.Log(buttonNum);

        if (buttonNum != "")
        {
            /* フェード処理 (黒)  
                         ( "シーン名",フェードの色, 速さ);  */
            Initiate.DoneFading();
            Initiate.Fade("Stage" + buttonNum + "Scene", Color.gray, 2.5f);
        }
    }

    /// <summary>
    /// 戻るボタン押下処理
    /// </summary>
    public void PushBackButton()
    {
        SEManager.Instance.Play(SEPath.CANCEL);

        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }
}
