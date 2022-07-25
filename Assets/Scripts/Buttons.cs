using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BadDog;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    public BGGrassCutter cutter;
    public Controller controller;
    public GameObject knife;
    public TMPro.TextMeshProUGUI coinStrength;
    public TMPro.TextMeshProUGUI coinIncome;
    public TMPro.TextMeshProUGUI coinCutterSize;
    public Button IncomeButton;
    public Button StaminaButton;
    public Button SizeButton;
    public int coinForSrength =1;
    public int coinForIncome = 1;
    public int coinForCutterSize =1;
    public float radiusValue = 0.23f;
    public float cutturSizeValue = 0.21f;
    public float stamineUpgradeValue = 1;
    public GameObject PlayerEffect;


    [SerializeField] public ParticleSystem coinSmashParticle;
    [SerializeField] public ParticleSystem cutterSizeParticle;

    private float z = 0.5f;
    private float y = 1;
    private float x =1;
    private Touch touch;

   

    public float cutterSize;


    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        coinForCutterSize = PlayerPrefs.GetInt(nameof(coinForCutterSize), coinForCutterSize);
        coinForSrength = PlayerPrefs.GetInt(nameof(coinForSrength), coinForSrength);
        coinForIncome = PlayerPrefs.GetInt(nameof(coinForSrength), coinForSrength);
        cutter.radius= PlayerPrefs.GetFloat("radius", cutter.radius);
        z =  PlayerPrefs.GetFloat(nameof(z),z);
        knife.transform.localScale = new Vector3(x, y, z);

        coinSmashParticle.gameObject.SetActive(false);
        coinSmashParticle.Pause();

        cutterSizeParticle.gameObject.SetActive(false);
        cutterSizeParticle.Pause();
    }

    private void Update()
    {
       controller.coinText.text = controller._coin.ToString();
        
        coinStrength.text = coinForSrength.ToString();
        coinIncome.text = coinForIncome.ToString();
        coinCutterSize.text = coinForCutterSize.ToString();

        if (controller.coin < coinForCutterSize)
        {
            SizeButton.image.color = Color.grey;
        }
        else
        {
            SizeButton.image.DOColor(Color.white, 0.1f);
        }

        if (controller.coin < coinForIncome)
        {
            IncomeButton.image.color = Color.grey;
        }
        else
        {
            IncomeButton.image.DOColor(Color.white, 0.1f);
        }

        if (controller.coin < coinForSrength)
        {
            StaminaButton.image.color = Color.grey;
        }
        else
        {
            StaminaButton.image.DOColor(Color.white, 0.1f);
        }
    }
    public void CutterSize()
    {
        if (controller.coin >= coinForCutterSize)
        {
            controller.coin -= coinForCutterSize;
            knife.transform.localScale = new Vector3(x,y,z);
            cutterSize =cutter.radius += 0.30f;
            z += 0.15f;
            knife.transform.localScale += new Vector3(0, 0, 0.1f);
            cutterSize =cutter.radius += 0.23f;
            knife.transform.localScale = new Vector3(x,y,z);
            cutterSize =cutter.radius += radiusValue;
            z += cutturSizeValue;
            coinForCutterSize = PlayerPrefs.GetInt(nameof(coinForCutterSize), coinForCutterSize);
            coinForCutterSize +=5;

            controller.artacakCoin += 0.1f;
            PlayerPrefs.SetInt(nameof(coinForCutterSize), coinForCutterSize);
            PlayerPrefs.SetFloat("radius", cutter.radius);
            PlayerPrefs.SetFloat(nameof(z), z);
            
            coinCutterSize.text = coinForCutterSize.ToString();

            cutterSizeParticle.gameObject.SetActive(true);
            cutterSizeParticle.Play();

            DOTween.Kill(2);

            SizeButton.transform.localScale = Vector3.one * 1.2f;

            SizeButton.transform.DOPunchScale(new Vector3(-0.2f, -0.2f, -0.2f), 1, 1, 1).SetId(2);
        }
        else
        {
            controller.coinText.color = Color.red;
            controller.coinText.DOColor(Color.white, 3f);
            coinCutterSize.color = Color.red;
            coinCutterSize.DOColor(Color.white, 3f);
  
        }
        controller.coinText.text = controller._coin.ToString();

    }
    public void Income()
    {
        if (controller.coin >= coinForIncome)
        {
            controller.coin -= coinForIncome;
            controller.artacakCoin += 1;
            coinForIncome+=5;
            PlayerPrefs.SetFloat(nameof(coinForIncome), coinForIncome);
            coinIncome.text = coinForIncome.ToString();

            coinSmashParticle.gameObject.SetActive(true);
            coinSmashParticle.Play();

            DOTween.Kill(1);

            IncomeButton.transform.localScale = Vector3.one * 1.2f;

            IncomeButton.transform.DOPunchScale(new Vector3(-0.2f, -0.2f, -0.2f), 1, 1, 1).SetId(1);

        }
        else
        {
            controller.coinText.color = Color.red;
            controller.coinText.DOColor(Color.white, 3f);
            coinIncome.color = Color.red;
            coinIncome.DOColor(Color.white, 3f);
        }
        controller.coinText.text = controller._coin.ToString();
    }
    public void Strength()
    {
        if (controller.coin >= coinForSrength)
        {
            controller.coin -= coinForSrength;
            coinForSrength+=5;
            PlayerPrefs.SetInt(nameof(coinForSrength), coinForSrength);
            controller.stamina += stamineUpgradeValue;
            stamineUpgradeValue++;
            coinStrength.text = coinForSrength.ToString();

            PlayerEffect.gameObject.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.25f).OnComplete(() => PlayerEffect.transform.DOScale(new Vector3(0.7f,0.7f,0.7f), 0.25f));

            DOTween.Kill(0);

            StaminaButton.transform.localScale = Vector3.one * 1.2f;

            StaminaButton.transform.DOPunchScale(new Vector3(-0.2f, -0.2f, -0.2f), 1, 1, 1).SetId(0);
        }
        else
        {
            controller.coinText.color = Color.red;
            controller.coinText.DOColor(Color.white, 3f);
            coinStrength.color = Color.red;
            coinStrength.DOColor(Color.white, 3f);
           
            
        }

        controller.coinText.text = controller._coin.ToString();
    }

   
}