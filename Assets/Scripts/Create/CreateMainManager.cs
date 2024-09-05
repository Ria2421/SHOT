//---------------------------------------------------------------
//
// �N���G�C�g�}�l�[�W���[ [ CreateMainManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class CreateMainManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���j���[�p�l��
    /// </summary>
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// �S�~���摜(��)
    /// </summary>
    [SerializeField] private Sprite closeBox;

    /// <summary>
    /// �S�~���摜(�J)
    /// </summary>
    [SerializeField] private Sprite openBox;

    /// <summary>
    /// Player�ݒu���ӃI�u�W�F
    /// </summary>
    [SerializeField] private RectTransform cautionPlayer;

    /// <summary>
    /// Goal�ݒu���ӃI�u�W�F
    /// </summary>
    [SerializeField] private RectTransform cautionGoal;

    /// <summary>
    /// �N���G�C�g�f�[�^�i�[�p
    /// </summary>
    private List<GimmickData> createDatas = new List<GimmickData>();

    /// <summary>
    /// �폜���[�h�t���O�v���p�e�B
    /// </summary>
    public bool DeleteModeFlag {  get; private set; }

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    private void Start()
    {
        // �폜���[�h�I�t
        DeleteModeFlag = false;

        // �X�e�[�W�f�[�^�ۊǗp�I�u�W�F�N�g�̑��݊m�F
        GameObject check = GameObject.Find("StageDataObject");

        if (check == null)
        {   // �f�[�^�ۊǗp�I�u�W�F�N�g�̐���
            GameObject stageDataObject = new GameObject("StageDataObject");
            stageDataObject.AddComponent<StageDataObject>();
            DontDestroyOnLoad(stageDataObject);    // Scene�J�ڂŔj������Ȃ悤�ɂ���
        }
        else
        {   // �f�[�^�����ɃM�~�b�N��z�u
            var gimmckDatas = check.GetComponent<StageDataObject>().GetData();
            foreach(GimmickData data in gimmckDatas)
            {
                // Resources�t�H���_����M�~�b�N�̃I�u�W�F�N�g���擾�E����
                GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
                GameObject gimmick = Instantiate(obj, new Vector3(data.X,data.Y,0), Quaternion.identity);

                // �h���b�O�p�R���|�[�l���g�̒ǉ�
                gimmick.AddComponent<BoxCollider2D>();
                gimmick.AddComponent<ObjDrag>();

                // �^�O��t�^
                gimmick.tag = "Create";

                if (gimmick.name == "12(Clone)")
                {   // �v���C���[�������ɓ����Ȃ��悤�ɃR���|�[�l���g���폜
                    Destroy(gimmick.GetComponent<PlayerManager>());
                }
            }
        }
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// ���ӏ����\������
    /// </summary>
    /// <param name="cautionImage">�ړ��Ώ�</param>
    private void MoveCaution(RectTransform cautionImage)
    {
        cautionImage.DOAnchorPos(new Vector2(0f, cautionImage.anchoredPosition.y), 0.6f).SetEase(Ease.OutBack);
    }

    //=====================================
    // �{�^����������

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
    /// �z�[���{�^��������
    /// </summary>
    public void PushHomeButton()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.black, 1.5f);
        
    }

    /// <summary>
    /// �S�~���{�^����������
    /// </summary>
    public void PushDustBox(GameObject gameObject)
    {
        if (!DeleteModeFlag)
        {   // �폜���[�hOFF�̎�
            DeleteModeFlag = true;
            Image dustBoxImg = gameObject.GetComponent<Image>();
            dustBoxImg.sprite = openBox;    // �摜�ύX
            dustBoxImg.color = Color.red;   // �F�ύX
        }
        else
        {   // �폜���[�hON�̎�
            DeleteModeFlag = false;
            Image dustBoxImg = gameObject.GetComponent<Image>();
            dustBoxImg.sprite = closeBox;
            dustBoxImg.color = Color.white;
        }
    }

    /// <summary>
    /// �M�~�b�N�{�^����������
    /// </summary>
    public void PushGimmickButton(GameObject gameObject)
    {
        Debug.Log(gameObject.name); // �M�~�b�N���̕\��

        // Resources�t�H���_����M�~�b�N�̃I�u�W�F�N�g���擾�E����
        GameObject obj = (GameObject)Resources.Load(gameObject.name);
        GameObject gimmick = Instantiate(obj,Vector3.zero,Quaternion.identity);

        // �h���b�O�p�R���|�[�l���g�̒ǉ�
        gimmick.AddComponent<BoxCollider2D>();
        gimmick.AddComponent<ObjDrag>();

        // �^�O��t�^
        gimmick.tag = "Create";

        if(gimmick.name == "12(Clone)")
        {   // �v���C���[�������ɓ����Ȃ��悤�ɃR���|�[�l���g���폜
            Destroy(gimmick.GetComponent<PlayerManager>());
            Destroy(gimmick.GetComponent<Rigidbody2D>());
        }
    }

    /// <summary>
    /// �e�X�g�{�^����������
    /// </summary>
    public void PushTestPlay()
    {
        // �v���C���[�E�S�[���ݒu�t���O
        bool plFlag = false;
        bool glFlag = false;

        // Create�^�O�̃Q�[���I�u�W�F�N�g�����ׂĎ擾����
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Create");

        // ��������
        foreach (GameObject obj in objs)
        {
            // obj����ID�ɕϊ�
            string name = obj.name.Replace("(Clone)", "");

            // �ݒu����
            if(name == "10")
            {   // �S�[���L
                glFlag = true;
            }else if(name == "12")
            {   // PL�L
                plFlag = true;
            }

            // �M�~�b�N�f�[�^�̍쐬
            GimmickData gimmickData = new GimmickData();
            gimmickData.ID = int.Parse(name);
            gimmickData.X = (float)Math.Round(obj.transform.position.x, 3); // �����_�͑�R�ʂ܂�
            gimmickData.Y = (float)Math.Round(obj.transform.position.y, 3);
            Debug.Log(gimmickData.ID + ":" + " x=" + gimmickData.X + " y=" +gimmickData.Y); // �f�[�^�̕\��

            // ���X�g�ɒǉ�
            createDatas.Add(gimmickData);
        }

        // �e�t���O���ɒ��ӏ����\���E�X�e�[����񏉊���
        if(!plFlag && !glFlag)
        {   // ��������
            MoveCaution(cautionPlayer);
            MoveCaution(cautionGoal);
            createDatas.Clear();
            return;
        }else if (!plFlag)
        {   // PL����
            MoveCaution(cautionPlayer);
            createDatas.Clear();
            return;
        }else if(!glFlag)
        {   // �S�[������
            MoveCaution(cautionGoal);
            createDatas.Clear();
            return;
        }
        else
        {
            // �X�e�[�W�f�[�^��ۊǃI�u�W�F�Ɏ󂯓n��
            var dataObj = GameObject.Find("StageDataObject").GetComponent<StageDataObject>();
            dataObj.SetData(createDatas);
        }

        /* �t�F�[�h���� (��)  
                ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("TestPlayScene", Color.white, 2.5f);
    }

    /// <summary>
    /// ���Ӊ�������
    /// </summary>
    public void PushCation(RectTransform rectTransform)
    {
        // �����ʒu�Ɉړ�
        rectTransform.anchoredPosition = new Vector2(1000.0f, rectTransform.anchoredPosition.y);
    }
}
