//---------------------------------------------------------------
//
// �I�u�W�F�h���b�O�ړ��p�X�N���v�g [ ObjDrag.cs ]
// Author:Kenta Nakamoto
// Data:2024/09/03
// Update:2024/09/03
//
//---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjDrag : MonoBehaviour
{
    //---------------------------------
    // �t�B�[���h

    /// <summary>
    /// �^�b�`���̃X�N���[�����W
    /// </summary>
    private Vector3 screenPoint;

    private CreateMainManager mainManager;

    //---------------------------------
    // ���\�b�h

    /// <summary>
    /// ��������
    /// </summary>
    void Start()
    {
        // �}�l�[�W���[�̎擾
        mainManager = GameObject.Find("CreateMainManager").GetComponent<CreateMainManager>();
    }

    /// <summary>
    /// �h���b�O����
    /// </summary>
    void OnMouseDrag()
    {
        if (mainManager.DeleteModeFlag)
        {   // �폜���[�h�̎�
            Destroy(this.gameObject); 
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
   if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))return;
#endif

        // �^�b�`���̃��[���h���W���X�N���[�����W�ɕϊ�
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // ���݂̃X�N���[�����W���擾
        float screenX = Input.mousePosition.x;
        float screenY = Input.mousePosition.y;
        float screenZ = screenPoint.z;

        Vector3 currentScreenPoint = new Vector3(screenX, screenY, screenZ);

        // ���[���h���W�ɕϊ���A���f
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        transform.position = currentPosition;
    }
}
