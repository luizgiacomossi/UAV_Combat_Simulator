using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody projectileRb;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // destroy projectile if it goes out of bounds
        if(transform.position.y < -60f)
        {
            Destroy(gameObject);
        }
        if(transform.position.y > 60f)
        {
            Destroy(gameObject);
        }
        if(transform.position.x < -60f)
        {
            Destroy(gameObject);
        }
        if(transform.position.x > 60f)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
        // if the other is kamikaze, destroy it
        if(other.gameObject.CompareTag("Kamikaze")){
            Destroy(other.gameObject);
        }
    }
}
