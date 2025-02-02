using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//岩を落とすオブジェクト管理
public class RockFallScript : MonoBehaviour
{
    [SerializeField] private GameObject rock;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Vector2 spawnWidth;
    [SerializeField] private float breakArea;
    [SerializeField] private float fallSpeed;
    [Tooltip("x:最小値  y:最大値")] [SerializeField] private Vector2 rockSize;


    Transform tf;

    private int intervalBuff;
    private Vector3 spawnPos;
    private List<RockScript> rockList = new List<RockScript>();

    // Start is called before the first frame update
    //管理
    public void FallRockController()
    {

        if (TimeCountScript.TimeCounter(ref intervalBuff)) 
        {
            SpawnRock();    //岩生成
        }

    }
    public bool GetInterval()
    {
        return TimeCountScript.TimeCounter(ref intervalBuff);
    }
    public void SetTime()
    {
        TimeCountScript.SetTime(ref intervalBuff, spawnInterval);
    }
    //岩生成
    public void SpawnRock()
    {
        spawnPos = tf.position; //現在地を代入

        //生成する範囲とサイズセット
        float randX = Random.Range(0, spawnWidth.x);
        float randY = Random.Range(0, spawnWidth.y);
        randX -= spawnWidth.x / 2;
        randY -= spawnWidth.y / 2;
        float randScale=Random.Range(rockSize.x, rockSize.y);
        //////////////////////////////

        GameObject _=Instantiate(rock); //岩生成
        _.transform.position = new Vector3(spawnPos.x + randX, spawnPos.y, spawnPos.z + randY); //座標代入
        _.transform.localScale = new Vector3(_.transform.localScale.x * randScale, _.transform.localScale.y * randScale, _.transform.localScale.z * randScale); //サイズ代入
        _.transform.SetParent(tf.transform, true);
        RockScript rs=_.GetComponent<RockScript>(); //コンポーネント取得
        rs.StartRock();
        rockList.Add(rs);   //生成した岩をリストに格納

    }

    public void ListController(in bool isPose)
    {
        if (rockList == null)    //リストにオブジェクトが入ってなかったらリターン//////
        {
            return;
        }//////////////////////////////////////////////////////////////////////////////////


        for (int i = 0; i < rockList.Count;)
        {
            
            if (rockList[i].GetPos()<breakArea)  //破壊フラグがオンになっていたら
            {
                rockList[i].BreakRock(); //オブジェクトを削除
                rockList.Remove(rockList[i]); //リストから削除
                return;
            }/////////////////////////////////////////////////////////////////////
            else
            {
                rockList[i].Fall(fallSpeed,isPose);
                i++;
            }
        }
    }
    public void StartRockFall()
    {
        tf = GetComponent<Transform>();
        spawnPos = tf.position;
        SetTime();
    }    

}
