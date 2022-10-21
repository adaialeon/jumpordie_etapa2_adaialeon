using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class playermovement : MonoBehaviour
{

    //VIDEO
    public Animator anim;

    public PlayableDirector director;
    private Rigidbody2D rb;

    public bool isGrounded;

    public float jumpForce = 10f;

    float dirX;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();    

    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector2 (horizontal * speed, rb.velocity.y);
    }

    //Movimiento del personaje
    public float speed = 5.5f; 
    

    //Almacenamiento de imputs, movimiento horizontal
    private float horizontal;

    //Variable para acceder al transform
    private Transform playerTransform;

    //Se ejecuta en el primer frame
    void Start()
    {
        playerTransform = GetComponent<Transform>();
    }

    //Se ejecuta una vez por frame
    void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");
        Debug.Log(dirX);

        horizontal = Input.GetAxisRaw("Horizontal");     
        
        //Rotacion del personaje
        if(dirX ==-1)
        {
            anim.SetBool("Correr", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(dirX == 1)
        {
            anim.SetBool("Correr", false);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            anim.SetBool("Correr", false);
        }

        //Activar las animaciones del Animator
        if(horizontal == 0)
        {
            anim.SetBool("Correr", false);
        }   
        else 
        {
            anim.SetBool("Correr", true);
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("Saltar", true);
        }

        //Acceder a la posicion del transform (se puede escribir de varias maneras)
        //playerTransform.position += new Vector3 (horizontal * speed * Time.deltaTime, 0, 0);
        //playerTransform.position += new Vector3 (1, 0, 0) * horizontal * speed * Time.deltaTime;
        //playerTransform.Translate(Vector3.right * horizontal * speed * Time.deltaTime, Space.World);
       
    }

    //Activar la cinematica
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Cinematica")  
        {
            director.Play();
        }
    }   
}

