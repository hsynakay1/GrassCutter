using Facebook.Unity;
using UnityEngine;

public class FacebookInitializer : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, onHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize the Facebook SDK");
        }
    }
    private void onHideUnity(bool IsGameShown)
    {
        if (!IsGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
