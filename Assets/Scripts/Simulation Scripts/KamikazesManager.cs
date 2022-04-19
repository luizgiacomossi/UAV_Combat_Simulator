using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazesManager : MonoBehaviour
{
    private GameObject[] kamikazeDronesObjects;
    private List<Rigidbody> listKamikazeDrones;
    private GameObject kamikazeDroneObject;
    public GameObject kamikazeDronePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // find objects with tag "Kamikaze"
        kamikazeDronesObjects = GameObject.FindGameObjectsWithTag("Kamikaze");
        // add all objects to the list
        foreach (GameObject kamikazeDrone in kamikazeDronesObjects)
        {
            listKamikazeDrones.Add(kamikazeDrone.GetComponent<Rigidbody>());
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check if kamikaze is destroyed
        CheckKamikazeDestroyed();

    }

    private void CheckKamikazeDestroyed()
    {
        // check if kamikaze is destroyed
        foreach (Rigidbody kamikazeDrone in listKamikazeDrones)
        {
            if (kamikazeDrone.gameObject.GetComponent<DroneManager>().isDestroyed)
            {
                // destroy kamikaze
                Destroy(kamikazeDrone.gameObject);
                // instantiate a new one
                kamikazeDroneObject = Instantiate(kamikazeDronePrefab, kamikazeDrone.transform.position, kamikazeDrone.transform.rotation);
                // add new kamikaze to the list
                listKamikazeDrones.Add(kamikazeDroneObject.GetComponent<Rigidbody>());

            }
        }
    }

}
