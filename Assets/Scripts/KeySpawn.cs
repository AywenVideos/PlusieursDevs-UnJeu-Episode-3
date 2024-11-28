using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] keySpawnPoint;
    [SerializeField] private GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        int index =Random.Range(0,keySpawnPoint.Length);
        Instantiate(key,keySpawnPoint[index].transform.position,Quaternion.identity,keySpawnPoint[index].transform);
    }

}
