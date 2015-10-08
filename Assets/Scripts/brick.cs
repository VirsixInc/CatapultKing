using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts;
using System.Linq;
using System;

public class brick : MonoBehaviour
{
  
    public GameManager currManager;
    public int id;
    private List<GameObject> Blocks;
<<<<<<< HEAD

    public int maxHealth = 70; //todo make maxhealth
    private int currentHealth;


=======
    public float shattimer =5.0f;
    private float currentHealth;
>>>>>>> e0b60e0e3a91346a0374f709801b896c3f828828




    void Start(){
      if(gameObject.tag == "broken"){
            
           Destroy(this);
      }

      currManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      currentHealth = maxHealth;
    }
    void Update()
    {
    }



    void DestroyBlock(){
      currManager.allBricks[id].updateBlock(0); //KILL BLOCK
      Destroy(gameObject);
    }
    void OnCollisionEnter(Collision col)
    {
      if(col.gameObject.tag == "ground"){
        DestroyBlock();
      }
      int damageToReceive;
      if(currManager.damageValues.TryGetValue(col.gameObject.tag, out damageToReceive)){
      }else{
        return;
      }
     
      //Debug.Log("I'm attached to " + gameObject.name);


<<<<<<< HEAD
         
      if(gameObject.tag == "block"){
        currentHealth = currentHealth-damageToReceive;
        if(currManager != null){
          bool isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
          if(isDead){
            GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
            foreach (GameObject shattered in shatters){
              shattered.gameObject.tag = "fragment";
              shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
                                
=======
           
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
>>>>>>> e0b60e0e3a91346a0374f709801b896c3f828828
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

















