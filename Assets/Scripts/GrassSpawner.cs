using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] GameObject grass;
    [SerializeField] Transform ground;

    private void Start()
    {
        
    }
    private void Update()
    {
        Spawner();
    }

    void Spawner()
    {
        Debug.Log("spawn");
        Vector3 temp = new Vector3(90,0,0);
        Instantiate(grass, ground.transform.position,Quaternion.Euler(temp));
    }
}
