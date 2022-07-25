using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public GameObject[] levels; 
    private int levelIndex = 0;

    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt(nameof(levelIndex),levelIndex);
    }
    public void levelCounter()
    {
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }
        levels[levelIndex].SetActive(true);
    }
    public void updateLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt(nameof(levelIndex), levelIndex);
    }
}
