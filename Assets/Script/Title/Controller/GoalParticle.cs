using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�~�j�Q�[���̃S�[�����o�Ǘ�
public class GoalParticle : MonoBehaviour
{
    private TitlegameScript ts;
    ParticleSystem ps;

    //�p�[�e�B�N���̔����Ə���
    private void PariticleController()
    {
        if (ts.GetGoalActionFlag()) //�S�[��������p�[�e�B�N���Đ�////
        {
            ps.Play();
        }/////////////////////////////////////////////////////////////////
        else
        {
            ps.Pause(); //�p�[�e�B�N����~
            ps.Clear(); //�p�[�e�B�N���폜
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        ps=GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        PariticleController();
    }
}
