using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`���[�g���A�����Ǘ�
public class TutorialScript : MonoBehaviour
{
    private PlayerScript player;

    //�`���[�g���A���Ǘ��p
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
    //�X�e�B�b�N���͌��m�p
    private bool DetectStick()
    {
        if(Input.GetAxis("LeftStickX")!=0&&Input.GetAxis("LeftStickY")!=0)
        {
            return true;
        }
        return false;
    }

    //�E�g���K�[���͌��m�p
    private bool DetectRTrigger()
    {
        if (Input.GetAxis("RightTrigger") != 0)
        {
            return true;
        }
        return false;
    }


    //�v���C���[�擾�p
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
