using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEngine;

//インゲーム開始時のカメラ演出時の演出
public class MovieFade : MonoBehaviour
{
    [SerializeField] private bool upside;
    [SerializeField] private float parfectShadePos;
    [SerializeField] private float movieShadePos;
    [SerializeField] private float openlyPos;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;

    //このスクリプトを動かす関数
    private void MovieFadeController()
    {
        if(shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else
        {
            ParfectSgade();
        }
    }
    //完全に画面を黒帯で囲う
    private void ParfectSgade()
    {

    }
    //上下に帯を表示する
    private void MovieShade()
    {

    }
    //完全に帯を非表示にする
    private void Openly()
    {

    }
    //帯のレベルを取得
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(upside)
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
