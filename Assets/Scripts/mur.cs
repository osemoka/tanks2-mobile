using UnityEngine;
using System.Collections;

public class mur : MonoBehaviour {

    private Animator anim;


    void Awake()
    {
        // Setting up the reference.
        anim = transform.root.GetComponent<Animator>();
    }


    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("jestem Mur i walnelo we mnie: " + c.gameObject.tag.ToString());
        if (c.gameObject.tag.ToString() == "Bullet_Player")
        {
            Destroy(gameObject);
        }
        
    }


}
