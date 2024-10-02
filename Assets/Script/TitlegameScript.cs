using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlegameScript : MonoBehaviour
{
    miniPlayerScript mps;
    private void TitleGameController()
    {

    }
    private void ResetMinigame()
    {
        mps.ResetPlayer();  
    }
    // Start is called before the first frame update
    void Start()
    {
       mps=GameObject.FindWithTag("miniPlayer").GetComponent<miniPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
