﻿using UnityEngine;
using System.Collections;

// Require these components when using this script
// a reference to the capsule collider of the character


public class enemycontact : MonoBehaviour
{

    public float Health = 150f;
   
    private float EnemyHealth;
    // Use this for initialization
    void Start()
    {
        EnemyHealth = Health - 30f;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Rigidbody>() == null) return;
          //if we are hit by a ball
        if (col.gameObject.tag == "ball")
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
            {
                GetComponent<AudioSource>().Play();
            }

            if (Health < EnemyHealth)
            {//change the shown animation
                Debug.Log("dkjlhfalkdsajlkfjkaldfjlkdasjfkldsjflkdjfoiwekfjkdlnfkjefijfkdjfkeajifjdskjfiejflkdjflka");
                GetComponent<Animation>().Play("Shotgun");
                
            }
            if (Health <= 0) Destroy(this.gameObject);
        }
    }
}