//---------------------------------------------------------------
//
// �X�e�[�W�Z���N�g�}�l�[�W���[ [ SelectManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/07/18
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
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
    /// �X�e�[�W�I������
    /// </summary>
    public void PushStageSelect(string buttonNum)
    {
        // ���O�ɂăX�e�[�WNo��\��
        Debug.Log(buttonNum);

        if (buttonNum != "")
        {
            /* �t�F�[�h���� (��)  
                         ( "�V�[����",�t�F�[�h�̐F, ����);  */
            Initiate.DoneFading();
            Initiate.Fade("Stage" + buttonNum + "Scene", Color.gray, 2.5f);
        }
    }

    /// <summary>
    /// �߂�{�^����������
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }
}
