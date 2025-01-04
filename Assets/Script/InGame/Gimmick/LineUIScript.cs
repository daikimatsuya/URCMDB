using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//予測線管理
public class LineUIScript : MonoBehaviour
{
    Transform pos;
    LineRenderer line;

    private int timeBuff;
    private bool red;
    //private bool brock;
    private RaycastHit hit;
    private bool isShade;

    [SerializeField] private float randPow;
    [SerializeField] private float colorChangeTime;

    //予測線設置
    public void SetLine(Vector3 Pos,Vector3 targetLength,float time)
    {
        //照準があうまでブレさせる///////////////////////////////////////////////////////////////////////////////////////////
        float randX = (Random.Range(-time * randPow, time * randPow));
        float randY = (Random.Range(-time * randPow, time * randPow));
        float randZ = (Random.Range(-time * randPow, time * randPow));
        line.SetPosition(0, new Vector3( targetLength.x+randX , targetLength.y+randY,targetLength.z + randZ));
        pos.position = Pos;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        if(Physics.Raycast(pos.position,Vector3.Normalize(targetLength),out hit, Vector3.Magnitude(targetLength)))  //レイキャストでプレイヤーと砲身の間に障害物があるかを確認////////////////
        {
            if (!hit.collider.CompareTag("Player")) //プレイヤーに一番初めにあたったら//////
            {
                isShade = true;
            }/////////////////////////////////////////////////////////////////////////////////////
            else 
            {
                isShade = false;
            }
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    //予測線削除
    public void Death()
    {
        Destroy(this.gameObject);   //オブジェクト削除
    }
    //色を赤に変更
    public void SetRed()
    {
        //赤にする
        line.startColor = Color.red;
        line.endColor = Color.red;
        ///////////
    }
    //色を赤と黄で点滅
    public void SetWarning()
    {
        if (TimeCountScript.TimeCounter(ref timeBuff))  //時間で点滅させる///////////
        {
            if (red)
            {
                line.startColor= Color.yellow;
                line.endColor = Color.yellow;
                SetTime();
                red = false;
            }
            else
            {
                line.startColor = Color.red;
                line.endColor = Color.red;
                SetTime();
                red = true;
            }
        }////////////////////////////////////////////////////////////////////////////////////
    }
    //色を透明にする
    public void SetVoid()
    {
        //透明にさせる
        line.startColor = new Color(0, 0, 0, 0);
        line.endColor = new Color(0, 0, 0, 0);    
        ///////////////
    }
    //時間を初期化
    private void SetTime()
    {
        TimeCountScript.SetTime(ref timeBuff, colorChangeTime);
    }
    //プレイヤーが撃てるかどうかを取得
    public bool GetShade()
    {
        return isShade;
    }
    // Start is called before the first frame update
    void Start()
    {
        pos=GetComponent<Transform>();
        line = GetComponent<LineRenderer>();

        SetTime();
        red = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
