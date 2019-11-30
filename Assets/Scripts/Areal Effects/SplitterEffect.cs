using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterEffect : MonoBehaviour
{

    float splitDegrees = 30.0f;
    private const float deathMin = 10, deathMax = 30;
    public float deathTimer;

    // Start is called before the first frame update
    void Awake()
    {
        deathTimer = Random.Range(deathMin, deathMax);
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !other.GetComponent<Character.Projectile>().isCopy())
        {
            /*other.transform.Rotate(splitDegrees, 0, 0,Space.Self);
            var rotate1 = other.transform.rotation;
            other.transform.Rotate(-2 * splitDegrees, 0, 0, Space.Self);*/
            other.GetComponent<BulletCollision>().getOwner().GetComponent<Character.PlayerController>().Shoot(other.transform, -splitDegrees,true);
            other.GetComponent<Character.Projectile>().Deflect(-splitDegrees);
            other.GetComponent<Character.Projectile>().setIsCopy(true);
        }
    }
}
