using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;
    

    void Start()
    {
        
        roadSpawner = GetComponent<RoadSpawner>();
    }

    
    void Update()
    {
        
    }
    
    public void SpawnTriggerEntered()
    {
        
        roadSpawner.MoveRoad();
        
    }
   
    
}
