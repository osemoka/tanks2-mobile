using UnityEngine;
using System.Collections;

public class Enemy_Script : MonoBehaviour {

    private Manager m;

    private Vector3 Player;
    private Vector2 Playerdirection;
    private float Xdif;
    private float Ydif;
    private float speed = 0.5f;
    public Rigidbody2D rb2d;
    public Vector2 temp = new Vector2(0, 0);
    public Vector2 temp2 = new Vector2(0, 0);
    public float nextUsage;
    private float delay = 1f;
    private Random rand = new Random();
    private Animator animator;
    public Rigidbody2D bullet_pref_enemy;
    public int bullet_speed_enemy = 2;
    public bool pmin = false;
    public bool emin = false;


    public float nextFire = 1f;
    private float bullet_cd = 1.0f;
    public bool bullet_fired = false;


    public enum lastDirection
    {
        Left,
        Right,
        Up,
        Down
    };

    public lastDirection dir = lastDirection.Down;



    // Use this for initialization
    void Start() {

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();

        //  StartCoroutine(EnemyMove());	
    }

   
    public void EnemyMove()
    {
        #region old_code
        //przeciez x enemey i punktu c zawsze są równe

        /* if (Mathf.Abs(transform.position.x - temp2.x) > 0.5f)
         {
             Debug.Log("jestem w x");
             temp.y = 0;
             if (transform.position.x - temp2.x > 0) temp.x -= 1; 
             else temp.x += 1;
         }*/
        /*
        if (Mathf.Abs(transform.position.y - temp2.y) == 0 && Mathf.Abs(transform.position.x - temp2.x) != 0)
        {
            Debug.Log("jestem w x");
            temp.y = 0;
            if (transform.position.x - Player.x > 0) temp.x = -1;
            else temp.x = 1;
        }
        else if(Mathf.Abs(transform.position.y - temp2.y) == 0 && Mathf.Abs(transform.position.x - temp2.x) == 0)
        {           
            Debug.Log("jestem w y");
            temp.x = 0;
            if (transform.position.y - temp2.y > 0) temp.y = -1;
            else temp.y = 1;
        }
        */
        #endregion
        // Debug.Log("Temp2: " + temp2 + " , enemy: " + transform.position);
        #region random_movement
        /*
        int asdf = Random.Range(1, 4);
        switch (asdf)
        {
            case 1: //gora
                temp.y = 0.5f;
                temp.x = 0;
                break;

            case 2: //prawo
                temp.y = 0;
                temp.x = 0.5f;
                break;

            case 3: //dol
                temp.y = -0.5f;
                temp.x = 0;
                break;

            case 4: //lewo
                temp.y = 0;
                temp.x = -0.5f;
                break;

        }
        */
        #endregion
       
        //animator.SetBool("Moving", true);
        Vector2 P = new Vector2(Player.x, Player.y);
        Vector2 E = new Vector2(transform.position.x, transform.position.y);
        bool test = (Mathf.Abs(Mathf.Abs(P.x) - Mathf.Abs(E.x)) > Mathf.Abs(Mathf.Abs(P.y) - Mathf.Abs(E.y)));
        if (Mathf.Abs(Mathf.Abs(P.x) - Mathf.Abs(E.x)) > Mathf.Abs(Mathf.Abs(P.y) - Mathf.Abs(E.y))) //go horizontal
        {
           // Debug.Log();
           // Debug.Log("Go Left/Right " + "Longer Distance!");
            temp.y = 0;
            if (P.x > E.x)
            {
                dir = lastDirection.Right;
                animator.SetBool("FacingRight", true);
                ResetFacesCustom("FacingRight");
                temp.x = speed;
            }
            else if (P.x <= E.x) 
            {
                dir = lastDirection.Left;
                animator.SetBool("FacingLeft", true);
                ResetFacesCustom("FacingLeft");
                temp.x = -speed;
            }
            else Debug.Log("OMG WTF");
        }
        else if (Mathf.Abs(Mathf.Abs(P.x) - Mathf.Abs(E.x)) <= Mathf.Abs(Mathf.Abs(P.y) - Mathf.Abs(E.y))) //go vertical
        {
           // Debug.Log("Go Up/Down, Longer Distance!");
            temp.x = 0;
            if (P.y > E.y)
            {
                dir = lastDirection.Up;
                animator.SetBool("FacingUp", true);
                ResetFacesCustom("FacingUp");
                temp.y = speed;
            }
            else if (P.y <= E.y)
            {
                dir = lastDirection.Down;
                animator.SetBool("FacingDown", true);
                ResetFacesCustom("FacingDown");
                temp.y = -speed;
            }

            else Debug.Log("OMG WTF");
        }
        else Debug.Log("OMG, HUSTON WE HAVE A PROBLEMS");

        rb2d.velocity = temp;
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

    public void getpointC(Vector3 playerpos, Vector3 enemypos)
    {

        //pierwsza możliwość punktu C
        temp2.x = playerpos.x;
        temp2.y = enemypos.y;
        //druga możliwość punktu C
        //temp2.x = b.x;
        // temp2.y = a.y;
        //Debug.Log("temp2: " + temp2);
    }

    void enemy_shoot()
    {

    }
    void FixedUpdate() {

        Player = GameObject.Find("Player").transform.position;
        Vector2 P = new Vector2(Player.x, Player.y);
        Vector2 E = new Vector2(transform.position.x, transform.position.y);
        //  getpointC(Player, this.transform.position);
        if ((Mathf.Abs(Mathf.Round(P.x*100)) - Mathf.Abs(Mathf.Round(E.x*100)) <0.05f) || // 
            Mathf.Abs(Mathf.Round(P.y*100)) - Mathf.Abs(Mathf.Round(E.y*100)) <0.05f)
        {          
            //Debug.Log("P: " + P + "E: " + E  );
            //EnemyMove();
            Rigidbody2D bulletInstance = new Rigidbody2D();
            Vector3 bullet_pos = new Vector3(0,0,0);
            if (!bullet_fired) 
            {
               // Debug.Log("Bulled_fired: " + bullet_fired);
                switch (dir)
                {
                    case lastDirection.Right:
                        bulletInstance = Instantiate(bullet_pref_enemy, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(bullet_speed_enemy, 0);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.x += 0.2f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 180f, 90f));
                        break;
                    case lastDirection.Left:
                        bulletInstance = Instantiate(bullet_pref_enemy, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(-bullet_speed_enemy, 0);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.x -= 0.2f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                        break;
                    case lastDirection.Up:
                        bulletInstance = Instantiate(bullet_pref_enemy, transform.position, Quaternion.Euler(new Vector3(180f, 0, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(0, bullet_speed_enemy);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.y += 0.2f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        break;
                    case lastDirection.Down:
                        bulletInstance = Instantiate(bullet_pref_enemy, transform.position, Quaternion.Euler(new Vector3(0, 180f, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(0, -bullet_speed_enemy);
                        bullet_pos = bulletInstance.transform.position;
                        bullet_pos.y -= 0.2f;
                        bulletInstance.transform.position = bullet_pos;
                        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180f));
                        break;
                }

                Destroy(bulletInstance.gameObject, 5);
            }
            bullet_fired = true;

        }
        if (Time.time > nextUsage)
        {
           // Debug.Log("Current: " + Time.time + " , delay: " + delay + " , nextUsage: " + nextUsage);
            nextUsage = Time.time + delay;
            EnemyMove();
           // Debug.Log("P: " P + "E:  " + E);

        }
        
            if (Time.time > nextFire)
            {
                nextFire = Time.time + bullet_cd;
            //Debug.Log("lol: " + bullet_fired);
                bullet_fired = false;
            }

        #region stary move
        // Xdif = Player.x - transform.position.x;
       // Ydif = Player.y - transform.position.y;

       // Playerdirection = new Vector2(Xdif, Ydif);
       // temp = new Vector2(0, 0);

        /*
        if (Xdif < Ydif && !(Mathf.Abs(Ydif - Xdif) < 0.2f))
        {
            temp.y = 0;
            if (Xdif < 0) temp.x -= 1;
            else temp.x += 1;
        }
        else if (Ydif < Xdif && !(Mathf.Abs(Ydif - Xdif) < 0.2f))
        {
            temp.x = 0;
            if (Ydif < 0) temp.y -= 1;
            else temp.y += 1;
        }

        if (Mathf.Abs(Ydif) < 0.05f || Mathf.Abs(Ydif-Xdif) < 0.05f)
        {
           // Debug.Log("wyrownuje x");
            temp.y = 0;
            if (Xdif > 0)
            {
                temp.x += 1;
            }
            else
            {
                temp.x -= 1;
            }
        }
        if (Mathf.Abs(Xdif) < 0.05f)
        {
           // Debug.Log("wyrownuje y  ");

            temp.x = 0;
            if (Ydif > 0)
            {
                temp.y += 1;
            }
            else
            {
                temp.y -= 1;
            }
        }
        */
        //Debug.Log(Xdif + " : " + Ydif + " : " + temp + " : " + transform.position);
        //transform.position = temp;        
        //rb2d.AddForce(Playerdirection.normalized * speed);  
        #endregion
    }

    void OnCollisionEnter2D(Collision2D c)
    {

        if (c.gameObject.tag.ToString() == "Bullet_Player")
        {
            m.score++;
            //animator.SetBool("Hit", true);
            Destroy(gameObject);
        }

    }
}
//jezeli chodzi o to jak maja zachowywac enemy - docelowo leca by zniszczy orzelka, ale gdy player znajdzie sie w opdowiedniej odleglosci to wlacza sie agro i chca go zniszczyc
