using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インゲーム開始時のカメラ演出時の演出
public class MovieFade : MonoBehaviour
{
    [SerializeField] private GameObject upside;
    [SerializeField] private GameObject downside;
    [SerializeField] private float parfectShadeRot;
    [SerializeField] private float movieShadeRot;
    [SerializeField] private float openlyRot;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;
    private Vector3 rotBuff;
    private bool isShade;

    private  Transform upTf;
    private Transform downTf;

    //このスクリプトを動かす関数
    public void MovieFadeController()
    {

        if(shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else if(shadeLevel == 2)
        {
            ParfectSgade();
        }
        else{
            rotBuff = Vector3.zero;
        }
        SetRot();
    }
    //完全に画面を黒帯で囲う
    private void ParfectSgade()
    {
        if(rotBuff.x < parfectShadeRot)
        {
            isShade = false;
            rotBuff.x += moveSpeed;
            if(rotBuff.x >= parfectShadeRot)
            {
                rotBuff.x = parfectShadeRot;
                isShade = true;
            }
        }
    }
    //上下に帯を表示する
    private void MovieShade()
    {
        if (rotBuff.x < movieShadeRot)
        {
            isShade = false;
            rotBuff.x += moveSpeed;
            if (rotBuff.x >= movieShadeRot)
            {
                rotBuff.x = movieShadeRot;
            }
        }
        else if (rotBuff.x > movieShadeRot)
        {
            isShade = false;
            rotBuff.x -= moveSpeed;
            if (rotBuff.x <= movieShadeRot)
            {
                rotBuff.x = movieShadeRot;
            }
        }
    }
    //完全に帯を非表示にする
    private void Openly()
    {
        if (rotBuff.x < parfectShadeRot)
        {
            rotBuff.x += moveSpeed;
            if (rotBuff.x >= parfectShadeRot)
            {
                rotBuff.x = parfectShadeRot;
            }
        }
    }
    
    //帯のレベルを取得
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    //隠れているかどうかを取得
    public bool GetShade()
    {
        return isShade;
    }
    //値をトランスフォームに代入
    private void SetRot()
    {
        upTf.localEulerAngles = rotBuff;
        downTf.localEulerAngles = -rotBuff;
    }
    // Start is called before the first frame update
    void Start()
    {
        upTf = upside.GetComponent<Transform>();
        downTf = downside.GetComponent<Transform>();
        shadeLevel = 1;
        rotBuff = upTf.localEulerAngles;
        isShade = false;

        if (upside)
        {
            moveSpeed *= 1;
        }
        else
        {
            moveSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovieFadeController();
    }
}
