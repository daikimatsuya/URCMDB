using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インゲーム開始時のカメラ演出時の演出
public class MovieFade : MonoBehaviour
{
    [SerializeField] private GameObject upside;
    [SerializeField] private GameObject downside;
    [SerializeField] private float parfectShadePos;
    [SerializeField] private float movieShadePos;
    [SerializeField] private float openlyPos;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;
    private Vector3 posBuff;
    private bool isShade;
    private bool isEffectEnd;

    private  Transform upTf;
    private Transform downTf;

    //このスクリプトを動かす関数
    public void MovieFadeController()
    {

        isEffectEnd = false;

        if (shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else if(shadeLevel == 2)
        {
            ParfectShade();
        }
        else{
            isEffectEnd = true;
            Nothing();
        }
        SetPos();
    }
    //完全に画面を黒帯で囲う
    private void ParfectShade()
    {
        if(posBuff.y > parfectShadePos)
        {
            isShade = false;
            posBuff.y -= moveSpeed;
            if(posBuff.y <= parfectShadePos)
            {
                posBuff.y = parfectShadePos;
                isShade = true;
            }
        }
    }
    //上下に帯を表示する
    private void MovieShade()
    {
        isShade = false;
        if (posBuff.y < movieShadePos)
        {
            posBuff.y += moveSpeed;
            if (posBuff.y >= movieShadePos)
            {
                posBuff.y = movieShadePos;
            }
        }
        else if (posBuff.y > movieShadePos)
        {

            posBuff.y -= moveSpeed;
            if (posBuff.y <= movieShadePos)
            {
                posBuff.y = movieShadePos;
            }
        }
    }
    //完全に帯を非表示にする
    private void Openly()
    {
        isShade = false;
        if (posBuff.y > parfectShadePos)
        {
            posBuff.y -= moveSpeed;
            if (posBuff.y <= parfectShadePos)
            {
                posBuff.y = parfectShadePos;

                isEffectEnd = true;
            }
        }
    }
    //演出なく全開にする
    private void Nothing()
    {
        if (upside)
        {
            posBuff.y = openlyPos;
        }
        else
        {
            posBuff.y = -openlyPos;
        }
        isShade = true;
    }
    
    //帯のレベルを取得
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    //演出が終わったかどうかを取得
    public bool GetEffectEnd()
    {
        return isEffectEnd;
    }
    //完全に隠れているかどうかを取得
    public bool GetIsShade()
    {
        return isShade;
    }
    //値をトランスフォームに代入
    private void SetPos()
    {
        upTf.localPosition = posBuff;
        downTf.localPosition = -posBuff;
    }
    // Start is called before the first frame update
    void Start()
    {
        upTf = upside.GetComponent<Transform>();
        downTf = downside.GetComponent<Transform>();
        shadeLevel = 1;
        posBuff = upTf.localPosition;
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
        //MovieFadeController();
    }
}
