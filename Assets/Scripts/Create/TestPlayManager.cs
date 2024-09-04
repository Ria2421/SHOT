//---------------------------------------------------------------
//
// �e�X�g�v���C�}�l�[�W���[ [ TestPlayManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/04
// Update:2024/09/04
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �M�~�b�N�i�[�p�̐e�I�u�W�F�N�g
    /// </summary>
    [SerializeField] private GameObject parentObj;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �X�e�[�W�f�[�^�̎󂯎��E�z�u
        var stageDatas = GameObject.Find("StageDataObject").GetComponent<StageDataObject>().GetData();
        foreach (GimmickData data in stageDatas)
        {
            // Resources�t�H���_����M�~�b�N�̃I�u�W�F�N�g���擾�E����
            GameObject obj = (GameObject)Resources.Load(data.ID.ToString());
            Instantiate(obj, new Vector3(data.X, data.Y, 0), Quaternion.identity);
        }
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// ���v���C����
    /// </summary>
    public void PushGameReplay()
    {
        // �V�[���̍ēǂ�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �z�[���J�ڏ���
    /// </summary>
    public void PushHome()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateHomeScene", Color.white, 2.5f);

        Destroy(GameObject.Find("StageDataObject"));
    }

    /// <summary>
    /// �N���G�C�g��ʑJ�ڏ���
    /// </summary>
    public void PushBackCreate()
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("CreateMainScene", Color.white, 2.5f);
    }
}
