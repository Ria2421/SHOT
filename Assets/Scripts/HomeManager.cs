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
    /// ���j���[�\���{�^��
    /// </summary>
    [SerializeField] private GameObject upManuButton;

    /// <summary>
    /// ���j���[��\���{�^��
    /// </summary>
    [SerializeField] private GameObject downManuButton;

    /// <summary>
    /// ���j���[���
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// �A�`�[�u�����g���
    /// </summary>
    [SerializeField] private GameObject achievementPanel;

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
    /// [0:���[�U�[�� 1:���v���C�� 2:�N���A�� 3:�쐬�X�e�[�W�� 4:�t�H���[�� 5:�t�H�����[��]
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

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �l�b�g���[�N�}�l�[�W���[�擾
        networkManager = NetworkManager.Instance;

        // ���[�U���̎擾�E���f
        contentTexts[0].text = networkManager.GetUserName();

        // ���[�U�[�f�[�^���ۑ�����Ă��Ȃ��ꍇ�͓o�^
        StartCoroutine(NetworkManager.Instance.GetProfileInfo(
            result =>
            {   // ��񔽉f
                iconID = result.IconID;                                      // �A�C�R��ID�擾 
                iconImage.sprite = iconSprite[iconID - 1];                   // �A�C�R�����f
                contentTexts[1].text = result.PlayCnt.ToString() + "��";     // ���v���C��
                contentTexts[2].text = result.ClearCnt.ToString() + "��";    // �N���A��
                contentTexts[3].text = result.CreateCnt.ToString() + "��";   // �X�e�[�W�쐬��
                contentTexts[4].text = result.FollowCnt.ToString() + "��";   // �t�H���[��
                contentTexts[5].text = result.FollowerCnt.ToString() + "��"; // �t�H�����[��
            }));
    }

    //======================
    // �V�[���J�ڃ��\�b�h

    /// <summary>
    /// �m�[�}�����[�h�J�ڏ���
    /// </summary>
    public void TransNormalMode()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("StageSelectScene", Color.white, 2.5f);
    }

    /// <summary>
    /// �J�X�^���v���C���[�h�J�ڏ���
    /// </summary>
    public void TransCustomPlayMode()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CustumStageSelect", Color.white, 2.5f);
    }

    /// <summary>
    /// �J�X�^���v���C���[�h�J�ڏ���
    /// </summary>
    public void TransCreateMode()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.white, 2.5f);
    }

    //=======================================================
    // ���j���[�֘A���\�b�h

    /// <summary>
    /// ���j���[�\������
    /// </summary>
    public void PushMenuUpButton()
    {
        // ���j���[�\���{�^�����A�N�e�B�u��
        upManuButton.SetActive(false);

        // ���j���[��\���{�^�����A�N�e�B�u��
        downManuButton.SetActive(true);

        // ���j���[��\��
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// ���j���[��\������
    /// </summary>
    public void PushMenuDownButton()
    {
        // ���j���[�\���{�^�����A�N�e�B�u��
        upManuButton.SetActive(true);

        // ���j���[��\���{�^�����A�N�e�B�u��
        downManuButton.SetActive(false);

        // ���j���[�����
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// ���j���[���鏈��
    /// </summary>
    public void PushCloseButton(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
    }

    /// <summary>
    /// �e�A�C�R����������
    /// </summary>
    public void PushMenuIconButton(GameObject iconPanel)
    {
        iconPanel.SetActive(true);
    }

    /// <summary>
    /// �t�H���[�{�^��������
    /// </summary>
    public void PushFollowButton()
    {
        followPanel.SetActive(true);

        StartCoroutine(NetworkManager.Instance.GetFollow(
            result =>
            {
                foreach(var data in result.Follow)
                {   // �t�H���[���X�g
                    if(result.Follow.Count == 0) 
                    {
                        // �f�[�^�����\��
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);
                        break; 
                    }

                    // ���[�U�[�f�[�^����
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[0].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // �A�C�R���ݒ�
                    userData.transform.GetChild(1).GetComponent<Text>().text = data.Name;   // ���O�ݒ�
                    userData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {   // �t�H���[��������
                        //StartCoroutine(NetworkManager.Instance.GetIDCreate(
                        //    result =>
                        //    {

                        //    }));
                    });
                }

                foreach (var data in result.Follower)
                {   // �t�H�����[���X�g
                    if (result.Follower.Count == 0)
                    {
                        // �f�[�^�����\��
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);
                        break;
                    }

                    // ���[�U�[�f�[�^����
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[1].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // �A�C�R���ݒ�
                    userData.transform.GetChild(1).GetComponent<Text>().text = data.Name;   // ���O�ݒ�
                    userData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {   // �t�H���[��������
                        //StartCoroutine(NetworkManager.Instance.GetIDCreate(
                        //    result =>
                        //    {

                        //    }));
                    });
                }

                foreach (var data in result.Mutual)
                {   // ���݃��X�g
                    if (result.Mutual.Count == 0)
                    {
                        // �f�[�^�����\��
                        GameObject noData = Instantiate(noDataPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);
                        break;
                    }

                    // ���[�U�[�f�[�^����
                    GameObject userData = Instantiate(userInfoPrefab, Vector3.zero, Quaternion.identity, scrolltContents[2].transform);

                    userData.transform.GetChild(0).GetComponent<Image>().sprite = iconSprite[data.IconID - 1];  // �A�C�R���ݒ�
                    userData.transform.GetChild(1).GetComponent<Text>().text = data.Name;   // ���O�ݒ�
                    userData.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {   // �t�H���[��������
                        //StartCoroutine(NetworkManager.Instance.GetIDCreate(
                        //    result =>
                        //    {

                        //    }));
                    });
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
    /// �ʒm��������
    /// </summary>
    /// <param name="gameObject"></param>
    public void PushNoticeButton(RectTransform rectTransform)
    {
        // �����ʒu�Ɉړ�
        rectTransform.anchoredPosition = new Vector2(700.0f, rectTransform.anchoredPosition.y);
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
                    contentTexts[0].text = nameInput.text;
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
}
