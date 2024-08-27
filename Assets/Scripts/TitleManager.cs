//---------------------------------------------------------------
//
// �^�C�g���}�l�[�W���[ [ TitleManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/07/18
//
//---------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �{�^���R���|�[�l���g�ۑ��p
    /// </summary>
    [SerializeField] private Button btn;

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
    ///  �X�e�[�W�I����ʈڍs����
    /// </summary>
    public void TransSelectScene()
    {
        // ��x�����ꂽ�ꍇ�̓{�^���𖳌��ɂ���
        btn.interactable = false;

        // ���[�U�[�f�[�^�̓Ǎ������E���ʂ��擾
        bool isSuccess = NetworkManager.Instance.LoadUserData();

        if (!isSuccess)
        {
            // ���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
            StartCoroutine(NetworkManager.Instance.StoreUser(
                Guid.NewGuid().ToString(),  // ���[�U�[��
                result =>
                {
                    /* ��ʑJ�ڏ��� */
                    Initiate.DoneFading();
                    Initiate.Fade("HomeScene", Color.black, 1.5f);
                }));
        }
        else {
            /* ��ʑJ�ڏ��� */
            Initiate.DoneFading();
            Initiate.Fade("HomeScene", Color.black, 1.5f);
        }   
    }
}
