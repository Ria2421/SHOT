//---------------------------------------------------------------
//
// �X�e�[�W�f�[�^�ۊǃX�N���v�g [ StageDataObject.cs ]
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
    // �t�B�[���h

    /// <summary>
    /// �N���G�C�g�f�[�^
    /// </summary>
    private List<GimmickData> stageDatas = new List<GimmickData>();

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// �f�[�^��揈��
    /// </summary>
    /// <param name="datas">�X�e�[�W�f�[�^</param>
    public void SetData(List<GimmickData> datas)
    {
        stageDatas = datas;
        Debug.Log("��抮��");
    }

    /// <summary>
    /// �f�[�^�n������
    /// </summary>
    /// <returns>�N���G�C�g�f�[�^</returns>
    public List<GimmickData> GetData() {  return stageDatas; }
}
