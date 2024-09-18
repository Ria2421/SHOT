//---------------------------------------------------------------
//
// UI�}�l�[�W���[ [ UIManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/29
// Update:2024/09/04
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class UIManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �V�[����
    /// </summary>
    [SerializeField] private string sceneName = "";

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // NetworkManager�擾
        NetworkManager networkManager = NetworkManager.Instance;

#if UNITY_EDITOR
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Addressables.LoadScene("Stage" + networkManager.PlayStageNo.ToString(), LoadSceneMode.Additive);
#else
        // UI�V�[���̒ǉ�
        Addressables.LoadScene("Stage" + networkManager.PlayStageNo.ToString(), LoadSceneMode.Additive);
#endif
    }

    /// <summary>
    /// ���v���C����
    /// </summary>
    public void gameReplay()
    {
        // �V�[���̍ēǂ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void transitionHome()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }

    /// <summary>
    /// �X�e�[�W�I��J�ڏ���
    /// </summary>
    public void transitionSelect()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("StageSelectScene", Color.gray, 2.5f);
    }
}
