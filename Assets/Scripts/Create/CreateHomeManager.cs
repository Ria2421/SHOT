//---------------------------------------------------------------
//
// �N���G�C�g�z�[���}�l�[�W���[ [ CreateHomeManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/08/06
// Update:2024/08/06
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateHomeManager : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {

    }

    /// <summary>
    /// �V�[���ړ�����
    /// </summary>
    public void PushButton(string sceneName)
    {
        /* �t�F�[�h���� (��)  
                        ( "�V�[����",�t�F�[�h�̐F, ����);  */
        Initiate.DoneFading();
        Initiate.Fade(sceneName, Color.black, 1.5f);
    }
}
