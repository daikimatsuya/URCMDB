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

    //�v���C���[�̑��x�\��
    public void SetPlayerSpeed(float  speed,float speedBuff)
    {
        this.speed.text = speed + "M/S"; //�v���C���[�̊�{���x�\��
        this.speedBuff.text = speedBuff + "M/S"; //�v���C���[�̈ړ����x��\��
    }
    //UI�I���I�t�؂�ւ�
    public void SetSpeedMeterActive(bool flag)
    {
        this.playerSpeed.SetActive(flag);
        this.playerSpeedBuff.SetActive(flag);
    }
    // Start is called before the first frame update
    void Start()
    {
        speed=playerSpeed.GetComponent<TextMeshProUGUI>();
        speedBuff=playerSpeedBuff.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
