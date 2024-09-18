//---------------------------------------------------------------
//
// �X�e�[�W�f�[�^�ۊǃX�N���v�g [ StageDataObject.cs ]
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
    // �t�B�[���h

    /// <summary>
    /// �X�e�[�WID
    /// </summary>
    private int stageID = 0;

    /// <summary>
    /// �쐬��ID
    /// </summary>
    private int creatorID = 0;

    /// <summary>
    /// �N���G�C�g�f�[�^
    /// </summary>
    private List<GimmickData> stageDatas = new List<GimmickData>();

    /// <summary>
    /// �C�C�l��
    /// </summary>
    private int goodVol = 0;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// �f�[�^��揈��
    /// </summary>
    /// <param name="datas">�X�e�[�W�f�[�^</param>
    public void SetData(int id,int cid,List<GimmickData> datas,int good)
    {
        stageID = id;
        creatorID = cid;
        stageDatas = datas;
        goodVol = good;
        Debug.Log("��抮��");
    }

    /// <summary>
    /// �X�e�[�WID�Ԃ�����
    /// </summary>
    /// <returns></returns>
    public int GetID() { return stageID; }

    /// <summary>
    /// �X�e�[�WID�Ԃ�����
    /// </summary>
    /// <returns></returns>
    public int GetCreatorID() { return creatorID; }

    /// <summary>
    /// �X�e�[�W�f�[�^�Ԃ�����
    /// </summary>
    /// <returns>�N���G�C�g�f�[�^</returns>
    public List<GimmickData> GetStageData() {  return stageDatas; }

    /// <summary>
    /// �C�C�l���Ԃ�����
    /// </summary>
    /// <returns></returns>
    public int GetGood() { return goodVol; }

    /// <summary>
    /// �X�e�[�W�f�[�^���Z�b�g
    /// </summary>
    public void ResetData()
    {
        // �f�[�^�N���A
        stageDatas.Clear();
    }
}
