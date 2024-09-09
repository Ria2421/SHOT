//---------------------------------------------------------------
//
// �쐬�X�e�[�W�ꗗ [ LookCreateStageManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/08
// Update:2024/08/08
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class LookCreateStageManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���v���n�u
    /// </summary>
    [SerializeField] private GameObject infoPrefab;

    /// <summary>
    /// �e�I�u�W�F
    /// </summary>
    [SerializeField] private GameObject parentObj;

    /// <summary>
    /// �l�b�g���[�N�}�l�[�W���[�i�[�p
    /// </summary>
    private NetworkManager networkManager;

    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �l�b�g���[�N�}�l�[�W���[�̎擾
        networkManager = NetworkManager.Instance;

        // ���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
        StartCoroutine(NetworkManager.Instance.GetPlayerCreateStage(
            result =>
            {
                if (result != null)
                {
                    // �f�[�^�擾����
                    Debug.Log("�X�e�[�W�ꗗ�擾");
                    foreach (var item in result)
                    {
                        // �X�e�[�W�ꗗ�̐���
                        GameObject info = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, parentObj.transform);
                        // �����
                        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ID:" + item.ID.ToString();   // ID
                        info.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.Name;                    // �X�e�[�W��
                        info.transform.GetChild(2).gameObject.GetComponent<Text>().text = networkManager.GetUserName(); // ���[�U�[��
                        info.transform.GetChild(3).gameObject.GetComponent<Text>().text = item.GoodVol.ToString();      // �C�C�l��
                        // �N���b�N���X�e�[�W�J��
                        info.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            // �f�[�^�ۊǗp�I�u�W�F�N�g�̐���
                            GameObject stageDataObject = new GameObject("StageDataObject");
                            stageDataObject.AddComponent<StageDataObject>();
                            DontDestroyOnLoad(stageDataObject);    // Scene�J�ڂŔj������Ȃ悤�ɂ���

                            StartCoroutine(NetworkManager.Instance.GetIDCreate(
                                item.ID,
                                result =>
                                {
                                    Debug.Log("�X�e�[�W�f�[�^�擾");
                                }));

                        });
                    }
                }
                else
                {
                    Debug.Log("�擾���s");
                }
            }));
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// �߂�{�^����������
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.gray, 1.5f);
    }
}
