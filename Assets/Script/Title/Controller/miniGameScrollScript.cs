using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトルのミニゲームのステージをスクロールさせる
public class miniGameScrollScript : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;

    //ミニゲームのスクロール管理
    private void ScrollController()
    {
        if (ts.GetMoveFlag())   //ミニゲームが始まったらスクロールする///////
        {
            Scroll();
        }/////////////////////////////////////////////////////////////////////////

        if(ts.GetResetFlag())   //リセットフラグらオンになったら座標リセット///
        {
            ScrollReset();
        }//////////////////////////////////////////////////////////////////////////
    }
    //スクロールさせる
    private void Scroll()
    {
        tf.position = new Vector3(tf.position.x-scrollSpeed, tf.position.y, tf.position.z);
    }
    //スクロール値リセット
    private void ScrollReset()
    {
        tf.position=initialPos;
    }
    //初期値登録
    public void SetInitialPos(Vector3 pos)
    {
        initialPos = pos;
    }

    // Start is called before the first frame update
    public void StartMinigameScroll()
    {
        tf = GetComponent<Transform>();
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();

        initialPos = ts.transform.position;
    }
    void Start()
    {
        StartMinigameScroll();
    }

    // Update is called once per frame
    void Update()
    {
        ScrollController();
    }
}
