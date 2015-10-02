using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    public string buttonName = "Fire1";
    public float force = 100.0f;
    public float radius = 5.0f;
    public float upwardsModifier = 0.0f;
    public ForceMode forceMode;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
  void OnCollisionEnter(Collision coll)
    {

        if (coll.collider.gameObject.tag == "ground" || coll.collider.gameObject.tag == "block" || coll.collider.gameObject.tag == "groundMtn")
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
            {
                if (col.GetComponent<Rigidbody>() != null)
                {
                    col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
                }
            }
        }

    }

















    /*
        public float power = 10.0F;
        public float radius = 5.0F;
        // Use this for initialization
        void Start()
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);


            }


        }
    */
}
