//---------------------------------------------------------------
//
// プレイヤーマネージャー [ PlayerManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/24
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;
using static UnityEditor.PlayerSettings;

public class PlayerManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// クリアリザルトパネル
    /// </summary>
    private GameObject clearResultPanel = null;

    /// <summary>
    /// ゲームオーバーリザルトパネル
    /// </summary>
    private GameObject gameOverPanel = null;

    /// <summary>
    /// クリアフラグ
    /// </summary>
    private bool clearFlag;

    //======================================
    // 反射予測線用

    ///// <summary>
    ///// wallLayerを指定
    ///// </summary>
    [SerializeField] private LayerMask wallLayer;

    /// <summary>
    /// 物理剛体
    /// </summary>
    private Rigidbody2D physics = null;

    /// <summary>
    /// 発射方向
    /// </summary>
    [SerializeField]
    private LineRenderer direction = null;

    /// <summary>
    /// 最大付与力量
    /// </summary>
    private const float MaxMagnitude = 4f;

    /// <summary>
    /// 発射方向の力
    /// </summary>
    private Vector3 currentForce = Vector3.zero;

    /// <summary>
    /// メインカメラ
    /// </summary>
    private Camera mainCamera = null;

    /// <summary>
    /// メインカメラ座標
    /// </summary>
    private Transform mainCameraTransform = null;

    /// <summary>
    /// ドラッグ開始点
    /// </summary>
    private Vector3 dragStart = Vector3.zero;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 起動処理
    /// </summary>
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "CreateMainScene")
        {
            BGMManager.Instance.Play(BGMPath.GAME);

            // リザルトパネルの取得
            var panel = GameObject.Find("Panel");   // UIパネルの取得
            clearResultPanel = panel.transform.Find("GameClearPanel").gameObject;
            gameOverPanel = panel.transform.Find("GameOverPanel").gameObject;
        }

        // フラグの初期化
        clearFlag = false;

        physics = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if(clearFlag) { return; }

        // マウスの動きと反対方向に発射する処理

        if (Input.GetMouseButtonDown(0))
        {   // マウスクリック開始時
            SEManager.Instance.Play(SEPath.SHOT_CHARGE);

            dragStart = GetMousePosition();

            direction.enabled = true;

            direction.SetPosition(0, physics.position);
            direction.SetPosition(1, physics.position);
        }
        else if (Input.GetMouseButton(0))
        {   // クリックホールド時
            var position = GetMousePosition();

            currentForce = position - dragStart;

            if (currentForce.magnitude > MaxMagnitude)
            {
                currentForce *= MaxMagnitude / currentForce.magnitude;
            }

            direction.SetPosition(0, physics.position);
            direction.SetPosition(1, physics.position + new Vector2(-currentForce.x, -currentForce.y));
        }
        else if (Input.GetMouseButtonUp(0))
        {   // マウスクリックを離した時
            SEManager.Instance.Play(SEPath.SHOT);

            direction.enabled = false;
            Flip(currentForce * 6f);
        }
    }

    /// <summary>
    /// 定期更新処理
    /// </summary>
    void FixedUpdate()
    {
        if (clearFlag) { return; }
        physics.velocity *= 0.95f;
    }

    /// <summary>
    /// マウス座標をワールド座標に変換して取得
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMousePosition()
    {
        // マウスから取得できないZ座標を補完する
        var position = Input.mousePosition;
        position.z = mainCameraTransform.position.z;
        position = mainCamera.ScreenToWorldPoint(position);
        position.z = 0;

        return position;
    }

    /// <summary>
    /// ボールをはじく
    /// </summary>
    /// <param name="force"></param>
    public void Flip(Vector3 force)
    {
        // 瞬間的に力を加えてはじく
        physics.AddForce(-force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 判定接触時の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (clearFlag) { return; }

        SEManager.Instance.Play(SEPath.OBJ_BUTUKARI);

        if (collision.gameObject.tag == "Finish")
        {   // ゴール判定
            SEManager.Instance.Play(SEPath.CLEAR,0.7f);

            // 成功ログ登録
            StorePlayLog(true);

            // クリアリザルトON
            clearResultPanel.SetActive(true);

            // 速度0に
            physics.velocity *= 0;

            clearFlag = true;
        }
        else if (collision.gameObject.tag == "Trap")
        {   // トラップ判定
            SEManager.Instance.Play(SEPath.FAILED);

            // 失敗ログ登録
            StorePlayLog(false);

            // ゲームオーバーパネル表示
            gameOverPanel.SetActive(true);

            // 速度0に
            physics.velocity *= 0;

            clearFlag = true;
        }
    }

    /// <summary>
    /// トリガー判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (clearFlag) { return; }

        SEManager.Instance.Play(SEPath.OBJ_BUTUKARI);

        if (collision.gameObject.tag == "Trap")
        {   // 罠判定

            // 失敗ログ登録
            StorePlayLog(false);

            // ゲームオーバーパネル表示
            gameOverPanel.SetActive(true);

            // 速度0に
            physics.velocity *= 0;

            clearFlag = true;
        }
    }

    /// <summary>
    /// プレイログ登録処理
    /// </summary>
    /// <param name="stageID">ステージID</param>
    /// <param name="type">   [1:ノーマル 2:クリエイト]</param>
    /// <param name="flag">   クリアフラグ</param>
    private void StorePlayLog(bool flag)
    {
        string name = SceneManager.GetActiveScene().name;   // シーン名取得
        int stageID = 0;
        int type = 0;

        switch (name)
        {
            case "UIScene":
                stageID = GameObject.Find("GameManager").GetComponent<GameManager>().GetStageNo();
                type = 1;
                break;

            case "CustomGameScene":
                stageID = GameObject.Find("StageDataObject").GetComponent<StageDataObject>().GetID();
                type = 2;
                break;

            default:
                break;
        }

        // プレイログ登録API呼び出し
        StartCoroutine(NetworkManager.Instance.StorePlayLog(
            stageID,
            type,
            flag,
            result =>
            {
                Debug.Log("ログ登録完了");
                Destroy(GetComponent<Rigidbody2D>());
            }));
    }
}
