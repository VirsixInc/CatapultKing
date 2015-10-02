using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class brick : MonoBehaviour
{
    public GUIStyle progress_empty;
    public GUIStyle progress_full;

    //current progress

    public float Health = 70f;
    Vector2 pos = new Vector2(10, 50);
    Vector2 size = new Vector2(250, 50);

    public Texture2D emptyTex;
    public Texture2D fullTex;

    private List<GameObject> Blocks;


    void Update()
    {
       
    }


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
        if (Health <= 0) Destroy(this.gameObject);
        
    }
    


}
