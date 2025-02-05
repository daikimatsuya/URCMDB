using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�C�g���V�[������C���Q�[���։��o�̉~�����k���Ă������o�Ǘ�
public class CircleFadeObjectScript : MonoBehaviour
{
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float initialPosY;


    Transform tf;
    private SceneChangeAnimationScript scas;

    //���o�t���O
    private void CircleFadeController()
    {
        if(scas.GetIsFadeStartFlag())   //�t�F�[�h�t���O���I���ɂȂ��Ă���ƃt�F�[�h�J�n////
        {
            Fade();
        }/////////////////////////////////////////////////////////////////////////////////////////
    }
    private void Fade()
    {
        tf.localScale = new Vector3(2, 2,tf.localScale.z + increaseSpeed);  //�T�C�Y���k��
    }
    // Start is called before the first frame update
    public void StartCircleFadeObject()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();

        tf = GetComponent<Transform>();
        tf.localScale = Vector3.zero;
        tf.localPosition = new Vector3(0, initialPosY, 0);
    }
    void Start()
    {
        StartCircleFadeObject();
    }

    // Update is called once per frame
    void Update()
    {
        CircleFadeController();
    }
}
