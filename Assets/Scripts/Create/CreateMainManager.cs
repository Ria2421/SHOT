//---------------------------------------------------------------
//
// �N���G�C�g�}�l�[�W���[ [ CreateMainManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMainManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���j���[�p�l��
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// ���j���[������
    /// </summary>
    public void PushMenuButton()
    {
        // ���j���[�p�l����\��
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// ����{�^��������
    /// </summary>
    public void PushCloseButton()
    {
        // ���j���[�p�l�����\��
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// �z�[���{�^��������
    /// </summary>
    public void PushHomeButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.black, 1.5f);
        
    }
}
