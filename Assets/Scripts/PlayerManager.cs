//---------------------------------------------------------------
//
// �v���C���[ [ Player.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/17
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float speed;

    /// <summary>
    /// �w(�}�E�X)�̃^�b�v�J�n�ʒu
    /// </summary>
    private Vector2 startPos;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // ���@��Rigidbody2D���擾
        this.rigid2d = GetComponent<Rigidbody2D>();

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
        {   // �}�E�X�N���b�N��

            // �N���b�N���W��ۑ�
            this.startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {   // �}�E�X�N���b�N�𗣂�����

            // �������ʒu��ۑ�
            Vector2 endPos = Input.mousePosition;

            // ������������Ƃ͋t�̃x�N�g�����v�Z���A���K��
            Vector2 startDirection = (startPos - endPos).normalized;

            // �X�s�[�h�萔 * �v�Z�����͂̌���
            this.rigid2d.AddForce(startDirection * speed);
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
}
