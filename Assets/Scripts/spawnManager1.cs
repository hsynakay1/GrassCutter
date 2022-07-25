using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager1 : MonoBehaviour
{
    public Terrain terrain;

    private void Start()
    {
        TerrainData terrainData = new TerrainData();
    }

    private void OnTriggerEnter(Collider other)
    {
        terrain.transform.position += new Vector3(0, 0, 70);
        TerrainData terrainData = new TerrainData();
    }
}
