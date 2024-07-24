//---------------------------------------------------------------
//
// ���˗\�����̕`�揈�� [ Physics2DUtill.cs ]
// Author:Kenta Nakamoto
// Data:2024/07/24
// Update:2024/07/24
//
//---------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Physics2DUtil
    {
        //-------------------------------------------
        // �t�B�[���h

        //�ő�q�b�g��
        private const int MAX_HIT_COUNT = 10;

        //--------------------------------------------
        // ���\�b�h

        public static List<Vector2> RefrectionLinePoses(Vector2 position, Vector2 direction, float length, LayerMask layerMask)
        {
            var points = new List<Vector2>() { position };
            var hit = Physics2D.Raycast(position, direction, length, layerMask);
            int hitcount = 0;
            while (hit)
            {
                if (hit.point != position)
                {
                    // HIT�����ꏊ�̏����L�^
                    position = hit.point;
                    points.Add(position);
                    length -= hit.distance;
                    direction = Vector2.Reflect(direction, hit.normal);
                }
                else
                {
                    // HIT�������ꏊ�Ŕ�������ꍇ�����邽�߂����h���ړI�Adirection���������炷
                    position = hit.point + direction;
                }

                hit = Physics2D.Raycast(position, direction, length, layerMask);

                // �����ꏊ�ŕ�����q�b�g���Ă��܂����Ƃ��������[�v���Ă��܂��̂�
                // �ő�q�b�g�񐔈ȏ�̏ꍇ�̓��[�v�𔲂���悤�ɂ���
                hitcount += 1;
                if (hitcount > MAX_HIT_COUNT) break;
            }

            points.Add(position + direction * length);
            return points;
        }
    }
}