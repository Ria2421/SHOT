//---------------------------------------------------------------
//
// プレイヤー [ Player.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/17
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float speed;

    /// <summary>
    /// 指(マウス)のタップ開始位置
    /// </summary>
    private Vector2 startPos;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // 自機のRigidbody2Dを取得
        this.rigid2d = GetComponent<Rigidbody2D>();

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
        {   // マウスクリック時

            // クリック座標を保存
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {   // マウスクリックを離した時

            // 離した位置を保存
            Vector2 endPos = Input.mousePosition;

            // 引っ張り方向とは逆のベクトルを計算し、正規化
            Vector2 startDirection = (startPos - endPos).normalized;

            // スピード定数 * 計算した力の向き
            this.rigid2d.AddForce(startDirection * speed);
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
}
