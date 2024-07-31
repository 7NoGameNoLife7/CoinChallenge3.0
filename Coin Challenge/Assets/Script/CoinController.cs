using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int coinValue = 1;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("coin" + other.gameObject.tag);
        if (!other.isTrigger)return;
        if (other.gameObject.tag == "Player")
        {
            InfosPlayer.Instance.Addscore(coinValue);
        }
        Destroy(this.gameObject);
              
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
