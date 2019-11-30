using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    public GameObject go;
    Vector3 currentMovement = new Vector3(0, 0, 0);
    bool shaking = false;
    int directionX = 1;
    int directionY = 1;
    int directionZ = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            shakeCamera();
            go.transform.Rotate(currentMovement);
        }
        else
        {
            go.transform.rotation = Quaternion.Euler(75,0,0);
        }
    }

    void shakeCamera(){
        
        if(directionX == 1)
        {
            if(currentMovement.x <0.55 )
            {
                currentMovement.x += 0.1f;
            }
            else
            {
                directionX = -1;
            }
        }
        else
        {
            if (currentMovement.x > -0.5)
            {
                currentMovement.x -= 0.1f;
            }
            else
            {
                directionX = 1;
            }
        }
        
        if (directionY == 1)
        {
            if (currentMovement.y < 0.35)
            {
                currentMovement.y += 0.05f;
            }
            else
            {
                directionY = -1;
            }
        }
        else
        {
            if (currentMovement.y > -0.3)
            {
                currentMovement.y -= 0.05f;
            }
            else
            {
                directionY = 1;
            }
        }


        if (directionZ == 1)
        {
            if (currentMovement.z < 0.35)
            {
                currentMovement.z += 0.05f;
            }
            else
            {
                directionZ = -1;
            }
        }
        else
        {
            if (currentMovement.z > -0.3)
            {
                currentMovement.z -= 0.05f;
            }
            else
            {
                directionZ = 1;
            }
        }
    }
}
