//---------------------------------------------------------------
//
// �X�e�[�W�Z���N�g�}�l�[�W���[ [ SelectManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/18
// Update:2024/07/27
//
//---------------------------------------------------------------
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    //-------------------------------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// ��������{�^���̃v���n�u
    /// </summary>
    [SerializeField] private GameObject buttonPrefub;

    /// <summary>
    /// �e�I�u�W�F�N�g (�X�N���[���r���[)
    /// </summary>
    [SerializeField] private Transform scrollView;

    /// <summary>
    /// �X�N���[���r���[�I�u�W�F�N�g
    /// </summary>
    [SerializeField] private GameObject scrollObj;

    /// <summary>
    /// �G���[�\���I�u�W�F
    /// </summary>
    [SerializeField] private GameObject errorObj;

    //-------------------------------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �m�[�}���X�e�[�W�����擾
        StartCoroutine(NetworkManager.Instance.GetNormalStage(
            result =>
            {
                if (result != null)
                {   // �X�e�[�W�f�[�^�����鎞

                    scrollObj.SetActive(true);  // �X�N���[���E�B���h�E�L����

                    // NetworkManager���擾
                    NetworkManager networkManager = NetworkManager.Instance;

                    foreach (NormalStageResponse stageData in result)
                    {
                        // �v���n�u����I�u�W�F�N�g�̐���
                        GameObject selectBtn = Instantiate(buttonPrefub, Vector3.zero, Quaternion.identity, scrollView);

                        // �{�^���̃e�L�X�g�ɃX�e�[�WID�𔽉f
                        selectBtn.transform.GetChild(0).gameObject.GetComponent<Text>().text = stageData.StageID.ToString();

                        // ���������{�^���ɃN���b�N���̏�����ǉ�
                        selectBtn.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            // NetworkManager��StageNo�EType��ۑ�
                            networkManager.PlayStageNo = stageData.StageID;
                            networkManager.PlayStageType = 1;

                            /* �t�F�[�h���� (��)  
                                         ( "�V�[����",�t�F�[�h�̐F, ����);  */
                            Initiate.DoneFading();
                            Initiate.Fade("UIScene", Color.gray, 2.5f);
                        });
                    }
                }
                else
                {
                    // �G���[�\��
                    errorObj.SetActive(true);
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
    /// �X�e�[�W�I������
    /// </summary>
    public void PushStageSelect(string buttonNum)
    {
        SEManager.Instance.Play(SEPath.MENU_SELECT);

        // ���O�ɂăX�e�[�WNo��\��
        Debug.Log(buttonNum);

        if (buttonNum != "")
        {
            /* �t�F�[�h���� (��)  
                         ( "�V�[����",�t�F�[�h�̐F, ����);  */
            Initiate.DoneFading();
            Initiate.Fade("Stage" + buttonNum + "Scene", Color.gray, 2.5f);
        }
    }

    /// <summary>
    /// �߂�{�^����������
    /// </summary>
    public void PushBackButton()
    {
        SEManager.Instance.Play(SEPath.CANCEL);

        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade("HomeScene", Color.gray, 2.5f);
    }
}
