//---------------------------------------------------------------
//
// �N���G�C�g�v���C�}�l�[�W���[ [ CreatePlayManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/10
// Update:2024/09/10
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreatePlayManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �M�~�b�N�i�[�p�̐e�I�u�W�F�N�g
    /// </summary>
    [SerializeField] private GameObject parentObj;

    /// <summary>
    /// ���j���[�p�l��
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �X�e�[�W�f�[�^�̎󂯎��E�z�u
        var stageDatas = GameObject.Find("StageDataObject").GetComponent<StageDataObject>().GetStageData();
        foreach (GimmickData data in stageDatas)
        {
            // Resources�t�H���_����M�~�b�N�̃I�u�W�F�N�g���擾�E����
            GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
            Instantiate(obj, new Vector3(data.X, data.Y, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// �Z���N�g�{�^����������
    /// </summary>
    public void PushSelectButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("LookCreateStageScene", Color.white, 2.5f);
    }

    /// <summary>
    /// ���v���C����
    /// </summary>
    public void PushGameReplay()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // �V�[���̍ēǂ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void PushComplete()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("ConfCreateScene", Color.white, 2.5f);
    }

    /// <summary>
    /// ���j���[������
    /// </summary>
    public void PushMenuButton()
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // ���j���[�p�l����\��
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// ����{�^��������
    /// </summary>
    public void PushCloseButton()
    {
        SEManager.Instance.Play(SEPath.CANCEL);

        // ���j���[�p�l�����\��
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// �o�b�N�{�^��������
    /// </summary>
    public void PushBackButton()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.HOME_SELECT);
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateMainScene", Color.white, 1.5f);
    }
}
