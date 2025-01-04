using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�u�[�X�g���̃G�t�F�N�g�Ǘ�
public class BoostEffectScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private int lifeTimeBuff;

    private bool deleteFlag=false;

    //�t���O�Ǘ�
    public void CountTime()
    {
        if(TimeCountScript.TimeCounter(ref lifeTimeBuff))
        {
            deleteFlag = true;
        }
    }
    //�������ԃZ�b�g
    public void SetTime()
    {
        TimeCountScript.SetTime(ref lifeTimeBuff, lifeTime);
    }
    //�f���[�g�t���O��Ԃ�
    public bool IsDelete()
    {
        return deleteFlag;
    }
    //�f���[�g
    public void Break()
    {
        Destroy(this.gameObject);
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
