//---------------------------------------------------------------
//
// ステージデータ保管スクリプト [ StageDataObject.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/04
// Update:2024/09/04
//
//---------------------------------------------------------------
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataObject : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    /// <summary>
    /// ステージID
    /// </summary>
    private int stageID = 0;

    /// <summary>
    /// 作成者ID
    /// </summary>
    private int creatorID = 0;

    /// <summary>
    /// クリエイトデータ
    /// </summary>
    private List<GimmickData> stageDatas = new List<GimmickData>();

    /// <summary>
    /// イイネ数
    /// </summary>
    private int goodVol = 0;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// データ受取処理
    /// </summary>
    /// <param name="datas">ステージデータ</param>
    public void SetData(int id,int cid,List<GimmickData> datas,int good)
    {
        stageID = id;
        creatorID = cid;
        stageDatas = datas;
        goodVol = good;
        Debug.Log("受取完了");
    }

    /// <summary>
    /// ステージID返し処理
    /// </summary>
    /// <returns></returns>
    public int GetID() { return stageID; }

    /// <summary>
    /// ステージID返し処理
    /// </summary>
    /// <returns></returns>
    public int GetCreatorID() { return creatorID; }

    /// <summary>
    /// ステージデータ返し処理
    /// </summary>
    /// <returns>クリエイトデータ</returns>
    public List<GimmickData> GetStageData() {  return stageDatas; }

    /// <summary>
    /// イイネ数返し処理
    /// </summary>
    /// <returns></returns>
    public int GetGood() { return goodVol; }

    /// <summary>
    /// ステージデータリセット
    /// </summary>
    public void ResetData()
    {
        // データクリア
        stageDatas.Clear();
    }
}
