using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectile;
    public float speedProjectile;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision other) {
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    void ReadInput(){
        if(Input.GetKeyDown(KeyCode.Space)){
            // creates projectile
            Vector3 projectilePosition = new Vector3(transform.position.x, transform.position.y , transform.position.z + 2.5f);

            GameObject projectileClone = Instantiate(projectile, projectilePosition, projectile.transform.rotation);
            projectileClone.GetComponent<Rigidbody>().velocity = transform.forward * speedProjectile; 
        }
    }



    

}
