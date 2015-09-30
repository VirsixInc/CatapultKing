using UnityEngine;
using System.Collections;

public class brick : MonoBehaviour
{


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Rigidbody>() == null) return;

        float damage = col.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 10;
        //don't play audio for small damages
        if (damage >= 10)
            GetComponent<AudioSource>().Play();
        //decrease health according to magnitude of the object that hit us
        Health -= damage;
        //if health is 0, destroy the block
        if (Health <= 0) Destroy(this.gameObject);
        
    }
    
    public float Health = 70f;
    

}
