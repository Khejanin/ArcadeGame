using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed;
    public int index;
    public Conveyor[] conveyors;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVector = transform.position;
        newVector.z += speed * Time.deltaTime;
        transform.position = newVector;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "BouncyWallTop") {
            Vector3 newVector = conveyors[(index + 1) % conveyors.Length].transform.position;
            newVector.z -= 1.05f;
            transform.position = newVector;
        }
    }
}
