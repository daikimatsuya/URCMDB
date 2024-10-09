using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScript : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private int maxStage;
    [SerializeField] private string[] stage;
    [SerializeField] private float stageSelectCoolTime;
    [SerializeField] private int coolTimeBuff;

    private bool rotateEnd;
    private int stageChangeCount;

    private void SelectController()
    {
        StageSelect();
    }
    private void StageSelect()
    {
        if (rotateEnd)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff == 0))
            {
                stageChangeCount--;
                if (stageCount > 0)
                {
                    stageCount--;
                }
                else
                {
                    stageCount = maxStage;
                }
                coolTimeBuff = (int)(stageSelectCoolTime * 60);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff == 0))
            {
                stageChangeCount++;
                if (stageCount < maxStage)
                {
                    stageCount++;
                }
                else
                {
                    stageCount = 0;
                }
                coolTimeBuff = (int)(stageSelectCoolTime * 60);
            }
            coolTimeBuff--;
        }
    }
    public void SetRotateEnd(bool flag)
    {
        rotateEnd = flag;
    }
    public bool GetRotateEnd() 
    {
        return rotateEnd;
    }
    public Vector2 GetStageCount()
    {
        return new Vector2(stageChangeCount,maxStage);
    }
    // Start is called before the first frame update
    void Start()
    {
        stageCount = 1;
        stageChangeCount = 1;
        rotateEnd = true;
    }

    // Update is called once per frame
    void Update() 
    {
        SelectController();
    }
}
