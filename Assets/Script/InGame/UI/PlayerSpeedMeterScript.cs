using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//�v���C���[�̑��x�\��
public class PlayerSpeedMeterScript : MonoBehaviour
{
    [SerializeField] private GameObject playerSpeed;
    private TextMeshProUGUI speed;
    [SerializeField] private GameObject playerSpeedBuff;
    private TextMeshProUGUI speedBuff;
    private bool viewFlag;

    //�v���C���[�̑��x�\��
    public void SetPlayerSpeed(float  speed,float speedBuff)
    {
        this.speed.text = speed + "M/S"; //�v���C���[�̊�{���x�\��
        this.speedBuff.text = "+"+(speedBuff-speed) + "M/S"; //�v���C���[�̈ړ����x��\��
        if (speedBuff - speed <= 0)
        {
            viewFlag = false;
        }
        else
        {
            viewFlag = true;
        }
    }
    //UI�I���I�t�؂�ւ�
    public void SetSpeedMeterActive(bool flag)
    {
        this.playerSpeed.SetActive(flag);
        if (!viewFlag)
        {
            this.playerSpeedBuff.SetActive(false);
        }
        else
        {
            this.playerSpeedBuff.SetActive(flag);
        }
    }
    public void StartPlayerSpeedMeterScript()
    {
        speed = playerSpeed.GetComponent<TextMeshProUGUI>();
        speedBuff = playerSpeedBuff.GetComponent<TextMeshProUGUI>();
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
