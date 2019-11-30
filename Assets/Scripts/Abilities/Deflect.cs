using UnityEngine;

public class Deflect : Ability
{
    public GameObject Shield;
        
    public override bool Cancel()
    {
        return false;
    }

    public override bool isActive()
    {
        return false;
    }

    protected override bool useAbility()
    {
        Shield.SetActive(true);
        var bullets = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var t in bullets)
        {
            if(t.GetComponent<BulletCollision>().getOwner() != gameObject)
            {
                var dist = Vector3.Distance(gameObject.transform.position, t.transform.position);
                if (dist < 2)
                {
                    t.GetComponent<BulletCollision>().getOwner().GetComponent<Character.PlayerController>().RemoveBullet(t);
                    t.GetComponent<BulletCollision>().SetOwner(gameObject);
                    t.transform.rotation = _playerController.gameObject.transform.rotation;
                    t.GetComponent<Character.Projectile>().clearFuture();
                    t.GetComponent<Character.Projectile>().Deflect();
                }
                else if(dist < 4)
                {
                    //we need animations to be clear about what happened
                    Debug.Log("DESTROYED BY DEFLECT");
                    t.GetComponent<BulletCollision>().getOwner().GetComponent<Character.PlayerController>().RemoveBullet(t);
                    Destroy(t);
                }
            }
        }
        Invoke(nameof(DisableShield),2);
        return true;
    }

    private void DisableShield()
    {
        Shield.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        keyDown = true;
    }

  
}
