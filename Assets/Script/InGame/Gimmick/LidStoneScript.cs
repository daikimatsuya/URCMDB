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
            shortcut.SetActive(true);       //ショートカットを表示
            detour.SetActive(false);         //回り道非表示
            Destroy(this.gameObject);    //削除
        }
    }

    //初期化
    public void StartLibStone()
    {
        shortcut.SetActive(false);
    }
}
