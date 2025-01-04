using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//��𗎂Ƃ��I�u�W�F�N�g�Ǘ�
public class RockFallScript : MonoBehaviour
{
    [SerializeField] private GameObject rock;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Vector2 spawnWidth;
    [SerializeField] private float breakArea;
    [SerializeField] private float fallSpeed;
    [Tooltip("x:�ŏ��l  y:�ő�l")] [SerializeField] private Vector2 rockSize;

    Transform tf;

    private int intervalBuff;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    //�Ǘ�
    private void FallRockController()
    {
        spawnPos = tf.position; //���ݒn����
        if (TimeCountScript.TimeCounter(ref intervalBuff)) 
        {
            SpawnRock();    //�␶��
        }

    }
    //�␶��
    private void SpawnRock()
    {
        //��������͈͂ƃT�C�Y�Z�b�g
        float randX = Random.Range(0, spawnWidth.x);
        float randY = Random.Range(0, spawnWidth.y);
        randX -= spawnWidth.x / 2;
        randY -= spawnWidth.y / 2;
        float randScale=Random.Range(rockSize.x, rockSize.y);
        //////////////////////////////

        GameObject _=Instantiate(rock); //�␶��
        _.transform.position = new Vector3(spawnPos.x + randX, spawnPos.y, spawnPos.z + randY); //���W���
        _.transform.localScale = new Vector3(_.transform.localScale.x * randScale, _.transform.localScale.y * randScale, _.transform.localScale.z * randScale); //�T�C�Y���
        RoclScript rs=_.GetComponent<RoclScript>(); //�R���|�[�l���g�擾
        rs.SetBreakArea(breakArea); //�j����W���
        rs.SetFallSpeed(fallSpeed); //���x���

        TimeCountScript.SetTime(ref intervalBuff, spawnInterval);
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
