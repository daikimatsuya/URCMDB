using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallScript : MonoBehaviour
{
    [SerializeField] private GameObject rock;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Vector2 spawnWidth;
    [SerializeField] private float breakArea;
    [SerializeField] private float fallSpeed;
    [SerializeField] private Vector2 rockSize;

    Transform tf;

    private int intervalBuff;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    private void FallRockController()
    {
        spawnPos = tf.position;//��ŏ���
        if (intervalBuff < 0) 
        {
            SpawnRock();
        }
        IntervalTimer();
    }
    private void SpawnRock()
    {
        float randX = Random.Range(0, spawnWidth.x);
        float randY = Random.Range(0, spawnWidth.y);
        randX -= spawnWidth.x / 2;
        randY -= spawnWidth.y / 2;

        GameObject _=Instantiate(rock);
        _.transform.position = new Vector3(spawnPos.x + randX, spawnPos.y, spawnPos.z + randY);
        RoclScript rs=_.GetComponent<RoclScript>();
        rs.GetBreakArea(breakArea);
        rs.GetFallSpeed(fallSpeed);

        intervalBuff = (int)(spawnInterval * 60);
    }
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
