using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rレールのショートカット開通させるときのやつ
public class LidStoneScript : MonoBehaviour
{
    [SerializeField] GameObject shortcut;
    [SerializeField] GameObject detour;

    //プレイヤーがぶつかってきたらショートカット開通させて消える
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shortcut.SetActive(true);
            detour.SetActive(false);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        shortcut.SetActive(false);
    }
}
