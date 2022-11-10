using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flick : MonoBehaviour
{
    //スマホのフリック判定を取る。
    //画面タッチされたかどうか
    //タッチされた始点から動いているか。
    //また、どの方向に動かされたか

    private Vector3 position;//Playerのposition

    private Vector3 firstTouchPosition;//始点
    private Vector3 nowPosition;//現在のPosition
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
        //画面をタップされた時
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            firstTouchPosition = new (Input.mousePosition.x , Input.mousePosition.y
                                        , Input.mousePosition.z);

            isTouch = true;
        }

        //画面から指が離れたとき
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(direction == "touch")
            {
                Debug.Log("タッチされました");
            }
            isTouch = false;
        }
            
        //画面がタッチされている時
        if(isTouch)
        {
            nowPosition = new Vector3(Input.mousePosition.x , Input.mousePosition.y , Input.mousePosition.z);

            //上で入力されたPositionをもとに計算する。
            GetDirection();
        }

    }

    void GetDirection()
    {
        var directionX = nowPosition.x - firstTouchPosition.x;
        var directionY = nowPosition.y - firstTouchPosition.y;

        //大きさによって分岐させる
        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                direction = "右";
            }
            else if (-30 > directionX)
            {
                direction = "左";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if(30 < directionY)
            {
                direction = "上";
            }
            else if(-30 > directionY)
            {
                direction = "下";
            }
        }
        else
        {
            direction = "触ってないよ";
        }

        if(direction == null || direction == "触ってないよ")
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
            case "上":
                position.y += moveSpeed;
                break;

            case "下":
                position.y -= moveSpeed;
                break;

            case "右":
                position.x += moveSpeed;
                break;

            case "左":
                position.x -= moveSpeed;
                break;

            default:
                return;
        }

        transform.position = position;
    }


}
