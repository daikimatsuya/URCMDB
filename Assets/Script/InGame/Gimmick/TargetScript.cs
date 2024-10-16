using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//îjâÛñ⁄ïWä«óù
public class TargetScript : MonoBehaviour
{
    [SerializeField] private int hp;

    private GameManagerScript gm;

    private void TargetController()
    {
        IsBreak();
    }
    private void IsBreak()
    {
        if(hp <= 0)
        {
            gm.SetClearFlag();
            Destroy(this.gameObject);
        }
    }
    public int GetHp()
    {
        return hp;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript ps=collision.gameObject.GetComponent<PlayerScript>();
            hp-=(int)(ps.GetPlayerSpeedBuffFloat()/10);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetController();
    }
}
