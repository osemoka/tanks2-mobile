using UnityEngine;
using System.Collections;

public class PowerUp2 : MonoBehaviour
{

    public Player_Script mov;
    // Use this for initialization
    void Start()
    {
        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Script>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.CompareTag("Player"))
        {
            mov.bullet_speed = 2;
            //mov.bullet_pref = jakis inny sprie? ale to opcjonalnie w przyszlosci
            Destroy(gameObject);
        }
    }

}
