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
using UnityEngine.Serialization;
using Utility;

public class PlayerManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// 自機の移動速度量
    /// </summary>
    [SerializeField] float playerSpeed;

    /// <summary>
    /// クリアリザルトパネル
    /// </summary>
    [SerializeField] GameObject clearResultPanel;

    /// <summary>
    /// ゲームオーバーリザルトパネル
    /// </summary>
    [SerializeField] GameObject gameOverPanel;

    /// <summary>
    /// 自機の当たり判定
    /// </summary>
    private Rigidbody2D rigid2d;
    
    /// <summary>
    /// 自機のスピード
    /// </summary>
    private float speed = 0.0f;

    /// <summary>
    /// 指(マウス)のタップ開始位置
    /// </summary>
    private Vector2 startPos = Vector2.zero;

    //======================================
    // 反射予測線用

    /// <summary>
    /// 発射方向
    /// </summary>
    private Vector2 launchDirection;

    /// <summary>
    /// ドラッグ開始位置を取得する
    /// </summary>
    private Vector2 dragStart = Vector2.zero;

    /// <summary>
    /// 反射予測線の最大値
    /// </summary>
    [SerializeField] private float maxMagnitude;

    /// <summary>
    /// 予測線の描画
    /// </summary>
    [SerializeField] private LineRenderer predictionLineRenderer;

    /// <summary>
    /// wallLayerを指定
    /// </summary>
    [SerializeField] private LayerMask wallLayer;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 起動処理
    /// </summary>
    private void Awake()
    {
        //オブジェクトの位置を取得するためにリジッドボディの取得
        rigid2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // 自機の速度設定
        this.speed = playerSpeed;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // マウスの動きと反対方向に発射する処理

        if (Input.GetMouseButtonDown(0))
        {   // マウスクリック開始時

            // クリック座標を保存
            this.startPos = Input.mousePosition;

            //ドラッグの開始位置をワールド座標で取得する
            dragStart = GetMousePosition();

            //描画線の予測を有効にする
            predictionLineRenderer.enabled = true;

        }
        else if (Input.GetMouseButton(0))
        {   // クリックホールド時

            //ドラッグ中のマウスの位置をワールド座標で取得する。
            var position = GetMousePosition();

            //ドラッグ開始点からの距離を取得する
            var currentForce = dragStart - position;

            // MaxMagnitudeに直線の長さの制限を指定しておきそれを超える場合は、最大値となるようにします。
            if (currentForce.magnitude > maxMagnitude)
            {
                currentForce *= maxMagnitude / currentForce.magnitude;
            }

            // 予測線を描画する
            DrawLineOfReflection(currentForce);
        }
        else if (Input.GetMouseButtonUp(0))
        {   // マウスクリックを離した時

            // 離した位置を保存
            Vector2 endPos = Input.mousePosition;

            // 引っ張り方向とは逆のベクトルを計算し、正規化
            Vector2 startDirection = (startPos - endPos).normalized;

            // スピード定数 * 計算した力の向き
            this.rigid2d.AddForce(startDirection * speed);

            // 予測線描画終了
            predictionLineRenderer.enabled = false;
        }

        // テスト用：スペースキー押下で停止
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rigid2d.velocity *= 0;
        }
    }

    /// <summary>
    /// 判定接触時の処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {   // ゴール判定

            // クリアリザルトON
            clearResultPanel.SetActive(true);

            // 速度0に
            this.rigid2d.velocity *= 0;

        }

        if (collision.gameObject.tag == "Thunder")
        {   // 雷判定

            // ゲームオーバーパネル表示
            gameOverPanel.SetActive(true);

            // プレイヤー破棄
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ワールド座標のマウスの場所を取得
    /// </summary>
    /// <returns>マウスポジション</returns>
    private Vector2 GetMousePosition()
    {
        // マウスの場所を取得
        Vector2 position = Input.mousePosition;

        // ワールド座標に変換
        return Camera.main.ScreenToWorldPoint(position);
    }

    /// <summary>
    /// 反射予測線の描画
    /// </summary>
    /// <param name="currentForce">反射予測線の方向と大きさ</param>
    private void DrawLineOfReflection(Vector2 currentForce)
    {
        var poses = Physics2DUtil.RefrectionLinePoses(rigid2d.position, currentForce.normalized, maxMagnitude, wallLayer).ToArray();
        predictionLineRenderer.positionCount = poses.Length;
        for (var i = 0; i < poses.Length; i++)
        {
            predictionLineRenderer.SetPosition(i, poses[i]);
        }
    }
}
