using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    private GameObject _owner;
    public bool hit = false;

    public void SetOwner(GameObject player)
    {
        _owner = player;
    }

    public GameObject getOwner()
    {
        return _owner;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.CompareTag("BouncyWall"))
        {
            gameObject.GetComponent<Character.Projectile>().bounce();
        }*/
        //Remove yourself out of the owner and Destroy yourself - You hit something that destroys you
        if (other.CompareTag("Wall"))
        {
            _owner.GetComponent<Character.PlayerController>().RemoveBullet(gameObject);
            Destroy(gameObject);
        }
        //Remove yourself out of the owner and destroy yourself and the Hit Object - You hit something that you can destroy
        else if (other.CompareTag("Destroyable"))
        {
            Destroy(other.gameObject);
            _owner.GetComponent<Character.PlayerController>().RemoveBullet(gameObject);
            Destroy(gameObject);
        }
        //Remove yourself out of the Owner and call a hit event on the other Player, then Destroy yourself - You hit the enemy
        else if (other.GetComponentInParent<Character.PlayerController>())
        {
            if (!hit && other.CompareTag("Player") && other.gameObject.GetComponentInParent<Character.PlayerController>().player != _owner.gameObject.GetComponentInParent<Character.PlayerController>().player)
            {
                hit = true;
                other.GetComponentInParent<Character.PlayerController>().Hit();
                _owner.GetComponent<Character.PlayerController>().RemoveBullet(gameObject);   
            }
        }
    }

    void Update()
    {
        if (hit) Destroy(gameObject);
    }
}
