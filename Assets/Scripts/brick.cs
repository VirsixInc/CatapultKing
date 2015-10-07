using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class brick : MonoBehaviour
{
  
    public GameManager currManager;
    public int id;
    private List<GameObject> Blocks;

    public int maxHealth = 150; //todo make maxhealth
    private int currentHealth;



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
      if(col.gameObject.GetComponent<Rigidbody>()){
        damageToReceive = (int)(damageToReceive*col.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        if(damageToReceive < 3){
          return;
        }
      }

      //Debug.Log("I'm attached to " + gameObject.name);


         
      if(gameObject.tag == "block"){
        currentHealth = currentHealth-damageToReceive;
        if(currManager != null){
          bool isDead = currManager.allBricks[id].updateBlock((int)currentHealth);
          if(isDead){
            GameObject[] shatters = TurboSlice.instance.shatter(gameObject, 2);
            foreach (GameObject shattered in shatters){
              shattered.gameObject.tag = "fragment";
              shattered.GetComponent<Rigidbody>().velocity = shattered.GetComponent<Rigidbody>().velocity / 2;
                                
            }
            Destroy(gameObject);
          }
        }
      }
      //if health is 0, destroy the block
      //if (Health <= 0) Destroy(this.gameObject);
        
    }

}
