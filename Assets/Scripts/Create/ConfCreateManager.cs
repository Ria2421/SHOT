//---------------------------------------------------------------
//
// �����m�F��ʃ}�l�[�W���[ [ ConfCreateManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/06
// Update:2024/09/09
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ConfCreateManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �����{�^���R���|�[�l���g�ۑ��p
    /// </summary>
    [SerializeField] private Button compBtn;

    /// <summary>
    /// �߂�{�^��
    /// </summary>
    [SerializeField] private Button backBtn;

    /// <summary>
    /// �X�e�[�W�����̓t�B�[���h
    /// </summary>
    [SerializeField] private InputField inputField;

    /// <summary>
    /// �X�e�[�W�f�[�^�I�u�W�F
    /// </summary>
    private StageDataObject stageDataObject;

    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �X�e�[�W�f�[�^�I�u�W�F�N�g�̓���
        stageDataObject = GameObject.Find("StageDataObject").GetComponent<StageDataObject>();
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void PushCompButton()
    {
        // UI�𖳌���
        compBtn.interactable = false;
        backBtn.interactable = false;
        inputField.interactable = false;

        // �X�e�[�W�f�[�^��json�V���A���C�Y
        var data = stageDataObject.GetData();
        string json = JsonConvert.SerializeObject(data);

        // ���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
        StartCoroutine(NetworkManager.Instance.StoreCreateStage(
            inputField.text,    // ���[�U�[��
            json,               // �X�e�[�W�f�[�^
            result =>
            {
                if (result)
                {
                    // �o�^�����\���B�\�����Ƀz�[���֖߂�{�^����z�u
                    Debug.Log("�o�^����");
                    // �X�e�[�W�f�[�^�I�u�W�F��j��
                    Destroy(GameObject.Find("StageDataObject"));
                }
                else
                {
                    Debug.Log("�o�^���s");
                }
            }));
    }

    /// <summary>
    /// ���V���[�h�J�ڏ���
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("TestPlayScene", Color.white, 2.5f);
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void PushHomeButton()
    {
        // �X�e�[�W�f�[�^�I�u�W�F��j��
        Destroy(GameObject.Find("StageDataObject"));

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.white, 2.5f);
    }
}
