//---------------------------------------------------------------
//
// �z�[���}�l�[�W���[ [ HomeManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/01
// Update:2024/09/13
//
//---------------------------------------------------------------
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �l�b�g���[�N�}�l�[�W���[
    /// </summary>
    private NetworkManager networkManager;

    /// <summary>
    /// �A�C�R��ID
    /// </summary>
    private int iconID = 0;

    [Header(" �z�[����ʊ֘A ")]

    /// <summary>
    /// �t�H���[�p�l��
    /// </summary>
    [SerializeField] private GameObject followPanel;

    [Header(" �A�J�E���g��ʊ֘A ")]

    /// <summary>
    /// ���[�U�[�����͗�
    /// </summary>
    [SerializeField] private InputField nameInput;

    /// <summary>
    /// �v���t�B�[�����e�L�X�g
    /// [0:���[�U�[ID 1:���[�U�[�� 2:���v���C�� 3:�N���A�� 4:�쐬�X�e�[�W�� 5:�t�H���[�� 6:�t�H�����[��]
    /// </summary>
    [SerializeField] private List<Text> contentTexts;

    /// <summary>
    /// �����ʒm�ʒu���
    /// </summary>
    [SerializeField] private RectTransform changeComplete;

    /// <summary>
    /// ���s�ʒm�ʒu���
    /// </summary>
    [SerializeField] private RectTransform changeFailed;

    /// <summary>
    /// ���O���͗�
    /// </summary>
    [SerializeField] private GameObject namePanel;

    /// <summary>
    /// �A�C�R���ꗗ�p�l��
    /// </summary>
    [SerializeField] private GameObject iconPanel;

    /// <summary>
    /// �A�C�R���W
    /// </summary>
    [SerializeField] private List<Sprite> iconSprite;

    /// <summary>
    /// �A�C�R���摜
    /// </summary>
    [SerializeField] private Image iconImage;

    [Header(" �t�H���[��ʊ֘A ")]

    /// <summary>
    /// ���[�U�[���v���n�u
    /// </summary>
    [SerializeField] private GameObject userInfoPrefab;

    /// <summary>
    /// �m�[�f�[�^�v���n�u
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
    /// �X�N���[�����X�g    [0:�t�H���[ 1:�t�H�����[ 2:����]
    /// </summary>
    [SerializeField] private List<GameObject> scrollList;

    /// <summary>
    /// �X�N���[���R���e���c
    /// </summary>
    [SerializeField] private List<GameObject> scrolltContents;

    /// <summary>
    /// �o�^�摜�X�v���C�g
    /// </summary>
    [SerializeField] private Sprite registSprite;

    /// <summary>
    /// �t�H���[ID���͗�
    /// </summary>
    [SerializeField] private InputField followIDInput;

    /// <summary>
    /// �t�H���[�o�^�{�^��
    /// </summary>
    [SerializeField] private Button followRegistButton;

    /// <summary>
    /// �G���[�p�l�� [0:���� 1:�t�H���[�� 2:404�G���[]
    /// </summary>
    [SerializeField] private List<RectTransform> noticePanels;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �l�b�g���[�N�}�l�[�W���[�擾
        networkManager = NetworkManager.Instance;

        // ���[�UID�̎擾�E���f
        contentTexts[0].text = "ID : " + networkManager.GetUserID().ToString();

        // ���[�U���̎擾�E���f
        contentTexts[1].text = networkManager.GetUserName();

        // ���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
        StartCoroutine(NetworkManager.Instance.GetProfileInfo(
            result =>
            {   // ��񔽉f
                iconID = result.IconID;                                      // �A�C�R��ID�擾 
                iconImage.sprite = iconSprite[iconID - 1];                   // �A�C�R�����f
                contentTexts[2].text = result.PlayCnt.ToString() + "��";     // ���v���C��
                contentTexts[3].text = result.ClearCnt.ToString() + "��";    // �N���A��
                contentTexts[4].text = result.CreateCnt.ToString() + "��";   // �X�e�[�W�쐬��
                contentTexts[5].text = result.FollowCnt.ToString() + "��";   // �t�H���[��
                contentTexts[6].text = result.FollowerCnt.ToString() + "��"; // �t�H�����[��
            }));
    }

    //======================
    // �V�[���J�ڃ��\�b�h

    public void TransScene(string name)
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade(name, Color.white, 2.5f);
    }

    //=======================================================
    // ���j���[�֘A���\�b�h

    /// <summary>
    /// �e�A�C�R����������
    /// </summary>
    public void PushMenuIconButton(GameObject iconPanel)
    {
        iconPanel.SetActive(true);
    }

    /// <summary>
    /// �p�l�����鏈��
    /// </summary>
    public void PushCloseButton(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// �t�H���[�{�^��������
    /// </summary>
    public void PushFollowButton()
    {
        followPanel.SetActive(true);

        // �������߃��[�U�[���X�g�̐���
        StartCoroutine(NetworkManager.Instance.GetRandom(
            result =>
            {
                if (result.Count == 0)
                {
                    // �f�[�^�����\��
                    GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);
                }
                foreach (var data in result)
                {   // �t�H���[���X�g

                    // ���[�U�[�f�[�^����
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // �A�C�R���ݒ�
                    userData.transform.GetChild(1).GetComponent<Text>().text = "ID : " + data.ID.ToString();    // ID�ݒ�
                    userData.transform.GetChild(2).GetComponent<Text>().text = data.Name;                       // ���O�ݒ�
                    userData.transform.GetChild(3).GetComponent<Image>().sprite = registSprite;                 // �摜�ݒ�
                    userData.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
                    {   // �t�H���[�o�^����
                        // �{�^���̖�����
                        userData.transform.GetChild(3).GetComponent<Button>().interactable = false;

                        StartCoroutine(NetworkManager.Instance.RegistFollow(
                            data.ID,
                            result =>
                            {
                                Debug.Log("�o�^����");
                            }));
                    });
                }
            }));

        // �t�H���[�E�t�H�����[���X�g�̐���
        StartCoroutine(NetworkManager.Instance.GetFollow(
            result =>
            {
                if (result.Follow.Count == 0)
                {
                    // �f�[�^�����\��
                    GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);
                }
                foreach (var data in result.Follow)
                {   // �t�H���[���X�g

                    // ���[�U�[�f�[�^����
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // �A�C�R���ݒ�
                    userData.transform.GetChild(1).GetComponent<Text>().text = "ID : " + data.ID.ToString();    // ID�ݒ�
                    userData.transform.GetChild(2).GetComponent<Text>().text = data.Name;                       // ���O�ݒ�
                    userData.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
                    {   // �t�H���[��������
                        StartCoroutine(NetworkManager.Instance.DestroyFollow(
                            data.ID,
                            result =>
                            {   // ���[�UInfo�I�u�W�F�̍폜
                                Destroy(userData);
                            }));
                    });
                }

                if (result.Follower.Count == 0)
                {
                    // �f�[�^�����\��
                    GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);
                }
                foreach (var data in result.Follower)
                {   // �t�H�����[���X�g
                    
                    // ���[�U�[�f�[�^����
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // �A�C�R���ݒ�
                    userData.transform.GetChild(1).GetComponent<Text>().text = "ID : " + data.ID.ToString();    // ID�ݒ�
                    userData.transform.GetChild(2).GetComponent<Text>().text = data.Name;                       // ���O�ݒ�
                    Destroy(userData.transform.GetChild(3).gameObject);    // �{�^���̔j�� 
                }

                Debug.Log("���X�g��������");

                foreach (GameObject button in listButtons)
                {   // �{�^���̗L����
                    button.GetComponent<Button>().interactable = true;
                }

                SetList(0); // �t�H���[���X�g�̗L����
            }));
    }

    /// <summary>
    /// �t�H���[�p�l�����鏈��
    /// </summary>
    public void PushFollowClose()
    {
        followPanel.SetActive(false);

        for(int i=0; i<scrolltContents.Count; i++)
        {
            foreach (Transform content in scrolltContents[i].transform)
            {
                //�����̎q����Destroy����
                Destroy(content.gameObject);
            }
        }
    }

    /// <summary>
    /// �ʒm��������
    /// </summary>
    /// <param name="gameObject"></param>
    public void PushNoticeButton(RectTransform rectTransform)
    {
        // �����ʒu�Ɉړ�
        rectTransform.anchoredPosition = new Vector2(1000.0f, rectTransform.anchoredPosition.y);
    }

    /// <summary>
    /// �ʒm�\������
    /// </summary>
    /// <param name="cautionImage">�ړ��Ώ�</param>
    private void MoveCaution(RectTransform cautionImage)
    {
        cautionImage.DOAnchorPos(new Vector2(0f, cautionImage.anchoredPosition.y), 0.6f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// ���O�ύX�{�^��������
    /// </summary>
    public void PushNameChange()
    {
        StartCoroutine(NetworkManager.Instance.ChangeName(
            nameInput.text,
            result =>
            {
                if (result)
                {
                    // ���O�̕ύX����
                    contentTexts[1].text = nameInput.text;
                    MoveCaution(changeComplete);    // �����ʒm
                    namePanel.SetActive(false);     // ���͗���\��
                }
                else
                {
                    MoveCaution(changeFailed);      // ���s�ʒm
                    namePanel.SetActive(false);     // ���͗���\��
                }
            }));
    }

    /// <summary>
    /// �A�C�R���ύX��������
    /// </summary>
    /// <param name="id">�A�C�R��ID</param>
    public void PushIconChange(int id)
    {
        StartCoroutine(NetworkManager.Instance.ChangeIcon(
            id,
            result =>
            {
                if (result)
                {
                    // �A�C�R���̕ύX����
                    iconImage.sprite = iconSprite[id - 1];
                    MoveCaution(changeComplete);    // �����ʒm
                    iconPanel.SetActive(false);     // ���͗���\��
                }
                else
                {
                    MoveCaution(changeFailed);      // ���s�ʒm
                    iconPanel.SetActive(false);     // ���͗���\��
                }
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
    /// �t�H���[�o�^����
    /// </summary>
    public void PushRegistFollow()
    {
        if(followIDInput.text == "") { return; }  // ���͖�����

        followRegistButton.interactable = false;    // �{�^��������

        Invoke("RevivalFollowButton", 2.5f);        // �w�莞�Ԍo�ߌ�{�^���L����

        StartCoroutine(NetworkManager.Instance.RegistFollow(
            int.Parse(followIDInput.text),
            result =>
            {
                switch (result)
                {
                    case "200": // �o�^����
                        Debug.Log("�o�^����");
                        MoveCaution(noticePanels[0]);
                        break;

                    case "400": // �o�^��
                        Debug.Log("�o�^��");
                        MoveCaution(noticePanels[1]);
                        break;

                    case "404": // �w��ID�����݂��Ȃ�
                        Debug.Log("ID�����݂��Ȃ�");
                        MoveCaution(noticePanels[2]);
                        break;

                    default:
                        break;
                }
            }));
    }

    /// <summary>
    /// �{�^���L��������
    /// </summary>
    /// <param name="button"></param>
    private void RevivalFollowButton()
    {
        followRegistButton.interactable = true;
    }
}
