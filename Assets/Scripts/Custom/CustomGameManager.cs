//---------------------------------------------------------------
//
// �J�X�^���Q�[���}�l�[�W���[ [ CustomGameManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/05
// Update:2024/09/11
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomGameManager : MonoBehaviour
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
    /// �C�C�l�{�^��
    /// </summary>
    [SerializeField] private GameObject goodButton;

    /// <summary>
    /// ���L�{�^��
    /// </summary>
    [SerializeField] private GameObject shareButton;

    /// <summary>
    /// �l�b�g���[�N�}�l�[�W���[
    /// </summary>
    private NetworkManager networkManager;

    /// <summary>
    /// �X�e�[�W�f�[�^�I�u�W�F�N�g
    /// </summary>
    private StageDataObject stageDataObject;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        networkManager = NetworkManager.Instance;
        // �X�e�[�W�f�[�^�̎󂯎��E�z�u
        stageDataObject = GameObject.Find("StageDataObject").GetComponent<StageDataObject>();
        var stageDatas = stageDataObject.GetStageData();
        foreach (GimmickData data in stageDatas)
        {
            // Resources�t�H���_����M�~�b�N�̃I�u�W�F�N�g���擾�E����
            GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
            Instantiate(obj, new Vector3(data.X, data.Y, 0), Quaternion.identity);
        }
    }


    //================================
    // ���j���[����

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
    /// �߂鉟������
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CustumStageSelect", Color.white, 2.5f);
    }

    /// <summary>
    /// ���v���C��������
    /// </summary>
    public void PushReplayButton()
    {
        // �V�[���̍ēǂ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //========================
    // ���U���g����

    /// <summary>
    /// �C�C�l�{�^������
    /// </summary>
    public void PushGoodButton()
    {
        goodButton.GetComponent<Button>().interactable = false;    // �{�^��������

        // �C�C�l�X�V����
        StartCoroutine(NetworkManager.Instance.UpdateGood(
            stageDataObject.GetID(),
            stageDataObject.GetGood() + 1,
            result =>
            {
                if (result)
                {
                    goodButton.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    goodButton.GetComponent<Image>().color = Color.red;
                }
            }));
    }

    /// <summary>
    /// ���L�{�^������
    /// </summary>
    public void PushShareButton()
    {
        shareButton.GetComponent<Button>().interactable = false;    // �{�^��������

        // �C�C�l�X�V����
        StartCoroutine(NetworkManager.Instance.ShereStage(
            stageDataObject.GetID(),
            result =>
            {
                if (result)
                {
                    shareButton.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    shareButton.GetComponent<Image>().color = Color.red;
                }
            }));
    }

    // �z�[���{�^����������
    public void PushHomeButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.white, 2.5f);
    }
}
