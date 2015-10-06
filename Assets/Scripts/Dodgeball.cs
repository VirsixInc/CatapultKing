using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Dodgeball : MonoBehaviour {
    public float bulletlifetime = 3.0f;
    public GameObject hitParticle;
    public float partlifetime = 0.1f;
    public ParticleSystem hitpart;

    //public ParticleEmitter hitpart;

    // Update is called once per frame
    void Update () {}
    void Start()
    {
        ParticleSystem hitpart = GetComponent<ParticleSystem>();
    }
    /*
    void FixedUpdate()
    {
        StartCoroutine(DestroyAfter(2));

    }

    IEnumerator(DestroyAfter(float seconds)
    {
        yeild return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }*/



    void OnCollisionEnter(Collision coll) {
        SpecialEffectsHelper.Instance.Explosion(transform.position);

        //GameObject.Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(GameObject.FindGameObjectWithTag("ball"), bulletlifetime);
        //hitpart.Emit();
       /* if (hitpart)
        {
            if (!hitpart.IsAlive())
                Destroy(gameObject);
        }
        */

        Destroy(GameObject.FindGameObjectWithTag("particle"), partlifetime);
        
    }

}
