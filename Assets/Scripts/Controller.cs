using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using BadDog;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public NextLevel nextLevel;
    private Animator animator;

    [SerializeField] public float movementSpeed = 10f;
    [SerializeField] public float rotationSpeed = 500;
    [SerializeField] public float stamina = 100;
    [SerializeField] private float wayHalf = 4f;
    [SerializeField] private Material skinMaterial;
    [SerializeField] private GameObject knife;
    [SerializeField] private ParticleSystem particleSmoke;
    [SerializeField] private ParticleSystem cryParticleOne;
    //[SerializeField] private ParticleSystem coinParticle;
    [SerializeField] public float _radius;
    [SerializeField] public Vector3 turnBackPoint = new Vector3(15f, 0, -16);
    [SerializeField] public Vector3 point;
    [SerializeField] public float maxStamina = 100;
    [SerializeField] private Transform instantiateCoinTransform;
    [SerializeField] private GameObject coinForInstantiate;
    //[SerializeField] private Image image;
    

    public float downGradeValue = 10;
    public float upGradeValue = 10;
    public float criticalPoint = 20;
    public float knifeSpeed = 1000;
    public float coin;
    public float artacakCoin = 0.1f;
    public TextMeshProUGUI coinText;
    public int _coin;

    private Touch touch;

    private Vector3 touchDown;
    private Vector3 touchUp;

    private bool stop;
    private bool dragStarted;
    private bool isMoving;
    private BGGrassCutter cutter;
    private bool coinCoroutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        //updateCanvas.SetActive(false);
        particleSmoke.Pause();
        cryParticleOne.Pause();
        //coinParticle.Pause();

        coinCoroutine = false;

        cutter = GetComponent<BGGrassCutter>();
        stamina = 100;

        //coinText.text = coin.ToString();

        coin = PlayerPrefs.GetFloat(nameof(coin), coin);
    }


    void Update()
    {
        Debug.Log(coinCoroutine);
        knife.transform.Rotate(new Vector3(0, Time.deltaTime * knifeSpeed, 0));
        if (stamina > 100)
        {
            stamina = 100;
        }
        coinText.text = ((int)coin).ToString();

        //Debug.Log(isMoving + " y?r?yor ");
        if (Input.touchCount > 0 && stamina > 0 && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dragStarted = true;
                isMoving = true;
                touchDown = touch.position;
                touchUp = touch.position;
                stop = false;

                coinCoroutine = true;

                StartCoroutine(CoinTimer());
                
               
            }
        }
        if (dragStarted == true)
        {

            if (touch.phase == TouchPhase.Moved)
            {
                touchDown = touch.position;

            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchDown = touch.position;
                isMoving = false;
                dragStarted = false;
                animator.SetBool("Move", false);
                coinCoroutine = false;
            }
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);

            if (stop == false)
            {
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            }

        }

        if (transform.position.x <= -wayHalf)
        {
            Vector3 _position = new Vector3(-wayHalf, transform.position.y, transform.position.z);
            transform.position = _position;
        }
        else if (transform.position.x >= wayHalf)
        {
            Vector3 _position = new Vector3(wayHalf, transform.position.y, transform.position.z);
            transform.position = _position;
        }
        if (isMoving == true)
        {
            animator.SetBool("Move", true);
            stamina -= downGradeValue * Time.deltaTime;

            //coin += artacakCoin * Time.deltaTime;
            //_coin =(int)coin;

            PlayerPrefs.SetFloat(nameof(coin), coin);

            coinCoroutine = true;
            

        }
        if (isMoving == false)
        {
            stamina += upGradeValue * Time.deltaTime;
            //coinCoroutine = false;
        }
        if (stamina <= criticalPoint)
        {
            skinMaterial.DOColor(Color.red, criticalPoint);
            particleSmoke.gameObject.SetActive(true);
            particleSmoke.Play();
            particleSmoke.gameObject.transform.DOScale(0.04f, 5);
            cryParticleOne.Play();
            cryParticleOne.gameObject.transform.DOScale(2.5f, 1);


        }
        if (Input.touchCount < 1)
        {
            particleSmoke.gameObject.transform.DOScale(0.0f, 5);
            cryParticleOne.gameObject.transform.DOScale(0.0f, 5);

            //animator.SetBool("Move", false);

            if (stamina <= maxStamina)
                stamina += upGradeValue * Time.deltaTime;

            isMoving = false;
            skinMaterial.DOColor(Color.white, 5);
        }

        if (stamina < 0.5f)
        {

            //stamina = 0;
            staminaController();

        }
        else
        {
            movementSpeed = 10f;
        }
        if (stamina > criticalPoint)
        {
            skinMaterial.color = Color.white;
        }
    
    }

    
    private void staminaController()
    {
        if (Input.touchCount > 0)
        {
            animator.SetBool("Move", true);
            movementSpeed = 3;
            stamina += upGradeValue * Time.deltaTime;
            stop = false;
        }
        else
        {
            animator.SetBool("Move", false);
            movementSpeed = 0;
            stop = true;
        }
        
    }

    Quaternion CalculateRotation()
    {
        Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return temp;
    }
    Vector3 CalculateDirection()
    {
        Vector3 temp = (touchDown - touchUp).normalized;
        temp.z = temp.y;
        temp.y = 0;
        if (temp.z < 0)
        {
            temp.z = 0;
        }
        
        return temp;
    }
    public bool changeLevel = false;
    public void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Line"))
        {
            changeLevel = true;
            this.animator.SetBool("Move", true);
            //this.gameObject.transform.DOMove(point, 2).OnComplete(() => animator.SetBool("Move", false));

        }
    }

    IEnumerator CoinTimer()
    {

       yield return new WaitForSeconds(3);
        coin += artacakCoin;

        PlayerPrefs.SetFloat(nameof(coin), coin);

        //image.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f).OnComplete(() => image.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
        //coinParticle.Play();
        if (coinCoroutine == true)
        {
            StartCoroutine(CoinTimer());
        }
    }
    
}
