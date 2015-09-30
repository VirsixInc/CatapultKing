using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{


    void OnTriggerEnter(Collider col)
    {
        //destroyers are located in the borders of the screen
        //if something collides with them, the'll destroy it
        string tag = col.gameObject.tag;
        if (tag == "block" || tag == "enemy" || tag == "ball")
        {
            Destroy(col.gameObject);
        }
    }
}
