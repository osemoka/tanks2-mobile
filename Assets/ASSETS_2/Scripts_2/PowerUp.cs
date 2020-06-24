using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public Player_Script mov;
	// Use this for initialization
	void Start ()
    {
        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Script>();
	}
	
	// Update is called once per frame
	void Update () {
	

	}

    void OnTriggerEnter2D(Collider2D c)
    {           

        if (c.CompareTag("Player"))
        {
            mov.speed = 2f;
            Destroy(gameObject);
        }
    }

}
