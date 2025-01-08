using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーの速度表示
public class PlayerSpeedMeterScript : MonoBehaviour
{
    [SerializeField] private GameObject playerSpeed;
    private TextMeshProUGUI speed;
    [SerializeField] private GameObject playerSpeedBuff;
    private TextMeshProUGUI speedBuff;

    //プレイヤーの速度表示
    public void SetPlayerSpeed(float  speed,float speedBuff)
    {
        this.speed.text = speed + "M/S"; //プレイヤーの基本速度表示
        this.speedBuff.text = "+"+(speedBuff-speed) + "M/S"; //プレイヤーの移動速度を表示
        if (speedBuff - speed <= 0)
        {
            playerSpeedBuff.SetActive(false);
        }
        else
        {
            playerSpeedBuff.SetActive(true);
        }
    }
    //UIオンオフ切り替え
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
