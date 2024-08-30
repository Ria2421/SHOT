//---------------------------------------------------------------
//
// �A�Z�b�g�ǂݍ��� [ AssetLoader.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/29
// Update:2024/08/29
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���[�f�B���O�X���C�_�[
    /// </summary>
    [SerializeField] private Slider loadingSlider;

    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        StartCoroutine(loading());
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// �X�V�f�[�^�ǂݍ��ݏ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator loading()
    {
        // �J�^���O�X�V����
        var handle = Addressables.UpdateCatalogs(); // �ŐV�̃J�^���O(json)���擾
        yield return handle;

        // �_�E�����[�h�̎��s                                                           ���O���[�v�Őݒ肵�����x��
        AsyncOperationHandle downloadingHandle = Addressables.DownloadDependenciesAsync("default", false);  

        // �_�E�����[�h��������܂ŃX���C�_�[��UI���X�V
        while(downloadingHandle.Status == AsyncOperationStatus.None)
        {
            loadingSlider.value = downloadingHandle.GetDownloadStatus().Percent * 100;  // Percent��0�`1�Ŏ擾
            yield return null;  // 1�t���[���҂�
        }

        loadingSlider.value = 100;  // ������A�o�[���ő�l�ɐݒ�
        Addressables.Release(downloadingHandle);

        // ���̃V�[���ֈړ�
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.black, 1.5f);
    }
}
