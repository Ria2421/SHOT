//---------------------------------------------------------------
//
// メニュードラッグ移動用スクリプト [ MenuDrag.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/03
// Update:2024/09/03
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDrag : MonoBehaviour, IDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ドラッグ移動処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ座標の取得
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(eventData.position);
        TargetPos.z = 0;

        // 対象の座標を更新
        transform.position = eventData.position;
    }
}
