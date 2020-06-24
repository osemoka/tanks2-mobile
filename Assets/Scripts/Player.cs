using UnityEngine;
using System.Collections;

//This script manages the player object
public class Player : MonoBehaviour
{

    public bool facingRight = false;
    public bool facingUp = true;
    public bool facingLeft = false;
    public bool facingDown = false;

    public float speed = 0.2f;
    public int bullet_speed = 3;
    public Rigidbody2D bullet_pref;
    public Vector2 lastMove = new Vector2(0, 0);

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);
       

        if (x > 0)
        {
            facingRight = true;
            facingDown = facingLeft = facingUp = false;
        }
        else if (x < 0)
        {
            facingLeft = true;
            facingRight = facingDown = facingUp = false;
        }
        else if (y > 0)
        {
            facingUp = true;
            facingDown = facingLeft = facingRight = false;
        }
        else if (y < 0)
        {
            facingDown = true;
            facingRight = facingLeft = facingUp = false;
        }
        #region fire
        if (Input.GetButtonDown("Fire1"))
        {
            //animator.SetTrigger("Shoot");
            Rigidbody2D bulletInstance = new Rigidbody2D();
            
            if (facingRight)
            {
                // ... instantiate the bullet_pref facing right and set it's velocity to the right. 
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(bullet_speed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x += 0.2f;
                bulletInstance.transform.position = temp;

                
            }
            else if (facingLeft)
            {
                // Otherwise instantiate the bullet_pref facing left and set it's velocity to the left.
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-bullet_speed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x -= 0.2f;
                bulletInstance.transform.position = temp;

            }
            else if (facingUp)
            {
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(180f, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, bullet_speed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y += 0.2f;
                bulletInstance.transform.position = temp;

            }
            else if (facingDown)
            {
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, -bullet_speed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y -= 0.2f;
                bulletInstance.transform.position = temp;

            }
              Destroy(bulletInstance.gameObject, 1);



        }
        #endregion

        Move(direction);
        
    }

    /*
    public void TouchMoveRight(float kierunek)
    {              
        facingRight = true;
        facingDown = facingLeft = facingUp = false;
        //Move(direction);        
    }
     * */



    void Move(Vector2 direction)
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        short x1 = 0, y1 = 0;
        if (inputX > 0)
        {
            x1 = 1;
            y1 = 0;
        }
        if (inputY > 0)
        {
            y1 = 1;
            x1 = 0;
        }
        Vector2 speedy = new Vector2(1, 0);
        Vector2 movement = new Vector2(speedy.x * x1, speedy.y * y1);

        animator.SetFloat("speedX", x1);
        animator.SetFloat("speedY", y1);

             
        Vector2 pos = transform.position;

        if (direction.x != 0 && direction.y != 0)
        {
            if (lastMove.y != 0)
            {
                direction.x = 0;
            }
            else if (lastMove.x != 0) direction.y = 0;
        }

        //Calculate the proposed position
        pos += direction * speed * Time.deltaTime;
          
        transform.position = pos;
    }

        
    void FixedUpdate()
    {

        float lastInputX = Input.GetAxis("Horizontal");
        float lastInputY = Input.GetAxis("Vertical");
        //Vector3 theScale = transform.localScale;

        if (lastInputX != 0 || lastInputY != 0)
        {
            animator.SetBool("walking", true);
            if (lastInputX > 0)
            {
                animator.SetFloat("lastmovex", 1f);
                lastMove = new Vector2(1, 0);

            }
            else if (lastInputX < 0)
            {
                animator.SetFloat("lastmovex", -1f);
                lastMove = new Vector2(-1, 0);

            }
            else
            {
                animator.SetFloat("lastmovex", 0f);
            }

            if (lastInputY > 0)
            {
                animator.SetFloat("lastmovey", 1f);
                lastMove = new Vector2(0, 1);


            }
            else if (lastInputY < 0)
            {
                animator.SetFloat("lastmovey", -1f);
                lastMove = new Vector2(0, -1);

            }
            else
            {
                animator.SetFloat("lastmovey", 0f);

            }
            //animator.SetBool("walking", false);
        }
        else
        {
            animator.SetBool("walking", false);
        }
       // transform.localScale = theScale;


        if (lastInputX > 0 && facingLeft)
        {
            animator.StopPlayback();
            Flip();
        }
        else if (lastInputX < 0 && facingRight)
        {
            animator.StopPlayback();
            Flip();
        }

        if (lastInputY > 0 && facingDown)
        {
            animator.StopPlayback();
            Flip2();
        }
        else if (lastInputY < 0 && facingUp)
        {
            animator.StopPlayback();
            Flip2();
        }

    }


    void Flip()
    {
        facingRight = !facingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= 1;
        transform.localScale = theScale;
    }

    void Flip2()
    {
        facingUp = !facingDown;

        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    public bool trigger = false;
    /*
    void OnTriggerEnter2D(Collider2D c)
    {
        trigger = true;
        //Get the layer of the collided object
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        //If the player hit an enemy bullet or ship...
        if (layerName == "Bullet (Enemy)" || layerName == "Enemy")
        {
            //...and the object was a bullet...
            if (layerName == "Bullet (Enemy)")
                //...return the bullet to the pool...
                ObjectPool.current.PoolObject(c.gameObject);
            //...otherwise...
            else
                //...deactivate the enemy ship
                c.gameObject.SetActive(false);

            //Tell the manager that we crashed
            Manager.current.GameOver();
            //Trigger an explosion
            //Explode();
            //Deactivate the player
            gameObject.SetActive(false);
        }
    } */
}
