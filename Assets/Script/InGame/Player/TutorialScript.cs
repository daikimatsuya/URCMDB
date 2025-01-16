using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チュートリアルを管理
public class TutorialScript : MonoBehaviour
{
    private PlayerScript player;

    //チュートリアル管理用
    private void TutorialContoroller()
    {
        if(player == null)
        {
            SetPlayer();
        }
        else
        {

        }
    }
    //スティック入力検知用
    private bool DetectStick()
    {
        if(Input.GetAxis("LeftStickX")!=0&&Input.GetAxis("LeftStickY")!=0)
        {
            return true;
        }
        return false;
    }

    //右トリガー入力検知用
    private bool DetectRTrigger()
    {
        if (Input.GetAxis("RightTrigger") != 0)
        {
            return true;
        }
        return false;
    }


    //プレイヤー取得用
    private void SetPlayer()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
