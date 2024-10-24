using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    //管理
    private void FallRockController()
    {
        spawnPos = tf.position;
        if (intervalBuff < 0) 
        {
            SpawnRock();
        }
        IntervalTimer();
    }
    //岩生成
    private void SpawnRock()
    {
        float randX = Random.Range(0, spawnWidth.x);
        float randY = Random.Range(0, spawnWidth.y);
        randX -= spawnWidth.x / 2;
        randY -= spawnWidth.y / 2;
        float randScale=Random.Range(rockSize.x, rockSize.y);

        GameObject _=Instantiate(rock);
        _.transform.position = new Vector3(spawnPos.x + randX, spawnPos.y, spawnPos.z + randY);
        _.transform.localScale = new Vector3(_.transform.localScale.x * randScale, _.transform.localScale.y * randScale, _.transform.localScale.z * randScale);
        RoclScript rs=_.GetComponent<RoclScript>();
        rs.GetBreakArea(breakArea);
        rs.GetFallSpeed(fallSpeed);

        intervalBuff = (int)(spawnInterval * 60);
    }
    //インターバル管理
    private void IntervalTimer()
    {
        intervalBuff--;
    }
    void Start()
    {
        tf=GetComponent<Transform>();
        spawnPos = tf.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        FallRockController();
    }
}
