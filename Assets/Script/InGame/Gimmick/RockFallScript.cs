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


    //管理
    public void FallRockController()
    {
        if (TimeCountScript.TimeCounter(ref intervalBuff)) 
        {
            SpawnRock();    //時間で岩生成
        }

    }

    //インターバル時間取得
    public bool GetInterval()
    {
        return TimeCountScript.TimeCounter(ref intervalBuff);
    }
    //インターバルセット
    public void SetTime()
    {
        TimeCountScript.SetTime(ref intervalBuff, spawnInterval);
    }
    //岩生成
    public void SpawnRock()
    {
        spawnPos = tf.position; //現在地を代入

        //生成する範囲とサイズセット
        float randX = Random.Range(0, spawnWidth.x);                            //X座標代入
        float randY = Random.Range(0, spawnWidth.y);                            //Y座標代入
        randX -= spawnWidth.x / 2;                                                         //ポジションを中心にする
        randY -= spawnWidth.y / 2;                                                         //ポジションを中心にする
        float randScale=Random.Range(rockSize.x, rockSize.y);

        GameObject _=Instantiate(rock);                                                                                                                                                                             //岩生成
        _.transform.position = new Vector3(spawnPos.x + randX, spawnPos.y, spawnPos.z + randY);                                                                                      //座標代入
        _.transform.localScale = new Vector3(_.transform.localScale.x * randScale, _.transform.localScale.y * randScale, _.transform.localScale.z * randScale); //サイズ代入
        _.transform.SetParent(tf.transform, true);                                                                                                                                                               //岩を自身の子に
        RockScript rs=_.GetComponent<RockScript>();                                                                                                                                                       //コンポーネント取得
        rs.StartRock();                                                                                                                                                                                                      //岩初期化
        rockList.Add(rs);                                                                                                                                                                                                   //生成した岩をリストに格納

    }

    //岩をリストで管理
    public void RockController(in bool isPose)
    {
        for (int i = 0; i < rockList.Count;)
        {
            if (rockList[i].GetPos()<breakArea)  
            {
                //岩が破壊位置についたら
                rockList[i].BreakRock();         //オブジェクトを削除
                rockList.Remove(rockList[i]); //リストから削除

            }
            else
            {
                rockList[i].Fall(fallSpeed,isPose); //岩を落とす
                i++;
            }
        }
    }
    //初期化
    public void StartRockFall()
    {
        tf = GetComponent<Transform>();
        spawnPos = tf.position;
        SetTime();
    }    

}
