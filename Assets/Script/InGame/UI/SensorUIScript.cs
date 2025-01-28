using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//センサーUIの色を変える
public class SensorUIScript : MonoBehaviour
{
    [SerializeField] private Image up;
    [SerializeField] private Image down;
    [SerializeField] private Image left;
    [SerializeField] private Image right;

    SensorScript ss;

    private SensorScript.HIT hit;
    private SensorScript.HIT hitChildren;

    //センサーUI管理
    public void SensorUIController()
    {
        SetHit();   //センサー情報取得
        if (hit == null || hitChildren == null)
        {
            return;
        }
        SensorChecker(up,hit.up,hitChildren.up);    //上センサー表示
        SensorChecker(down,hit.down,hitChildren.down);  //下センサー表示
        SensorChecker(left,hit.left, hitChildren.left); //左センサー表示
        SensorChecker(right,hit.right, hitChildren.right);  //右センサー表示
    }
    //センサーUIのフラグチェック
    private void SensorChecker(in Image image,bool flag,bool flagChild)
    {
        if (flag)
        {
            SetCloseToObject(image);   //赤表示
            return;
        }
        if (flagChild)
        {
            SetGreen(image);    //緑表示
            return;
        }

        SetSafe(image); //表示なし
    }
    //ヒットフラグ取得
    private void SetHit()
    {
        if (ss == null)
        {
            if (GameObject.FindWithTag("Sensor") != null)
            {
                ss = GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
            }
        }
        else
        {
            hit = ss.GetHit();  //内センサー情報取得
            hitChildren = ss.GetHitChild(); //外センサー上表取得
        }
    }
    //センサーUIを透明にする
    private void SetSafe(in Image image)
    {
        image.color = Color.clear;
    }
    //センサーUIを緑にする
    private void SetGreen(in Image image)
    {
        image.color = Color.green;
    }
    //センサーUIを赤にする
    private void SetCloseToObject(in Image image)
    {
        image.color = Color.red;
    }
    //センサーオンオフ
    public void SetSensorActive(bool flag)
    {
        up.enabled = flag;
        down.enabled = flag;
        left.enabled = flag;
        right.enabled = flag;

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
