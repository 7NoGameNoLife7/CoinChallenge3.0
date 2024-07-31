using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
   [SerializeField] List <GameObject> CoinPrefabList;
    void Start()
    {
        SpawnCoin();  
    }

    void SpawnCoin()
    {
        int index = Random.Range( 0, CoinPrefabList.Count - 1);                                             //Gere la selection aleatoir dune piece 

        GameObject prefab = CoinPrefabList[index];
        GameObject instance = Instantiate(prefab);
        instance.transform.position = transform.position;    
    }
   
}
