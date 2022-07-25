using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class NextLevel : MonoBehaviour
{
    public Controller controller;
    [SerializeField] public GameObject character;
    [SerializeField] public int sceneIndex;
    Animator animator;
    

    private void Start()
    {
        PlayerPrefs.SetInt("buildImdex", SceneManager.GetActiveScene().buildIndex);
    }
    private void Update()
    {
        if (controller.GetComponent<Controller>().changeLevel == true)
        {
            StartCoroutine(ChangeLevel());
            controller.GetComponent<Controller>().changeLevel = false;
            //sceneNumber++;
        } 
        
    }
    public IEnumerator ChangeLevel()
    {
        
        yield return new WaitForSecondsRealtime(1f);
        ChangeScane();
    }
    public void ChangeScane()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.GetInt("buildIndex", SceneManager.GetActiveScene().buildIndex);
       
    }
   
}
