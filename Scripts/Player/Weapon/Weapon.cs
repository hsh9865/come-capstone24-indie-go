using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator baseAnimator;
    protected Animator weaponAnimator;

    protected PlayerAttackState state;

    protected int attackCounter;


    protected virtual void Start()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();     //GetComponentInChildren<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();


        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if(attackCounter >= 3)
        {
            attackCounter = 0;
        }

        baseAnimator.SetBool("Attack", true);
        weaponAnimator.SetBool("Attack", true);

        baseAnimator.SetInteger("AttackCounter", attackCounter);
        weaponAnimator.SetInteger("AttackCounter", attackCounter);
    }
    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("Attack", false);
        weaponAnimator.SetBool("Attack", false);

        attackCounter++;

        gameObject.SetActive(false);
    }

    public virtual void AnimationActionTrigger() { }

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }
    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }
}
