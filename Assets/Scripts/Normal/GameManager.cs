//---------------------------------------------------------------
//
// �Q�[���}�l�[�W���[ [ GameManager.cs ]
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
    // �t�B�[���h

    // �X�e�[�WNo
    [SerializeField] int stageNo;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// �X�e�[�WNo�ԋp
    /// </summary>
    /// <returns></returns>
    public int GetStageNo() { return stageNo; }
}
