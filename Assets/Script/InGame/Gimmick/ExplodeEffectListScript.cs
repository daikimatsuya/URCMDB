using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������o�����X�g�ŊǗ�
public class ExplodeEffectListScript : MonoBehaviour
{
    private List<ExplodeEffectScript> explodeEffectList;
    private bool playerExplodeFlag;
    private Vector3 playerPos;

    //���o�Ǘ�
    public void ExplodeEffectListController(in PlayerControllerScript pcs)
    {
        CreatePlayerExplode(in pcs);    //�v���C���[�̔�������

        //�v���C���[�����݂��Ă��牉�o������
        if (pcs.GetPlayer() != null)
        {
            for (int i = 0; i < explodeEffectList.Count; i++)
            {
                explodeEffectList[i].Break();       //�폜
                explodeEffectList.RemoveAt(i);   //���X�g����폜
            }
            return;
        }

        //���o�𓮂���
        for (int i = 0; i < explodeEffectList.Count;)
        {
            explodeEffectList[i].SizeUp();      //�g��
            explodeEffectList[i].Rotation();    //��]
            explodeEffectList[i].Dissolve();    //�f�B�]���u������
            explodeEffectList[i].Edge();        //�[���f�B�]���u������

            //���ԂŔj�󂷂�
            if (explodeEffectList[i].CountDown())
            {
                explodeEffectList[i].Break();       //�폜
                explodeEffectList.RemoveAt(i);   //���X�g����폜
            }
            else
            {
                i++;
            }
        }
    }

    //�v���C���[�̔����G�t�F�N�g����
    private void CreatePlayerExplode(in PlayerControllerScript pcs)
    {
        //�v���C���[���������琶���t���O���I���ɂ��Ĉʒu��ۑ�
        if (pcs.GetPlayer())
        {
            playerExplodeFlag = true;
            playerPos=pcs.GetPlayer().GetTransform().position;
        }

        //�����t���O���I�t�ɂ��ăv���C���[�̈ʒu�ɔ����𐶐�
        else if(playerExplodeFlag) 
        {
            playerExplodeFlag=false;
            ExplodeEffectScript _ = pcs.CreateExplodeEffect(playerPos).GetComponent<ExplodeEffectScript>();
            _.StartExplodeEffect();
            explodeEffectList.Add(_);
        }

    }
    
    //����������
    public void StartExplodeEffectList()
    {
        explodeEffectList = new List<ExplodeEffectScript>(FindObjectsOfType<ExplodeEffectScript>());
    }
}
