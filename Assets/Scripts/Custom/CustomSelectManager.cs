//---------------------------------------------------------------
//
// �J�X�^���Z���N�g�}�l�[�W���[ [ CustomSelectManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/26
// Update:2024/09/11
//
//---------------------------------------------------------------
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSelectManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �X�e�[�W���i�[�v���n�u
    /// </summary>
    [SerializeField] private GameObject stageInfoPrefab;

    /// <summary>
    /// �f�[�^�Ȃ��\���v���n�u
    /// </summary>
    [SerializeField] private GameObject noDataPrefab;

    /// <summary>
    /// ���X�g�؂�ւ��{�^��
    /// </summary>
    [SerializeField] private List<GameObject> listButtons;

    /// <summary>
    /// �؂�ւ��{�^��Image���X�g
    /// </summary>
    [SerializeField] private List<Image> buttonColors;

    /// <summary>
    /// �X�N���[�����X�g    [0:�t�H���[ 1:�C�C�l 2:���L]
    /// </summary>
    [SerializeField] private List<GameObject> scrollList;

    /// <summary>
    /// �X�N���[���R���e���c
    /// </summary>
    [SerializeField] private List<GameObject> scrolltContents;

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

        if (stageDataObject != null)
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

        // �J�X�^�����X�g�̎擾
        StartCoroutine(NetworkManager.Instance.GetCustomList(
            result =>
            {
                for(int i = 0; i < 3; i++)
                {
                    if (result[i].Count == 0)
                    {   // �f�[�^�����\��
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[i].transform);
                        continue;
                    }

                    foreach (var data in result[i])
                    {
                        // �X�e�[�W�ꗗ�̐���
                        GameObject info = Instantiate(stageInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[i].transform);
                        // �X�e�[�W�����
                        info.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ID:" + data.ID.ToString();   // ID
                        info.transform.GetChild(1).gameObject.GetComponent<Text>().text = data.Name;                    // �X�e�[�W��
                        info.transform.GetChild(2).gameObject.GetComponent<Text>().text = data.UserName;                // ���[�U�[��
                        info.transform.GetChild(3).gameObject.GetComponent<Text>().text = data.GoodVol.ToString();      // �C�C�l��
                        // �N���b�N���X�e�[�W�J��
                        info.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            StartCoroutine(NetworkManager.Instance.GetIDCreate(
                                data.ID,
                                result =>
                                {
                                    // JSON�f�V���A���C�Y
                                    var resultData = JsonConvert.DeserializeObject<List<GimmickData>>(result.GimmickPos);
                                    if (resultData == null) { return; }  // null�`�F�b�N
                                    stageDataObject.GetComponent<StageDataObject>().SetData(data.ID, resultData, data.GoodVol);
                                    Debug.Log("�X�e�[�W�f�[�^�擾");

                                    /* �t�F�[�h���� (��)  
                                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
                                    Initiate.DoneFading();
                                    Initiate.Fade("CustomGameScene", Color.white, 2.5f);
                                }));
                        });
                    }
                }

                Debug.Log("���X�g��������");

                foreach(GameObject button in listButtons)
                {   // �{�^���̗L����
                    button.GetComponent<Button>().interactable = true;
                }

                SetList(0); // �t�H���[���X�g�̗L����
            }));
    }

    /// <summary>
    /// ���X�g�E�{�^���̕\��������
    /// </summary>
    private void ResetList()
    {
        // �{�^���E�X�N���[���E�B���h�E������
        for (int i = 0; i < listButtons.Count; i++)
        {
            scrollList[i].SetActive(false);         // �S���X�g���\��
            buttonColors[i].color = Color.white;    // �J���[��
        }
    }

    /// <summary>
    /// �w��No�̃��X�g��\��
    /// </summary>
    /// <param name="num"></param>
    public void SetList(int no)
    {
        ResetList();   // ����������
        scrollList[no].SetActive(true); 
        buttonColors[no].color = Color.gray;   
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void PushBackButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.white, 2.5f);
    }
}
