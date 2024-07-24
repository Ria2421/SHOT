//---------------------------------------------------------------
//
// �W���G���}�l�[�W���[ [ JewelManager.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/24
// Update:2024/07/24
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jewel : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    Material material = null;

    [Header("�F�ύX�X�p��")]
    public float Chnge_Color_Time = 0.1f;

    [Header("�ύX�̊��炩��")]
    public float Smooth = 0.01f;

    [Header("�F��")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;// 0 ~ 1

    [Header("�ʓx")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;// 0 ~ 1

    [Header("���x")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;// 0 ~ 1

    [Header("�F�� MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;// 0 ~ 1

    [Header("�F�� MIN")]
    [Range(0, 1)] public float HSV_Hue_min = 0.0f;// 0 ~ 1

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        material = GetComponent<Renderer>().material;
        HSV_Hue = HSV_Hue_min;
        StartCoroutine("Change_Color");
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        
    }

    /// <summary>
    /// �F�̕ύX����
    /// </summary>
    /// <returns></returns>
    IEnumerator Change_Color()
    {
        HSV_Hue += Smooth;

        if (HSV_Hue >= HSV_Hue_max)
        {
            HSV_Hue = HSV_Hue_min;
        }

        material.color = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);

        yield return new WaitForSeconds(Chnge_Color_Time);

        StartCoroutine("Change_Color");
    }

    /// <summary>
    /// �g���K�[����
    /// </summary>
    /// <param name="collision">�ڐG�����I�u�W�F�N�g</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {   // �v���C���[���̎�

            // �W���G���j��
            Destroy(this.gameObject);
        }
    }
}