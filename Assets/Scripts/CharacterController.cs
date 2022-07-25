//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using DG.DemiLib;
//using BadDog;

//public class CharacterController : MonoBehaviour
//{
//    private Animator animator;

//    [SerializeField] public float movementSpeed = 10f;
//    [SerializeField] public float rotationSpeed = 500;
//    [SerializeField] public float stamina = 10;
//    [SerializeField] public Material skinMaterial;
//    [SerializeField] public GameObject knife;
//    [SerializeField] public ParticleSystem particleSmoke;
//    [SerializeField] public ParticleSystem cryParticleOne;
//    [SerializeField] public ParticleSystem cryParticleTwo;
//    [SerializeField] public ParticleSystem confetParticle;
//    [SerializeField] public float _radius;
//    [SerializeField] public Vector3 turnBackPoint = new Vector3(15f, 0, -16);

//    public float downGradeValue = 1;
//    public float upGradeValue = 1;
//    public float criticalPoint = 5;
//    public float knifeSpeed = 1000;

//    private Touch touch;

//    private Vector3 touchDown;
//    private Vector3 touchUp;

//    private bool dragStarted;
//    private bool isMoving;
//    private BGGrassCutter cutter;

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        animator = GetComponentInChildren<Animator>();
//        //updateCanvas.SetActive(false);
//        particleSmoke.Pause();
//        cryParticleOne.Pause();
//        cryParticleTwo.Pause();
//        confetParticle.Pause();
//        cutter = GetComponent<BGGrassCutter>();
//    }

    
//    void Update()
//    {
//        if (Input.touchCount > 0 || stamina > 0)
//        {
//            touch = Input.GetTouch(0);
//            if (touch.phase == TouchPhase.Began)
//            {
//                dragStarted = true;
//                isMoving = true;
//                touchDown = touch.position;
//                touchUp = touch.position;
//            }
//        }
//        if (dragStarted == true)
//        {
//            if (touch.phase == TouchPhase.Moved)
//            {
//                touchDown = touch.position;
//            }
//            if (touch.phase == TouchPhase.Ended)
//            {
//                touchDown = touch.position;
//                isMoving = false;
//                dragStarted = false;
//            }
//            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);
//            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
//        }
//        if (isMoving)
//        {
//            animator.SetBool("Move", true);
//            stamina -= downGradeValue * Time.deltaTime;
//            knife.transform.Rotate(new Vector3(0, Time.deltaTime * knifeSpeed, 0));
//            if (stamina <= criticalPoint)
//            {
//                skinMaterial.DOColor(Color.red, criticalPoint);
//                particleSmoke.gameObject.SetActive(true);
//                particleSmoke.Play();
//                particleSmoke.gameObject.transform.DOScale(0.04f, 5);
//                cryParticleOne.Play();
//                cryParticleOne.gameObject.transform.DOScale(2.5f, 1);
//                cryParticleTwo.Play();
//                cryParticleTwo.gameObject.transform.DOScale(2.5f, 1);

//            }
//        }
//        else
//        {
//            particleSmoke.gameObject.transform.DOScale(0.0f, 5);
//            cryParticleOne.gameObject.transform.DOScale(0.0f, 5);
//            cryParticleTwo.gameObject.transform.DOScale(0.0f, 5);
//            animator.SetBool("Move", false);
//            stamina += upGradeValue * Time.deltaTime;
//        }
//        if (stamina < 1)
//        {
//            stamina = 0;
//            TurnBack();  
//            animator.SetBool("Fall", false);
//        }
//        if (stamina > criticalPoint)
//        {
//            skinMaterial.color = Color.white;
//        }

//    }
//    void TurnBack()
//    {
//        this.gameObject.SetActive(false);
//        gameObject.transform.position = new Vector3(15f, 0, -16);
//        this.gameObject.SetActive(true);
       
//    }

//    Quaternion CalculateRotation()
//    {
//        Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
//        return temp;
//    }
//    Vector3 CalculateDirection()
//    {
//        Vector3 temp = (touchDown - touchUp).normalized;
//        temp.z = temp.y;
//        temp.y = 0;
//        return temp;
//    }
//}
