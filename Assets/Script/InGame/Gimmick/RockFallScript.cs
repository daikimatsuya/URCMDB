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
    private List<RockScript> rockList = new List<RockScript>();


    //�Ǘ�
    public void FallRockController()
    {
        if (TimeCountScript.TimeCounter(ref intervalBuff)) 
        {
            SpawnRock();    //�␶��
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
    //�␶��
    public void SpawnRock()
    {
        spawnPos = tf.position; //���ݒn����

        //��������͈͂ƃT�C�Y�Z�b�g
        float randX = Random.Range(0, spawnWidth.x);
        float randY = Random.Range(0, spawnWidth.y);
        randX -= spawnWidth.x / 2;
        randY -= spawnWidth.y / 2;
        float randScale=Random.Range(rockSize.x, rockSize.y);
        //////////////////////////////

        GameObject _=Instantiate(rock);                                                                                                                                                                             //�␶��
        _.transform.position = new Vector3(spawnPos.x + randX, spawnPos.y, spawnPos.z + randY);                                                                                      //���W���
        _.transform.localScale = new Vector3(_.transform.localScale.x * randScale, _.transform.localScale.y * randScale, _.transform.localScale.z * randScale); //�T�C�Y���
        _.transform.SetParent(tf.transform, true);                                                                                                                                                               //������g�̎q��
        RockScript rs=_.GetComponent<RockScript>();                                                                                                                                                       //�R���|�[�l���g�擾
        rs.StartRock();                                                                                                                                                                                                      //�⏉����
        rockList.Add(rs);                                                                                                                                                                                                   //��������������X�g�Ɋi�[

    }

    //������X�g�ŊǗ�
    public void RockController(in bool isPose)
    {
        if (rockList == null)    //���X�g�ɃI�u�W�F�N�g�������ĂȂ������烊�^�[��//////
        {
            return;
        }//////////////////////////////////////////////////////////////////////////////////


        for (int i = 0; i < rockList.Count;)
        {
            
            if (rockList[i].GetPos()<breakArea)  //�j��t���O���I���ɂȂ��Ă�����
            {
                rockList[i].BreakRock();         //�I�u�W�F�N�g���폜
                rockList.Remove(rockList[i]); //���X�g����폜

            }/////////////////////////////////////////////////////////////////////
            else
            {
                rockList[i].Fall(fallSpeed,isPose);
                i++;
            }
        }
    }
    //������
    public void StartRockFall()
    {
        tf = GetComponent<Transform>();
        spawnPos = tf.position;
        SetTime();
    }    

}
