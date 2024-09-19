//---------------------------------------------------------------
//
// �v���C���[�}�l�[�W���[ [ PlayerManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/24
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility;
using static UnityEditor.PlayerSettings;

public class PlayerManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �N���A���U���g�p�l��
    /// </summary>
    private GameObject clearResultPanel = null;

    /// <summary>
    /// �Q�[���I�[�o�[���U���g�p�l��
    /// </summary>
    private GameObject gameOverPanel = null;

    /// <summary>
    /// �N���A�t���O
    /// </summary>
    private bool clearFlag;

    //======================================
    // ���˗\�����p

    ///// <summary>
    ///// wallLayer���w��
    ///// </summary>
    [SerializeField] private LayerMask wallLayer;

    /// <summary>
    /// ��������
    /// </summary>
    private Rigidbody2D physics = null;

    /// <summary>
    /// ���˕���
    /// </summary>
    [SerializeField]
    private LineRenderer direction = null;

    /// <summary>
    /// �ő�t�^�͗�
    /// </summary>
    private const float MaxMagnitude = 4f;

    /// <summary>
    /// ���˕����̗�
    /// </summary>
    private Vector3 currentForce = Vector3.zero;

    /// <summary>
    /// ���C���J����
    /// </summary>
    private Camera mainCamera = null;

    /// <summary>
    /// ���C���J�������W
    /// </summary>
    private Transform mainCameraTransform = null;

    /// <summary>
    /// �h���b�O�J�n�_
    /// </summary>
    private Vector3 dragStart = Vector3.zero;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// �N������
    /// </summary>
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "CreateMainScene")
        {
            BGMManager.Instance.Play(BGMPath.GAME);

            // ���U���g�p�l���̎擾
            var panel = GameObject.Find("Panel");   // UI�p�l���̎擾
            clearResultPanel = panel.transform.Find("GameClearPanel").gameObject;
            gameOverPanel = panel.transform.Find("GameOverPanel").gameObject;
        }

        // �t���O�̏�����
        clearFlag = false;

        physics = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if(clearFlag) { return; }

        // �}�E�X�̓����Ɣ��Ε����ɔ��˂��鏈��

        if (Input.GetMouseButtonDown(0))
        {   // �}�E�X�N���b�N�J�n��
            SEManager.Instance.Play(SEPath.SHOT_CHARGE);

            dragStart = GetMousePosition();

            direction.enabled = true;

            direction.SetPosition(0, physics.position);
            direction.SetPosition(1, physics.position);
        }
        else if (Input.GetMouseButton(0))
        {   // �N���b�N�z�[���h��
            var position = GetMousePosition();

            currentForce = position - dragStart;

            if (currentForce.magnitude > MaxMagnitude)
            {
                currentForce *= MaxMagnitude / currentForce.magnitude;
            }

            direction.SetPosition(0, physics.position);
            direction.SetPosition(1, physics.position + new Vector2(-currentForce.x, -currentForce.y));
        }
        else if (Input.GetMouseButtonUp(0))
        {   // �}�E�X�N���b�N�𗣂�����
            SEManager.Instance.Play(SEPath.SHOT);

            direction.enabled = false;
            Flip(currentForce * 6f);
        }
    }

    /// <summary>
    /// ����X�V����
    /// </summary>
    void FixedUpdate()
    {
        if (clearFlag) { return; }
        physics.velocity *= 0.95f;
    }

    /// <summary>
    /// �}�E�X���W�����[���h���W�ɕϊ����Ď擾
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMousePosition()
    {
        // �}�E�X����擾�ł��Ȃ�Z���W��⊮����
        var position = Input.mousePosition;
        position.z = mainCameraTransform.position.z;
        position = mainCamera.ScreenToWorldPoint(position);
        position.z = 0;

        return position;
    }

    /// <summary>
    /// �{�[�����͂���
    /// </summary>
    /// <param name="force"></param>
    public void Flip(Vector3 force)
    {
        // �u�ԓI�ɗ͂������Ă͂���
        physics.AddForce(-force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// ����ڐG���̏���
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (clearFlag) { return; }

        SEManager.Instance.Play(SEPath.OBJ_BUTUKARI);

        if (collision.gameObject.tag == "Finish")
        {   // �S�[������
            SEManager.Instance.Play(SEPath.CLEAR,0.7f);

            // �������O�o�^
            StorePlayLog(true);

            // �N���A���U���gON
            clearResultPanel.SetActive(true);

            // ���x0��
            physics.velocity *= 0;

            clearFlag = true;
        }
        else if (collision.gameObject.tag == "Trap")
        {   // �g���b�v����
            SEManager.Instance.Play(SEPath.FAILED);

            // ���s���O�o�^
            StorePlayLog(false);

            // �Q�[���I�[�o�[�p�l���\��
            gameOverPanel.SetActive(true);

            // ���x0��
            physics.velocity *= 0;

            clearFlag = true;
        }
    }

    /// <summary>
    /// �g���K�[����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (clearFlag) { return; }

        SEManager.Instance.Play(SEPath.OBJ_BUTUKARI);

        if (collision.gameObject.tag == "Trap")
        {   // 㩔���

            // ���s���O�o�^
            StorePlayLog(false);

            // �Q�[���I�[�o�[�p�l���\��
            gameOverPanel.SetActive(true);

            // ���x0��
            physics.velocity *= 0;

            clearFlag = true;
        }
    }

    /// <summary>
    /// �v���C���O�o�^����
    /// </summary>
    /// <param name="stageID">�X�e�[�WID</param>
    /// <param name="type">   [1:�m�[�}�� 2:�N���G�C�g]</param>
    /// <param name="flag">   �N���A�t���O</param>
    private void StorePlayLog(bool flag)
    {
        string name = SceneManager.GetActiveScene().name;   // �V�[�����擾
        int stageID = 0;
        int type = 0;

        switch (name)
        {
            case "UIScene":
                stageID = GameObject.Find("GameManager").GetComponent<GameManager>().GetStageNo();
                type = 1;
                break;

            case "CustomGameScene":
                stageID = GameObject.Find("StageDataObject").GetComponent<StageDataObject>().GetID();
                type = 2;
                break;

            default:
                break;
        }

        // �v���C���O�o�^API�Ăяo��
        StartCoroutine(NetworkManager.Instance.StorePlayLog(
            stageID,
            type,
            flag,
            result =>
            {
                Debug.Log("���O�o�^����");
                Destroy(GetComponent<Rigidbody2D>());
            }));
    }
}
