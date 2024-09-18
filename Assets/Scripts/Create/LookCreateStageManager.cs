//---------------------------------------------------------------
//
// �쐬�X�e�[�W�ꗗ [ LookCreateStageManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/08
// Update:2024/08/08
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
    /// �A�C�R���X�v���C�g
    /// </summary>
    [SerializeField] private List<Sprite> iconSprits;

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

        // �X�e�[�W�I�u�W�F�N�g������
        GameObject stageDataObject = GameObject.Find("StageDataObject");

        if(stageDataObject != null)
        {
            // �X�e�[�W�f�[�^�̃��Z�b�g
            stageDataObject.GetComponent<StageDataObject>().ResetData();
        }
        else
        {
            // �f�[�^�ۊǗp�I�u�W�F�N�g�̐���
            stageDataObject = new GameObject("StageDataObject");
            stageDataObject.AddComponent<StageDataObject>();
            DontDestroyOnLoad(stageDataObject);    // Scene�J�ڂŔj������Ȃ悤�ɂ���
        }

        // ����X�e�[�W�ꗗ�̎擾
        StartCoroutine(NetworkManager.Instance.GetPlayerCreateStage(
            result =>
            {
                if (result != null)
                {
                    // �f�[�^�擾����
                    Debug.Log("�X�e�[�W�ꗗ�擾");
                    foreach (var data in result)
                    {
                        // �X�e�[�W�ꗗ�̐���
                        GameObject info = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity, parentObj.transform);
                        // �X�e�[�W�����
                        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ID:" + data.ID.ToString();    // ID
                        info.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.Name;                     // �X�e�[�W��
                        info.transform.GetChild(2).gameObject.GetComponent<Text>().text = networkManager.GetUserName();  // ���[�U�[��
                        info.transform.GetChild(3).gameObject.GetComponent<Text>().text = data.GoodVol.ToString();       // �C�C�l��
                        info.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = iconSprits[data.IconID - 1];// �A�C�R���ݒ�
                        // �N���b�N���X�e�[�W�J��
                        info.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            StartCoroutine(NetworkManager.Instance.GetIDCreate(
                                data.ID,
                                result =>
                                {
                                    // JSON�f�V���A���C�Y
                                    var resultData = JsonConvert.DeserializeObject<List<GimmickData>>(result.GimmickPos);
                                    stageDataObject.GetComponent<StageDataObject>().SetData(data.ID,data.UserID,resultData,data.GoodVol);
                                    Debug.Log("�X�e�[�W�f�[�^�擾");

                                    /* �t�F�[�h���� (��)  
                                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
                                    Initiate.DoneFading();
                                    Initiate.Fade("CreatePlayScene", Color.white, 2.5f);
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
        var stageDataObject = GameObject.Find("StageDataObject");
        // �X�e�[�W�f�[�^�I�u�W�F�̍폜
        if(stageDataObject != null)
        {
            Destroy(stageDataObject);
        }

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.gray, 1.5f);
    }
}
