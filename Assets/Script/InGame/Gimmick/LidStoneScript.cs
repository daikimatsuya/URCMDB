using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidStoneScript : MonoBehaviour
{
    [SerializeField] GameObject shortcut;
    [SerializeField] GameObject detour;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
