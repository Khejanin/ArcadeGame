using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBullet : MonoBehaviour
{

    float deceleration;
    private const float deathMin = 10,deathMax = 30;
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
        if (other.CompareTag("Projectile"))
        {
            if (!other.GetComponent<Character.Projectile>().isFuture && !other.GetComponent<BulletCollision>().getOwner().GetComponent<Character.PlayerController>().HasFutureBullet())
            {
                other.GetComponent<Character.Projectile>().setFuture();
                other.GetComponent<Rigidbody>().drag = 15;
            }
        }
    }
}
