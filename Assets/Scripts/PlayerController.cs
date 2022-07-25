using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.DemiLib;
using BadDog;
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public BGGrassCutter cutter;

    //[SerializeField] public GameObject updateCanvas;
    [SerializeField] public float walkSpeed = 10f;
    [SerializeField] public Material skinMaterial;
    [SerializeField] public GameObject knife;
    [SerializeField] public ParticleSystem particleSmoke;
    [SerializeField] public ParticleSystem cryParicleOne;
    [SerializeField] public ParticleSystem cryParicleTwo;
    [SerializeField] public ParticleSystem confetiParticle;
    [SerializeField] public float _radius = 2;


    public Vector3 FinisWalk = new Vector3(5f, 0.5f, 505f);
    public SpawnManager spawnManager;
    public float stamina = 100f;
    public float downGradeValue = 1;
    public float upGradeValue = 1;
    public float criticalPoint = 5;
    public float knifeSpeed = 1000;
    

    bool isWalking;
    bool wait = false;
    public bool fine = true;
    public bool update = false;
    bool rest = true;
    public bool levelComplete = false;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //updateCanvas.SetActive(false);
        particleSmoke.Pause();
        cryParicleOne.Pause();
        cryParicleTwo.Pause();
        confetiParticle.Pause();
        cutter = GetComponent<BGGrassCutter>();
        
    }


    void Update()
    {
        //cutter.radius = _radius;
        PlayerPrefs.SetFloat(nameof(walkSpeed), walkSpeed);
        PlayerPrefs.SetFloat(nameof(stamina), stamina);
        
        if (Input.GetMouseButton(0) && stamina > 0 && fine == true)
        {

            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            isWalking = true;

            animator.SetBool("Move", true);
            

            stamina -= downGradeValue * Time.deltaTime;

            knife.transform.Rotate(new Vector3(0, Time.deltaTime * knifeSpeed, 0));

           
        }
        else
        {
            particleSmoke.gameObject.transform.DOScale(0.0f, 5);
            cryParicleOne.gameObject.transform.DOScale(0.0f, 5);
            cryParicleTwo.gameObject.transform.DOScale(0.0f, 5);

            animator.SetBool("Move", false);
            isWalking = false;
        }
        if (isWalking == false && rest == true)
        {
            stamina += upGradeValue * Time.deltaTime;
            skinMaterial.DOColor(Color.white, 5);
        }
        if (stamina < 1)
        {
            stamina = 0;
            TurnBack();
            update = true;
            animator.SetBool("Fall", false);
        }
        if (stamina <= criticalPoint)
        {
            skinMaterial.DOColor(Color.red, criticalPoint);
            particleSmoke.gameObject.SetActive(true);
            particleSmoke.Play();
            particleSmoke.gameObject.transform.DOScale(0.04f, 5);
            cryParicleOne.Play();
            cryParicleOne.gameObject.transform.DOScale(2.5f, 1);
            cryParicleTwo.Play();
            cryParicleTwo.gameObject.transform.DOScale(2.5f, 1);

        }

        if (stamina > criticalPoint)
        {
            skinMaterial.color = Color.white;
        }
        //TurningKnife();
        //Debug.Log(stamina);
    } 
   
    public void TurnBack()
    { 
        
        this.gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(15f, 0, -16);
        this.gameObject.SetActive(true);
        //updateCanvas.SetActive(true);
        //walkSpeed = 0;
        fine = false;
        //yield return new WaitForSecondsRealtime(3f);
        wait = true;
        //this.gameObject.SetActive(false);
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            rest = false;
            fine = false;
            isWalking = true;
            //animator.SetBool("Fall", true);
            gameObject.transform.DORotate(new Vector3(0, 180, 0), 1f);
            gameObject.transform.DOMove(FinisWalk, 0.5f).OnComplete(() => AnimationControl());
            skinMaterial.DOColor(Color.white, criticalPoint);
        }
     
    }
   public void AnimationControl()
    {
        
        isWalking = false;
        animator.SetBool("Fall", true);
        confetiParticle.Play();
        levelComplete = true;
    }
}

    
