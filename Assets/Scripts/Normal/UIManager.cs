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
using KanKikuchi.AudioManager;

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
    /// �N������
    /// </summary>
    void Awake()
    {
        // NetworkManager�擾
        NetworkManager networkManager = NetworkManager.Instance;

#if UNITY_EDITOR
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
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // �V�[���̍ēǂ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void transitionHome()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

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
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("StageSelectScene", Color.gray, 2.5f);
    }
}
