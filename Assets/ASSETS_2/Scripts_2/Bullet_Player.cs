using UnityEngine;

public class Bullet_Player : MonoBehaviour
{
    private int speed = 3;			//How fast the bullet moves
    public int power = 1;			//Power of the bullet

    public GameObject wykryty_enemy;
    public string enemy_name;

    void OnEnable()
    {
        //Send the bullet "forward"
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
    }

    void OnDisable()
    {
        CancelInvoke("Die");
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        wykryty_enemy = c.gameObject;
        enemy_name = c.gameObject.tag.ToString();

        if (enemy_name == "Enemy")
        {
            Destroy(this.gameObject);
        }

        else if (enemy_name == "Mur")
        {
            Destroy(this.gameObject);
        }

        else if (enemy_name == "PowerUp")
        {
            Destroy(this.gameObject);
        }

        else if (enemy_name == "Bullet_Enemy")
        {
            Destroy(this.gameObject);
        }

        else if (enemy_name == "Bullet_Player")
        {
            Destroy(this.gameObject);
        }
    }
}