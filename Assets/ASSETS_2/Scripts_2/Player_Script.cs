using UnityEngine;
using System.Collections;

//This script manages the player object
public class Player_Script : MonoBehaviour
{
    #region facing bools
    public bool FacingRight = false;
    public bool FacingUp = true;
    public bool FacingLeft = false;
    public bool FacingDown = false;
    public bool IsTouching = false;
    #endregion
    public Rigidbody2D bullet_pref;
    public Vector2 TouchDirection = new Vector2(0, 0);

    public float speed = 0.5f;
    public int bullet_speed = 1;
    private Animator animator;
    private Manager m;

    /*
    public GameObject Tank;		
    public int lives = 1;
        
    //Tu ustawimy co się dzieję w trakcie kiedy mamy daną ilość żyć, wstępnie Loadlevel(3) - to będzie obrazek game over, który utrzymuję się przez chwilę i powraca do MainMenu
    public void Death()
    {
        if (lives > 0)
        {
            //animator.SetBool("dead");
            Destroy(gameObject);
            respawn();
            speed = 1f;
            bullet_speed = 1f;
            bullet_cd = 1f;
            ?hp = 1f;
            *bullet_pwr = 1f;

        }
        if (lives < 1)
        {
            //animator.SetBool("dead");
            Destroy(gameObject);
            Application.LoadLevel(3);
        }
    }

    public void respawn()
    {
        Instantiate(Tank, transform.position, transform.rotation);
    }
    * */ // to jest do śmierci i ilosci zyc

    enum lastDirection
    {
        Left,
        Right,
        Up,
        Down
    };

    lastDirection lastDir =  lastDirection.Up;

    public string tempxx;
    void Start()
    {
        animator = GetComponent<Animator>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
       // pm = GameObject.FindGameObjectWithTag("PauseUI").GetComponent<PauseMenu>();

    }

   

    void Update()
    {
        tempxx = lastDir.ToString(); // Debuging purpose
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);


