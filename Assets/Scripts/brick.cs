using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class brick : MonoBehaviour
{
    public float Health = 70f;
    public float damage;
    private List<GameObject> Blocks;


    void Update()
    {}



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponentInParent<Rigidbody>() == null) return;
        gameObject.transform.parent.GetComponent<Rigidbody>();
        damage = col.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude * 10;
       
        //Debug.Log("I'm attached to " + gameObject.name);

        Health -= damage;
        //if health is 0, destroy the block
        //if (Health <= 0) Destroy(this.gameObject);
        
    }
}
