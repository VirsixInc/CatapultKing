using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts;
using System.Linq;
using System;

public class brick : MonoBehaviour
{
  
    public float Health = 70f; //todo make maxhealth
    public GameManager currManager;
    public float damage;
    public int id;
    private List<GameObject> Blocks;
    public float shattimer =5.0f;
    private float currentHealth;




    void Start(){
      if(gameObject.tag == "broken"){
            
           Destroy(this);
      }

      currManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      //currentHealth = maxHealth;
    }
    void Update()
    {
    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponentInParent<Rigidbody>() == null) return;
        gameObject.transform.parent.GetComponent<Rigidbody>();
        damage = col.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude * 2;
       
        //Debug.Log("I'm attached to " + gameObject.name);


           
        if(gameObject.tag == "block"){
          Health -= damage;
          currentHealth = Health;
          if(currManager != null){
            bool isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
            if(isDead){
              GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
              foreach (GameObject shattered in shatters){
                shattered.gameObject.tag = "broken";
                shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
                    }
              Destroy(gameObject);
            }
          }
        }
        //if health is 0, destroy the block
        //if (Health <= 0) Destroy(this.gameObject);
        
    }
    public void takeDamage(float dmgAmt){

    }
    /*IEnumerator shattertimer()
    {
        Debug.Log("before timer");
        
       yield return new WaitForSeconds(4);
        
        Debug.Log("After timer");
        yield break;
    }*/

    private void Destroy(GameObject gameObject, object bulletlifetime)
    {
        throw new NotImplementedException();
    }
}
