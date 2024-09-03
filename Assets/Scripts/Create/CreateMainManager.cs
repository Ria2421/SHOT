//---------------------------------------------------------------
//
// �N���G�C�g�}�l�[�W���[ [ CreateMainManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateMainManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ���j���[�p�l��
    /// </summary>
    [SerializeField] private GameObject menuPanel;

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
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {

    }

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
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {   // �폜���[�hON�̎�
            DeleteModeFlag = false;
            gameObject.GetComponent<Image>().color = Color.white;
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

        gimmick.AddComponent<BoxCollider2D>();
        gimmick.AddComponent<ObjDrag>();
    }
}
