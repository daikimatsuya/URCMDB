using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの背後の火管理用
public class Spark : MonoBehaviour
{
    [SerializeField] private float time;
    private void SparkController()
    {
        Delete();
    }
    private void Delete()
    {        
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
        time--;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SparkController();
    }
}
