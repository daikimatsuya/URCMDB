using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUIScript : MonoBehaviour
{
    Transform pos;
    LineRenderer line;
    private Transform playerPos;
    [SerializeField] private float rand;
    private void LineUIController()
    {

    }
    public void SetLine(Vector3 Pos,Vector3 targetLength)
    {
 
        line.SetPosition(0, targetLength);
        pos.position = Pos;
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
    private void TestLine()
    {
        Vector3 pos0 = new Vector3(playerPos.position.x + Random.Range(-rand, rand), playerPos.position.y + Random.Range(-rand, rand), playerPos.position.z + Random.Range(-rand, rand));

        line.SetPosition(0, pos0);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerPos=GameObject.FindWithTag("Player").GetComponent<Transform>();
        pos=GetComponent<Transform>();
        line = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //TestLine();
    }
}
