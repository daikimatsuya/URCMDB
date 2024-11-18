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
    #region 値受け渡し
    public float GetPos()
    {
        return cursorPos.localPosition.x;
    }
    public void SetTargetHp()
    {
        targetHp=targetScript.GetHp();
    }
    #endregion
    //ターゲットのHPを表示
    public void TargetHpUI()
    {
        if (target != null)
        {
            SetTargetHp();
            targetHpTex.text = "Hp"+targetHp;
        }
        else
        {
            targetHpTex.text = "0";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Target");
        targetScript = target.GetComponent<TargetScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
