using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    [SerializeField] Canvas[] Canvases;
    [SerializeField] Image staminaBar;

    public Controller controller;

    public int CanvasIndex;

    bool clickButton = false;
    void Start()
    {
        CanvasIndex = 0;

        if ((SceneManager.GetActiveScene().buildIndex) != 0)
        {
            CanvasIndex = 1;
        }
    }

    void Update()
    {
        if (CanvasIndex == 0)
        {
            Time.timeScale = 0;
        }
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Moved)
                {
                    Time.timeScale = 1;
                    CloseTabToPlayCanvas();
                    
                }
            }

        }
        CanvasCounter();
        StaminaBar();
    }
    void CanvasCounter()
    {
        foreach (Canvas canvas in Canvases)
        {
            canvas.gameObject.SetActive(false);
        }
        Canvases[CanvasIndex].gameObject.SetActive(true);
    }
    public void CloseTabToPlayCanvas()
    {
        CanvasIndex = 1;
    }
   
    public void StaminaBar()
    {
        staminaBar.fillAmount = controller.stamina/100;
    }

    
    

}
