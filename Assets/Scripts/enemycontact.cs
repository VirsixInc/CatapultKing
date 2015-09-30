using UnityEngine;
using System.Collections;

public class enemycontact : MonoBehaviour 
{

    public float Health = 150f;
    private float EnemyHealth;
    // Use this for initialization




    void Start()
    {
        EnemyHealth = Health - 30.0f;

    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Rigidbody>() == null) return;

        //if we are hit by a ball
        if (col.gameObject.tag == "enemy")
        {
            GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else //we're hit by something else
        {
            //calculate the damage via the hit object velocity
            float damage = col.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 10;
            Health -= damage;
            //don't play sound for small damage
            if (damage >= 10)
                GetComponent<AudioSource>().Play();
            /*
            //Change animation hear when worked out
            if (Health < ChangeSpriteHealth)
            {//change the shown sprite
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }
            */
           

                
            if (Health <= 0) Destroy(this.gameObject);
        }
    }


}
