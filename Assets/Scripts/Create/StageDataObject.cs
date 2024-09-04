//---------------------------------------------------------------
//
// ステージデータ保管スクリプト [ StageDataObject.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/04
// Update:2024/09/04
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataObject : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// クリエイトデータ
    /// </summary>
    private List<GimmickData> stageDatas = new List<GimmickData>();

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// 初期処理
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// データ受取処理
    /// </summary>
    /// <param name="datas">ステージデータ</param>
    public void SetData(List<GimmickData> datas)
    {
        stageDatas = datas;
        Debug.Log("受取完了");
    }

    /// <summary>
    /// データ渡し処理
    /// </summary>
    /// <returns>クリエイトデータ</returns>
    public List<GimmickData> GetData() {  return stageDatas; }
}
