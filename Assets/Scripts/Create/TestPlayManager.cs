//---------------------------------------------------------------
//
// �e�X�g�v���C�}�l�[�W���[ [ TestPlayManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/04
// Update:2024/09/04
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayManager : MonoBehaviour
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

    //--------------------------------------------
    // ���\�b�h

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
    /// ���v���C����
    /// </summary>
    public void PushGameReplay()
    {
        // �V�[���̍ēǂ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void PushComplete()
    {
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
    /// �o�b�N�{�^��������
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateMainScene", Color.white, 1.5f);
    }
}
