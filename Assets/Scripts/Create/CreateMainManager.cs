//---------------------------------------------------------------
//
// クリエイトマネージャー [ CreateMainManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMainManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// メニューパネル
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// 削除モードフラグプロパティ
    /// </summary>
    public bool DeleteModeFlag {  get; private set; }

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    private void Start()
    {
        // 削除モードオフ
        DeleteModeFlag = false;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

    }

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
    /// ホームボタン押下時
    /// </summary>
    public void PushHomeButton()
    {
        /* フェード処理 (黒)  
                        ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.black, 1.5f);
        
    }

    /// <summary>
    /// ゴミ箱ボタン押下処理
    /// </summary>
    public void PushDustBox(GameObject gameObject)
    {
        if (!DeleteModeFlag)
        {   // 削除モードOFFの時
            DeleteModeFlag = true;
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {   // 削除モードONの時
            DeleteModeFlag = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    /// <summary>
    /// ギミックボタン押下処理
    /// </summary>
    public void PushGimmickButton(GameObject gameObject)
    {
        Debug.Log(gameObject.name); // ギミック名の表示

        // Resourcesフォルダからギミックのオブジェクトを取得・生成
        GameObject obj = (GameObject)Resources.Load(gameObject.name);
        GameObject gimmick = Instantiate(obj,Vector3.zero,Quaternion.identity);

        gimmick.AddComponent<BoxCollider2D>();
        gimmick.AddComponent<ObjDrag>();
    }
}
