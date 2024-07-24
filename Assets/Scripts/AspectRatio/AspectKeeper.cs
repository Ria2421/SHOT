//---------------------------------------------------------------
//
// �A�X�y�N�g��ێ� [ AspectKeeper.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/17
// Update:2024/07/17
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // �Đ����ȊO�ł����삷��
public class AspectKeeper : MonoBehaviour
{
    //-------------------------------------------
    // �t�B�[���h

    /// <summary>
    /// �ΏۂƂ���J����
    /// </summary>
    [SerializeField]
    private Camera targetCamera;

    /// <summary>
    /// �ړI�𑜓x
    /// </summary>
    [SerializeField]
    private Vector2 aspectVec;

    //--------------------------------------------
    // ���\�b�h

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        var screenAspect = Screen.width / (float)Screen.height; // ��ʂ̃A�X�y�N�g��
        var targetAspect = aspectVec.x / aspectVec.y; // �ړI�̃A�X�y�N�g��

        var magRate = targetAspect / screenAspect; // �ړI�A�X�y�N�g��ɂ��邽�߂̔{��

        var viewportRect = new Rect(0, 0, 1, 1); // Viewport�����l��Rect���쐬

        if (magRate < 1)
        {
            viewportRect.width = magRate; // �g�p���鉡����ύX
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;// ������
        }
        else
        {
            viewportRect.height = 1 / magRate; // �g�p����c����ύX
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;// ������
        }

        targetCamera.rect = viewportRect; // �J������Viewport�ɓK�p
    }
}