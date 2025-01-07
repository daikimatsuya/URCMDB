using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�X�e�[�W�̐�����\������
public class StageVersionScript : MonoBehaviour
{
    [SerializeField] private GameObject[] stage;

    private StageSelectScript sss;

    //�X�e�[�W�T�v�\��
    private void ViewVersion()
    {
       
        if(sss.GetStageCount() == 0)    //�X�e�[�W�P�̎�/////////////////////////////////////////////////
        {
            stage[(int)sss.GetStageChangeCount().y].SetActive(false);   //�ŏI�X�e�[�W�̐������\��
            stage[(int)sss.GetStageCount()+1].SetActive(false); //�X�e�[�W�Q�̐������\��
        }/////////////////////////////////////////////////////////////////////////////////////////////////////

        else if (sss.GetStageCount() == sss.GetStageChangeCount().y)    //�ŏI�X�e�[�W�̎�///////////////
        {
            stage[0].SetActive(false);  //�X�e�[�W�P�̐������\��
            stage[(int)sss.GetStageCount() -1].SetActive(false);    //��O�̃X�e�[�W�̐������\��
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////

        else   //����ȊO�̃X�e�[�W�̎�///////////////////////////////////////////////////////////////
        {
            stage[(int)sss.GetStageCount() + 1].SetActive(false);   //����̃X�e�[�W�̐������\��
            stage[(int)sss.GetStageCount() - 1].SetActive(false);   //��O�̃X�e�[�W�̐������\��
        }/////////////////////////////////////////////////////////////////////////////////////////////

        stage[(int)sss.GetStageCount()].SetActive(true);    //���݂̃X�e�[�W�̐�����\��
    }
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        sss=GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        ViewVersion();
    }

    // Update is called once per frame
    void Update()
    {
        ViewVersion();
    }
}
