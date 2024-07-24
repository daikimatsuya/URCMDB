using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIScript : MonoBehaviour
{
    [SerializeField] private float retryPos;
    [SerializeField] private float backTitlePos;
    [SerializeField] private Transform cursorPos;
    
    public void MoveRetry()
    {

        cursorPos.localPosition = new Vector3(retryPos, cursorPos.localPosition.y, cursorPos.localPosition.z);
        
    }
    public void MoveBackTitle()
    {

        cursorPos.localPosition = new Vector3(backTitlePos, cursorPos.localPosition.y, cursorPos.localPosition.z);
        
    }
    public float GetPos()
    {
        return cursorPos.localPosition.x;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