        if (x > 0 || TouchDirection.x > 0)
        {
            animator.SetBool("FacingRight", true);
            lastDir = lastDirection.Right;
        }
        else if (x < 0 || TouchDirection.x < 0)
        {
            animator.SetBool("FacingLeft", true);
            lastDir = lastDirection.Left;
        }
        else if (y > 0 || TouchDirection.y > 0)
        {
            animator.SetBool("FacingUp", true);
            lastDir = lastDirection.Up;
        }
        else if (y < 0 || TouchDirection.y < 0)
        {
            animator.SetBool("FacingDown", true);
            lastDir = lastDirection.Down;
        }
        switch (lastDir)
        {
            case lastDirection.Up:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                ResetFacesCustom("FacingUp");
                break;
            case lastDirection.Down:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 180f));
                ResetFacesCustom("FacingDown");
                break;
            case lastDirection.Right:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
                ResetFacesCustom("FacingRight");
                break;
            case lastDirection.Left:
                transform.rotation = Quaternion.Euler(new Vector3(180f, 0, 90f));
                ResetFacesCustom("FacingLeft");

                break;
            default:
                break;
        }
        
        #region fire
        
        if (Input.GetButtonDown("Fire1"))
        {
            //animator.SetTrigger("Shoot");
            Rigidbody2D bulletInstance = new Rigidbody2D();

            if (lastDir == lastDirection.Right)
            {
                // ... instantiate the bullet_pref Facing right and set it's velocity to the right. 
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(bullet_speed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x += 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation =  Quaternion.Euler(new Vector3(0, 180f, 90f));

            }
            else if (lastDir == lastDirection.Left)
            {
                // Otherwise instantiate the bullet_pref Facing left and set it's velocity to the left.
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-bullet_speed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x -= 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

            }
            else if (lastDir == lastDirection.Up)
            {
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(180f, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, bullet_speed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y += 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            else if (lastDir == lastDirection.Down)
            {
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, -bullet_speed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y -= 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));

            }
            Destroy(bulletInstance.gameObject, 5);



        }
        
        #endregion
        
        if (IsTouching == false)
        {
            if (Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.y) > 0) Move(direction);
            else if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0) Move(direction);
            else
            {
                animator.SetBool("Moving", false);
                ResetFaces();
            }
        }
        else
        {
           // Debug.Log("ruch z toucha: " + TouchDirection.ToString());
            Move(TouchDirection);
        }

    }

    void ResetFaces()
    {
        animator.SetBool("FacingDown", false);
        animator.SetBool("FacingUp", false);
        animator.SetBool("FacingLeft", false);
        animator.SetBool("FacingRight", false);
    }

    void ResetFacesCustom(string custom)
    {
        if (custom != "FacingDown") animator.SetBool("FacingDown", false);
        if (custom != "FacingUp") animator.SetBool("FacingUp", false);
        if (custom != "FacingLeft") animator.SetBool("FacingLeft", false);
        if (custom != "FacingRight") animator.SetBool("FacingRight", false);
    }

    public void Move2(int x)
    {
        //Debug.Log("costam: " + x );
        Vector2 temp = new Vector2(0, 0);
        switch (x)
        {
            case 1:
                temp = new Vector2(0f, 1f);               
                animator.SetBool("FacingUp", true);               
                IsTouching = true;
                break;
            case 2:
                temp = new Vector2(0f, -1f);               
                animator.SetBool("FacingDown", true);               
                IsTouching = true;
                break;
            case 3:
                temp = new Vector2(1f, 0f);
                animator.SetBool("FacingRight", true);
                IsTouching = true;
                break;
            case 4:
                temp = new Vector2(-1f, 0f);
                animator.SetBool("FacingLeft", true);
                IsTouching = true;
                break;
            default:
                break;

        }
        TouchDirection = temp;
        animator.SetBool("Moving", true);
        Move(temp);
    }

    public void fire(int x)
        
    {
        #region touch fire
       // if (Input.GetButtonDown("Fire1"))
      //  {
            //animator.SetTrigger("Shoot");
            Rigidbody2D bulletInstance = new Rigidbody2D();

            if (lastDir == lastDirection.Right)
            {
                // ... instantiate the bullet_pref Facing right and set it's velocity to the right. 
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(bullet_speed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x += 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 90f));

            }
            else if (lastDir == lastDirection.Left)
            {
                // Otherwise instantiate the bullet_pref Facing left and set it's velocity to the left.
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(-bullet_speed, 0);
                Vector3 temp = bulletInstance.transform.position;
                temp.x -= 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

            }
            else if (lastDir == lastDirection.Up)
            {
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(180f, 0, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, bullet_speed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y += 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            }
            else if (lastDir == lastDirection.Down)
            {
                bulletInstance = Instantiate(bullet_pref, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(0, -bullet_speed);
                Vector3 temp = bulletInstance.transform.position;
                temp.y -= 0.2f;
                bulletInstance.transform.position = temp;
                bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));

            }
            Destroy(bulletInstance.gameObject, 5);

        
    }
        #endregion


    public void ChangeBool(int touch)
    {
        IsTouching = false;
    }

   public void Move(Vector2 direction)
    {
        switch (lastDir)
        {
            case lastDirection.Up:
                direction.x = 0;
                break;
            case lastDirection.Down:
                direction.x = 0;
                break;
            case lastDirection.Right:
                direction.y = 0;
                break;
            case lastDirection.Left:
                direction.y = 0;
                break;
            default:
                break;
            
        }
        animator.SetBool("Moving", true);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;
        transform.position = pos;
    }

   void OnCollisionEnter2D(Collision2D c)
   {

       if (c.gameObject.tag.ToString() == "Bullet_Enemy")
       {
           Destroy(gameObject);
           if (PlayerPrefs.HasKey("Highscore"))
           {
               if (PlayerPrefs.GetInt("Highscore") < m.score)
               {
                   PlayerPrefs.SetInt("Highscore", m.score);
               }
           }
           else
           {
               PlayerPrefs.SetInt("Highscore", m.score);
           }

           PlayerPrefs.SetInt("Score", m.score);
           Application.LoadLevel(0);
       }

   }

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

//na pewno trzeba zrobić cd pocisków oprócz sterowania. na Playerze jak i na Enemy(tu szczelanie i animacja zalezna od kierunku)