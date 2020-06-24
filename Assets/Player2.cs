using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour
{
    public float maxSpeed = 1;
    public float speed = 1f;
    private Rigidbody2D rb2d;
    private Animator anim;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime);
           // transform.localScale = new Vector3(1, 1, 1);
            
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
          //  transform.localScale = new Vector3(1, 1, 1);
            
            return;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * Time.deltaTime);
          //  transform.localScale = new Vector3(1, 1, -1);
           
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.up * Time.deltaTime);
         //   transform.localScale = new Vector3(1, -1, 1);
            
            return;
        }
    

        /*
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed);
            transform.localScale = new Vector3(1, 1, -1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * speed);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed);
            transform.localScale = new Vector3(1, -1, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * speed);
            transform.localScale = new Vector3(1, 1, 1);
        }
        

        /*
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if(Input.GetAxis("Horizontal") < 0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if(Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } */

    }


   

    void FixedUpdate()
    {


       /* float h = Input.GetAxis("Horizontal");

        rb2d.AddForce((Vector2.right * speed) * h);


      /*  if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
        */

    }

}