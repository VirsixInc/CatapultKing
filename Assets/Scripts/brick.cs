using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class brick : MonoBehaviour
{
    public float Health = 70f;
    private List<GameObject> Blocks;

    void Update()
    {}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Rigidbody>() == null) return;

        float damage = col.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 10;
        //don't play audio for small damages
        /*
        if (damage >= 10)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                  //GetComponent<AudioSource>().Play();
            }

            // GetComponent<AudioSource>().Play();
        }*/
        //decrease health according to magnitude of the object that hit us
        

        Health -= damage;
        //if health is 0, destroy the block
        //if (Health <= 0) Destroy(this.gameObject);
        
    }
}
