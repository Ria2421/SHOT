//---------------------------------------------------------------
//
// ゲームマネージャー [ GameManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/09/18
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //-------------------------------------------
    // フィールド

    // ステージNo
    [SerializeField] int stageNo;

    //--------------------------------------------
    // メソッド

    /// <summary>
    /// ステージNo返却
    /// </summary>
    /// <returns></returns>
    public int GetStageNo() { return stageNo; }
}
