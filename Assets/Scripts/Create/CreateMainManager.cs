//---------------------------------------------------------------
//
// クリエイトマネージャー [ CreateMainManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class CreateMainManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// メニューパネル
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// ゴミ箱画像(閉)
    /// </summary>
    [SerializeField] private Sprite closeBox;

    /// <summary>
    /// ゴミ箱画像(開)
    /// </summary>
    [SerializeField] private Sprite openBox;

    /// <summary>
    /// Player設置注意オブジェ
    /// </summary>
    [SerializeField] private RectTransform cautionPlayer;

    /// <summary>
    /// Goal設置注意オブジェ
    /// </summary>
    [SerializeField] private RectTransform cautionGoal;

    /// <summary>
    /// クリエイトデータ格納用
    /// </summary>
    private List<GimmickData> createDatas = new List<GimmickData>();

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

        // ステージデータ保管用オブジェクトの存在確認
        GameObject check = GameObject.Find("StageDataObject");

        if (check == null)
        {   // データ保管用オブジェクトの生成
            GameObject stageDataObject = new GameObject("StageDataObject");
            stageDataObject.AddComponent<StageDataObject>();
            DontDestroyOnLoad(stageDataObject);    // Scene遷移で破棄されなようにする
        }
        else
        {   // データを元にギミックを配置
            var gimmckDatas = check.GetComponent<StageDataObject>().GetData();
            foreach(GimmickData data in gimmckDatas)
            {
                // Resourcesフォルダからギミックのオブジェクトを取得・生成
                GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
                GameObject gimmick = Instantiate(obj, new Vector3(data.X,data.Y,0), Quaternion.identity);

                // ドラッグ用コンポーネントの追加
                gimmick.AddComponent<BoxCollider2D>();
                gimmick.AddComponent<ObjDrag>();

                // タグを付与
                gimmick.tag = "Create";

                if (gimmick.name == "12(Clone)")
                {   // プレイヤー生成時に動かないようにコンポーネントを削除
                    Destroy(gimmick.GetComponent<PlayerManager>());
                }
            }
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// 注意書き表示処理
    /// </summary>
    /// <param name="cautionImage">移動対象</param>
    private void MoveCaution(RectTransform cautionImage)
    {
        cautionImage.DOAnchorPos(new Vector2(0f, cautionImage.anchoredPosition.y), 0.6f).SetEase(Ease.OutBack);
    }

    //=====================================
    // ボタン押下処理

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
            Image dustBoxImg = gameObject.GetComponent<Image>();
            dustBoxImg.sprite = openBox;    // 画像変更
            dustBoxImg.color = Color.red;   // 色変更
        }
        else
        {   // 削除モードONの時
            DeleteModeFlag = false;
            Image dustBoxImg = gameObject.GetComponent<Image>();
            dustBoxImg.sprite = closeBox;
            dustBoxImg.color = Color.white;
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

        // ドラッグ用コンポーネントの追加
        gimmick.AddComponent<BoxCollider2D>();
        gimmick.AddComponent<ObjDrag>();

        // タグを付与
        gimmick.tag = "Create";

        if(gimmick.name == "12(Clone)")
        {   // プレイヤー生成時に動かないようにコンポーネントを削除
            Destroy(gimmick.GetComponent<PlayerManager>());
            Destroy(gimmick.GetComponent<Rigidbody2D>());
        }
    }

    /// <summary>
    /// テストボタン押下処理
    /// </summary>
    public void PushTestPlay()
    {
        // プレイヤー・ゴール設置フラグ
        bool plFlag = false;
        bool glFlag = false;

        // Createタグのゲームオブジェクトをすべて取得する
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Create");

        // 押下処理
        foreach (GameObject obj in objs)
        {
            // obj名をIDに変換
            string name = obj.name.Replace("(Clone)", "");

            // 設置判定
            if(name == "10")
            {   // ゴール有
                glFlag = true;
            }else if(name == "12")
            {   // PL有
                plFlag = true;
            }

            // ギミックデータの作成
            GimmickData gimmickData = new GimmickData();
            gimmickData.ID = int.Parse(name);
            gimmickData.X = (float)Math.Round(obj.transform.position.x, 3); // 小数点は第３位まで
            gimmickData.Y = (float)Math.Round(obj.transform.position.y, 3);
            Debug.Log(gimmickData.ID + ":" + " x=" + gimmickData.X + " y=" +gimmickData.Y); // データの表示

            // リストに追加
            createDatas.Add(gimmickData);
        }

        // 各フラグ毎に注意書き表示・ステーじ情報初期化
        if(!plFlag && !glFlag)
        {   // 両方無い
            MoveCaution(cautionPlayer);
            MoveCaution(cautionGoal);
            createDatas.Clear();
            return;
        }else if (!plFlag)
        {   // PL無い
            MoveCaution(cautionPlayer);
            createDatas.Clear();
            return;
        }else if(!glFlag)
        {   // ゴール無い
            MoveCaution(cautionGoal);
            createDatas.Clear();
            return;
        }
        else
        {
            // ステージデータを保管オブジェに受け渡し
            var dataObj = GameObject.Find("StageDataObject").GetComponent<StageDataObject>();
            dataObj.SetData(createDatas);
        }

        /* フェード処理 (白)  
                ( "シーン名",フェードの色, 速さ);  */
        Initiate.DoneFading();
        Initiate.Fade("TestPlayScene", Color.white, 2.5f);
    }

    /// <summary>
    /// 注意押下処理
    /// </summary>
    public void PushCation(RectTransform rectTransform)
    {
        // 初期位置に移動
        rectTransform.anchoredPosition = new Vector2(1000.0f, rectTransform.anchoredPosition.y);
    }
}
