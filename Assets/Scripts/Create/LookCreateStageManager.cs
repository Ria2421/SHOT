//---------------------------------------------------------------
//
// �쐬�X�e�[�W�ꗗ [ LookCreateStageManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/08
// Update:2024/08/08
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCreateStageManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h


    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// �߂�{�^����������
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.gray, 1.5f);
    }
}
