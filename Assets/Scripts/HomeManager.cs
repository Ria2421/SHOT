//---------------------------------------------------------------
//
// �z�[���}�l�[�W���[ [ HomeManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/01
// Update:2024/08/01
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���j���[�\���{�^��
    /// </summary>
    [SerializeField] private GameObject upManuButton;

    /// <summary>
    /// ���j���[��\���{�^��
    /// </summary>
    [SerializeField] private GameObject downManuButton;

    /// <summary>
    /// ���j���[���
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// �A�`�[�u�����g���
    /// </summary>
    [SerializeField] private GameObject achievementPanel;

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

    //=======================================================
    // �V�[���J��

    //=======================================================
    // ���j���[�֘A���\�b�h

    /// <summary>
    /// ���j���[�\������
    /// </summary>
    public void PushMenuUpButton()
    {
        // ���j���[�\���{�^�����A�N�e�B�u��
        upManuButton.SetActive(false);

        // ���j���[��\���{�^�����A�N�e�B�u��
        downManuButton.SetActive(true);

        // ���j���[��\��
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// ���j���[��\������
    /// </summary>
    public void PushMenuDownButton()
    {
        // ���j���[�\���{�^�����A�N�e�B�u��
        upManuButton.SetActive(true);

        // ���j���[��\���{�^�����A�N�e�B�u��
        downManuButton.SetActive(false);

        // ���j���[�����
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// ���j���[���鏈��
    /// </summary>
    public void PushCloseButton(GameObject menuPanel)
    {
        // �e�I�u�W�F�N�g���A�N�e�B�u��
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// �e�A�C�R����������
    /// </summary>
    public void PushMenuIconButton(GameObject iconPanel)
    {
        // �A�`�[�u�����g��ʂ̕\��
        iconPanel.SetActive(true);
    }
}
