using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundSpawner : MonoBehaviour
{
    
    [SerializeField] Terrain groundTile;

    
    Vector3 nextSpawnPoint;
   
    
    void Start()
    { 
        
        SpawnTile();
        
        
    }
    

    public void SpawnTile()
    {
        Terrain temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

    }
   
}
