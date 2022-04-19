using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public bool isDead;
    public Rigidbody rb;
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        // get rb form parent
        rb = GetComponentInParent<Rigidbody>();
        explosion = rb.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            explosion.Play();
        }
    }

    void UpdateDestroyed()
    {
        isDead = true;
    }
}
