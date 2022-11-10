using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flick : MonoBehaviour
{
    //�X�}�z�̃t���b�N��������B
    //��ʃ^�b�`���ꂽ���ǂ���
    //�^�b�`���ꂽ�n�_���瓮���Ă��邩�B
    //�܂��A�ǂ̕����ɓ������ꂽ��

    private Vector3 position;//Player��position

    private Vector3 firstTouchPosition;//�n�_
    private Vector3 nowPosition;//���݂�Position
    private string direction;

    bool isTouch;
    bool isMove;
    [SerializeField]
    private float moveSpeed;


    private void Start()
    {
        
    }

    private void Update()
    {
        if(isMove)
        {
            Move();
        }
        else
        {
            OnFlick();
        }
    }

    void OnFlick()
    {
        //��ʂ��^�b�v���ꂽ��
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            firstTouchPosition = new (Input.mousePosition.x , Input.mousePosition.y
                                        , Input.mousePosition.z);

            isTouch = true;
        }

        //��ʂ���w�����ꂽ�Ƃ�
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(direction == "touch")
            {
                Debug.Log("�^�b�`����܂���");
            }
            isTouch = false;
        }
            
        //��ʂ��^�b�`����Ă��鎞
        if(isTouch)
        {
            nowPosition = new Vector3(Input.mousePosition.x , Input.mousePosition.y , Input.mousePosition.z);

            //��œ��͂��ꂽPosition�����ƂɌv�Z����B
            GetDirection();
        }

    }

    void GetDirection()
    {
        var directionX = nowPosition.x - firstTouchPosition.x;
        var directionY = nowPosition.y - firstTouchPosition.y;

        //�傫���ɂ���ĕ��򂳂���
        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                direction = "�E";
            }
            else if (-30 > directionX)
            {
                direction = "��";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if(30 < directionY)
            {
                direction = "��";
            }
            else if(-30 > directionY)
            {
                direction = "��";
            }
        }
        else
        {
            direction = "�G���ĂȂ���";
        }

        if(direction == null || direction == "�G���ĂȂ���")
        {
            return;
        }

        isTouch = false;
        isMove = true;
    }

    void Move()
    {
        position = transform.position;

        switch(direction)
        {
            case "��":
                position.y += moveSpeed;
                break;

            case "��":
                position.y -= moveSpeed;
                break;

            case "�E":
                position.x += moveSpeed;
                break;

            case "��":
                position.x -= moveSpeed;
                break;

            default:
                return;
        }

        transform.position = position;
    }


}
