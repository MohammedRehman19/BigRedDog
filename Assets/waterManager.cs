using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.tag == "water")
        {
            print("Gameover");
            GameManager.instance.gameoverFtn();
            Instantiate(GameManager.instance.fxwater, GameManager.instance.fxwaterpos.transform);
        }
        else if (other.gameObject.tag == "meat")
        {
            print("meat");
            Destroy(other.gameObject);
            GameManager.instance.staminabar.value += 0.5f;
           
        }
    }
}
