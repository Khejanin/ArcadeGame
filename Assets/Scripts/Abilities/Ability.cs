using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected Character.PlayerController _playerController;
    protected CharacterController _characterController;
    public bool canCancel = false;
    public bool keyDown = false, keyHold = false, keyUp = false;

    protected float charges = 10;
    protected float maxCharges = 10;
    protected float chargeSeconds = 5;
    protected float chargeInc = 3;
    protected int chargeDec = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float getChargePercentage()
    {
        return charges / maxCharges;
    }

    protected void Initialize()
    {
        _playerController = GetComponent<Character.PlayerController>();
        _characterController = GetComponent<CharacterController>();
        //InvokeRepeating("addCharges", 1, 5);
    }

    protected void addCharges(float deltatime)
    {
        if(charges != maxCharges)
            charges = charges + chargeInc/chargeSeconds*deltatime;
        if (charges > maxCharges) charges = maxCharges;
    }

    public virtual bool canUse()
    {
        if (charges == maxCharges)
        {
            charges -= chargeDec;
            return true;
        }
        else return false;
    }

    public virtual bool use()
    {
        if (canUse())
        {
            useAbility();
            return true;
        }
        return false;
    }

    protected void Update()
    {
        addCharges(Time.deltaTime);
    }

    protected abstract bool useAbility();

    public abstract bool Cancel();

    public abstract bool isActive();

    public virtual void reset()
    {
        charges = maxCharges;
    }
}
