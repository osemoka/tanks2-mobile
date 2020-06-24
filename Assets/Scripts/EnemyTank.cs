using UnityEngine;
using System.Collections;

public class EnemyTank : MonoBehaviour {

    private Manager m;
	// Use this for initialization
	void Start () 
    {
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionEnter2D(Collision2D c)
    {
       
        if (c.gameObject.tag.ToString() == "Bullet")
        {
            m.score++;
            Destroy(gameObject);
        }
        
    }
}
