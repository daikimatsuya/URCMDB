using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//ゲームオーバー時のUI管理
public class GameOverUIScript : MonoBehaviour
{
    [SerializeField] private float retryPos;
    [SerializeField] private float backTitlePos;
    [SerializeField] private Transform cursorPos;
    [SerializeField] private TextMeshProUGUI targetHpTex;

    private GameObject target;
    private TargetScript targetScript;
    private int targetHp;

    //リトライを選択したときにカーソルを移動
    public void MoveRetry()
    {
        cursorPos.localPosition = new Vector3(retryPos, cursorPos.localPosition.y, cursorPos.localPosition.z);  
    }
    //タイトルに戻るを洗濯したときにカーソルを移動
    public void MoveBackTitle()
    {
        cursorPos.localPosition = new Vector3(backTitlePos, cursorPos.localPosition.y, cursorPos.localPosition.z);  
    }
    //カーソル位置をリセット
    public void ResetPos()
    {
        cursorPos.localPosition = new Vector3(0, cursorPos.localPosition.y, cursorPos.localPosition.z);
    }
    #region 値受け渡し
    public float GetPos()
    {
        return cursorPos.localPosition.x;
    }
    public void SetTargetHp()
    {
        if (targetScript != null)
        {
            targetHp = (int)targetScript.GetHp();
        }
    }

    #endregion
    //ターゲットのHPを表示
    public void TargetHpUI()
    {
        if (target != null)
        {
            SetTargetHp();  //ターゲットの体力取得

            //ターゲットの体力表示///////////////////////
            if (targetHp > 0)  
            {
                targetHpTex.text = "Hp" + targetHp;
            }
            else
            {
                targetHpTex.text = "Clear";
            }
            //////////////////////////////////////////////
        }
        else   //クリア表示//////////////
        {
            targetHpTex.text = "Clear";
        }/////////////////////////////////

    }
    
    //初期化
    public void StartGameOverUI()
    {
        target = GameObject.FindWithTag("Target");
        targetScript = target.GetComponent<TargetScript>();
    }

}
