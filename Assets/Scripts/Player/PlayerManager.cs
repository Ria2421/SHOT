//---------------------------------------------------------------
//
// �v���C���[�}�l�[�W���[ [ PlayerManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/24
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

public class PlayerManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���@�̈ړ����x��
    /// </summary>
    [SerializeField] float playerSpeed;

    /// <summary>
    /// �N���A���U���g�p�l��
    /// </summary>
    [SerializeField] GameObject clearResultPanel;

    /// <summary>
    /// �Q�[���I�[�o�[���U���g�p�l��
    /// </summary>
    [SerializeField] GameObject gameOverPanel;

    /// <summary>
    /// ���@�̓����蔻��
    /// </summary>
    private Rigidbody2D rigid2d;
    
    /// <summary>
    /// ���@�̃X�s�[�h
    /// </summary>
    private float speed = 0.0f;

    /// <summary>
    /// �w(�}�E�X)�̃^�b�v�J�n�ʒu
    /// </summary>
    private Vector2 startPos = Vector2.zero;

    //======================================
    // ���˗\�����p

    /// <summary>
    /// ���˕���
    /// </summary>
    private Vector2 launchDirection;

    /// <summary>
    /// �h���b�O�J�n�ʒu���擾����
    /// </summary>
    private Vector2 dragStart = Vector2.zero;

    /// <summary>
    /// ���˗\�����̍ő�l
    /// </summary>
    [SerializeField] private float maxMagnitude;

    /// <summary>
    /// �\�����̕`��
    /// </summary>
    [SerializeField] private LineRenderer predictionLineRenderer;

    /// <summary>
    /// wallLayer���w��
    /// </summary>
    [SerializeField] private LayerMask wallLayer;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// �N������
    /// </summary>
    private void Awake()
    {
        //�I�u�W�F�N�g�̈ʒu���擾���邽�߂Ƀ��W�b�h�{�f�B�̎擾
        rigid2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // ���@�̑��x�ݒ�
        this.speed = playerSpeed;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        // �}�E�X�̓����Ɣ��Ε����ɔ��˂��鏈��

        if (Input.GetMouseButtonDown(0))
        {   // �}�E�X�N���b�N�J�n��

            // �N���b�N���W��ۑ�
            this.startPos = Input.mousePosition;

            //�h���b�O�̊J�n�ʒu�����[���h���W�Ŏ擾����
            dragStart = GetMousePosition();

            //�`����̗\����L���ɂ���
            predictionLineRenderer.enabled = true;

        }
        else if (Input.GetMouseButton(0))
        {   // �N���b�N�z�[���h��

            //�h���b�O���̃}�E�X�̈ʒu�����[���h���W�Ŏ擾����B
            var position = GetMousePosition();

            //�h���b�O�J�n�_����̋������擾����
            var currentForce = dragStart - position;

            // MaxMagnitude�ɒ����̒����̐������w�肵�Ă�������𒴂���ꍇ�́A�ő�l�ƂȂ�悤�ɂ��܂��B
            if (currentForce.magnitude > maxMagnitude)
            {
                currentForce *= maxMagnitude / currentForce.magnitude;
            }

            // �\������`�悷��
            DrawLineOfReflection(currentForce);
        }
        else if (Input.GetMouseButtonUp(0))
        {   // �}�E�X�N���b�N�𗣂�����

            // �������ʒu��ۑ�
            Vector2 endPos = Input.mousePosition;

            // ������������Ƃ͋t�̃x�N�g�����v�Z���A���K��
            Vector2 startDirection = (startPos - endPos).normalized;

            // �X�s�[�h�萔 * �v�Z�����͂̌���
            this.rigid2d.AddForce(startDirection * speed);

            // �\�����`��I��
            predictionLineRenderer.enabled = false;
        }

        // �e�X�g�p�F�X�y�[�X�L�[�����Œ�~
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.rigid2d.velocity *= 0;
        }
    }

    /// <summary>
    /// ����ڐG���̏���
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {   // �S�[������

            // �N���A���U���gON
            clearResultPanel.SetActive(true);

            // ���x0��
            this.rigid2d.velocity *= 0;

        }

        if (collision.gameObject.tag == "Thunder")
        {   // ������

            // �Q�[���I�[�o�[�p�l���\��
            gameOverPanel.SetActive(true);

            // �v���C���[�j��
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ���[���h���W�̃}�E�X�̏ꏊ���擾
    /// </summary>
    /// <returns>�}�E�X�|�W�V����</returns>
    private Vector2 GetMousePosition()
    {
        // �}�E�X�̏ꏊ���擾
        Vector2 position = Input.mousePosition;

        // ���[���h���W�ɕϊ�
        return Camera.main.ScreenToWorldPoint(position);
    }

    /// <summary>
    /// ���˗\�����̕`��
    /// </summary>
    /// <param name="currentForce">���˗\�����̕����Ƒ傫��</param>
    private void DrawLineOfReflection(Vector2 currentForce)
    {
        var poses = Physics2DUtil.RefrectionLinePoses(rigid2d.position, currentForce.normalized, maxMagnitude, wallLayer).ToArray();
        predictionLineRenderer.positionCount = poses.Length;
        for (var i = 0; i < poses.Length; i++)
        {
            predictionLineRenderer.SetPosition(i, poses[i]);
        }
    }
}
