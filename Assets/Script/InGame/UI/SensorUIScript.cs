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
        SetHit();
        SensorChecker(up,hit.up,hitChildren.up);
        SensorChecker(down,hit.down,hitChildren.down);
        SensorChecker(left,hit.left, hitChildren.left);
        SensorChecker(right,hit.right, hitChildren.right);
    }
    //センサーUIのフラグチェック
    private void SensorChecker(Image image,bool flag,bool flagChild)
    {
        if (flag)
        {
            SetCloseToObject(image);
            return;
        }
        if (flagChild)
        {
            SetGreen(image);
            return;
        }

        SetSafe(image);
    }
    //ヒットフラグ取得
    private void SetHit()
    {
        if (ss == null)
        {
            ss = GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
        }
        else
        {
            hit = ss.GetHit();
            hitChildren = ss.GetHitChild();
        }
    }
    //センサーUIを透明にする
    private void SetSafe(Image image)
    {
        image.color = Color.clear;
    }
    //センサーUIを緑にする
    private void SetGreen(Image image)
    {
        image.color = Color.green;
    }
    //センサーUIを赤にする
    private void SetCloseToObject(Image image)
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
        ss=GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
