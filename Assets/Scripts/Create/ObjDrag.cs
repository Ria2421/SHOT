//---------------------------------------------------------------
//
// オブジェドラッグ移動用スクリプト [ ObjDrag.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/03
// Update:2024/09/03
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjDrag : MonoBehaviour
{
    //---------------------------------
    // フィールド

    /// <summary>
    /// タッチ時のスクリーン座標
    /// </summary>
    private Vector3 screenPoint;

    private CreateMainManager mainManager;

    //---------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    void Start()
    {
        // マネージャーの取得
        mainManager = GameObject.Find("CreateMainManager").GetComponent<CreateMainManager>();
    }

    /// <summary>
    /// ドラッグ処理
    /// </summary>
    void OnMouseDrag()
    {
        if (mainManager.DeleteModeFlag)
        {   // 削除モードの時
            Destroy(this.gameObject); 
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
   if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))return;
#endif

        // タッチ時のワールド座標をスクリーン座標に変換
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // 現在のスクリーン座標を取得
        float screenX = Input.mousePosition.x;
        float screenY = Input.mousePosition.y;
        float screenZ = screenPoint.z;

        Vector3 currentScreenPoint = new Vector3(screenX, screenY, screenZ);

        // ワールド座標に変換後、反映
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = currentPosition;
    }
}
