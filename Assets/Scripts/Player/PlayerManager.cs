//---------------------------------------------------------------
//
// プレイヤーマネージャー [ PlayerManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/24
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;

public class PlayerManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// クリアリザルトパネル
    /// </summary>
    private GameObject clearResultPanel;

    /// <summary>
    /// ゲームオーバーリザルトパネル
    /// </summary>
    private GameObject gameOverPanel;

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
        physics = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;

        // リザルトパネルの取得・非表示
        clearResultPanel = GameObject.Find("GameClearPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");

        if (clearResultPanel != null && gameOverPanel != null)
        {   // nullチェック
            clearResultPanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // マウスの動きと反対方向に発射する処理

        if (Input.GetMouseButtonDown(0))
        {   // マウスクリック開始時
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
            direction.enabled = false;
            Flip(currentForce * 6f);
        }
    }

    /// <summary>
    /// 定期更新処理
    /// </summary>
    void FixedUpdate()
    {
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
        if (collision.gameObject.tag == "Finish")
        {   // ゴール判定

            // 成功ログ登録
            StorePlayLog(true);

            // クリアリザルトON
            clearResultPanel.SetActive(true);

            // 速度0に
            physics.velocity *= 0;
        }

        if (collision.gameObject.tag == "Thunder")
        {   // 雷判定

            // 失敗ログ登録
            StorePlayLog(false);

            // ゲームオーバーパネル表示
            gameOverPanel.SetActive(true);

            // 速度0に
            physics.velocity *= 0;
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
            }));
    }
}
