//---------------------------------------------------------------
//
// �^�C�g���}�l�[�W���[ [ TitleManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/09/13
//
//---------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


public class TitleManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // ���[�U�[�f�[�^�̓Ǎ������E���ʂ��擾
        bool isSuccess = NetworkManager.Instance.LoadUserData();

        if (!isSuccess)
        {
            // ���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
            StartCoroutine(NetworkManager.Instance.StoreUser(
                Guid.NewGuid().ToString(),  // ���[�U�[��
                result =>
                {
                    // �J�^���O�X�V�`�F�b�N
                    StartCoroutine(checkCatalog());
                }));
        }
        else
        {
            // �J�^���O�X�V�`�F�b�N
            StartCoroutine(checkCatalog());
        }
    }
    /// <summary>
    /// �X�e�[�W�J�^���O�`�F�b�N
    /// </summary>
    /// <returns></returns>
    private IEnumerator checkCatalog()
    {
        // �J�^���O�f�[�^���X�V����Ă��邩���`�F�b�N
        var checkHandle = Addressables.CheckForCatalogUpdates(false);
        yield return checkHandle;
        var updates = checkHandle.Result;
        Addressables.Release(checkHandle);  // �������̊J��

        if(updates.Count >= 1)
        {   // �X�V��1�ȏ゠�����ꍇ�̓��[�h��ʂ�
            Initiate.DoneFading();
            Initiate.Fade("LoadingScene", Color.white, 2.5f);
        }
        else
        {   // �Ȃ��ꍇ�̓z�[����ʂ�
            Initiate.DoneFading();
            Initiate.Fade("HomeScene", Color.white, 2.5f);
        }
    }
}
