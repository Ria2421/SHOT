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

public class UIManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// シーン名
    /// </summary>
    [SerializeField] private string sceneName = "";

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // NetworkManager取得
        NetworkManager networkManager = NetworkManager.Instance;

#if UNITY_EDITOR
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Addressables.LoadScene("Stage" + networkManager.PlayStageNo.ToString(), LoadSceneMode.Additive);
#else
        // UIシーンの追加
        Addressables.LoadScene("Stage" + networkManager.PlayStageNo.ToString(), LoadSceneMode.Additive);
#endif
    }

    /// <summary>
    /// リプレイ処理
    /// </summary>
    public void gameReplay()
    {
        // シーンの再読み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ホーム遷移処理
    /// </summary>
    public void transitionHome()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }

    /// <summary>
    /// ステージ選択遷移処理
    /// </summary>
    public void transitionSelect()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("StageSelectScene", Color.gray, 2.5f);
    }
}
