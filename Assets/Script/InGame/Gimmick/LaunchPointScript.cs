using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ˑ�Ǘ�
public class LaunchPointScript : MonoBehaviour
{
    private Transform tf;

    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;
    [SerializeField] private Vector2 maxRow; 

    private bool isControll;
    private Vector2 rowling;
    private bool isStart;

    //���ˑ�Ǘ��֐�
    public void LaunchPointController(in bool isPause)
    {
        if(isPause)
        {
            return;
        }
        if (isStart)
        {
            if (isControll) //����ł���悤�ɂȂ��Ă�����////
            {
                Move();
            }//////////////////////////////////////////////////
        }
    }
    //������ύX
    private void Move()
    {
        //���͂Ŋp�x�����Z///////////////////////////////////////////////////////////////////

        Vector2 rowlingBuff = Vector2.zero;

        //�R���g���[���[����/////////////////////

        float axisY = Input.GetAxis("LeftStickY");
        rowlingBuff.x = rowlingSpeedY  * -axisY;

        float axisX = Input.GetAxis("LeftStickX");
        rowlingBuff.y = rowlingSpeedX * axisX;

        //////////////////////////////////////////

        //�L�[�{�[�h����//////////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rowlingBuff.x = rowlingSpeedY;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rowlingBuff.x = -rowlingSpeedY;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rowlingBuff.y = -rowlingSpeedX;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rowlingBuff.y = rowlingSpeedX;
        }
        /////////////////////////////////////////////////////////////////////////

        rowling += rowlingBuff;

        //////////////////////////////////////////////////////////////////////////////////////
        
        //�p�x��␳///////////////////////////
        if (rowling.y <= -maxRow.y)
        {
            rowling.y = -maxRow.y;
        }
        if (rowling.y >= maxRow.y)
        {
            rowling.y = maxRow.y;
        }
        if(rowling.x <= 0)
        {
            rowling.x = 0;
        }
        if(rowling.x >= maxRow.x)
        {
            rowling.x = maxRow.x;
        }
        ////////////////////////////////////////
        
        tf.localEulerAngles = new Vector3(rowling.x, rowling.y,0);  //�g�����X�t�H�[���ɑ��
    }
    #region �l�󂯓n��
    public void SetStart(bool flag)
    {
        isStart = flag;
    }
    public void Shoot()
    {
        isControll= false;
    }
    public void Bombed()
    {
        isControll = true;
    }

    public Vector3 GetPos()
    {
        return tf.position;
    }
    public Vector2 GetRowling()
    {
        return tf.eulerAngles;
    }
    public Transform GetTransform()
    {
        return tf.transform;
    }
    #endregion
    public void AwakeLaunchPoint()
    {
        tf = GetComponent<Transform>();
        rowling = new Vector2(tf.localEulerAngles.x, tf.localEulerAngles.y);

        isControll = true;
        isStart = false;
    }

}
