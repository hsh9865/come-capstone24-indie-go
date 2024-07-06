using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator baseAnimator { get; protected set; }
    public Animator weaponAnimator { get; protected set; }

    protected CharacterStats<PlayerStatsData> playerStats;
    protected PlayerAttackState attackState;
    protected PlayerShieldState shieldState { get; private set; }
    protected AggressiveWeaponHitboxToWeapon aggressiveWeaponHitboxToWeapon;
    protected ShieldWeaponHitboxToWeapon shieldWeaponHitboxToWeapon;
    protected BaseAnimationToWeapon weaponAnimationToWeapon;


    public PlayerShieldState GetPlayerShieldState()
    {
        return shieldState;
    }

    protected virtual void Start()
    {
        playerStats = transform.root.GetComponentInChildren<PlayerStats>(); 
        if (playerStats == null)
        {
            Debug.LogError("CharacterStats 컴포넌트를 찾을 수 없습니다.");
        }
        baseAnimator = transform.Find("Base").GetComponent<Animator>();     //GetComponentInChildren<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        aggressiveWeaponHitboxToWeapon = transform.GetComponentInChildren<AggressiveWeaponHitboxToWeapon>();

        shieldWeaponHitboxToWeapon = transform.GetComponentInChildren<ShieldWeaponHitboxToWeapon>();
        weaponAnimationToWeapon = transform.GetComponentInChildren<BaseAnimationToWeapon>();
        gameObject.SetActive(false);

    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        baseAnimator.SetBool("Active", true);
        weaponAnimator.SetBool("Active", true);
    }


    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("Active", false);
        weaponAnimator.SetBool("Active", false);

        gameObject.SetActive(false);
    }




    public void InitializeAttackWeapon(PlayerAttackState state)
    {
        attackState = state;
    }
    public void InitializeShieldWeapon(PlayerShieldState state)
    {
        shieldState = state;
    }
    public float GetCurrentAnimationLength()
    {
        return baseAnimator.GetCurrentAnimatorStateInfo(0).length;
    }

    public virtual void AnimationActionTrigger()
    {
    }


    public virtual void AnimationFinishTrigger()
    {
    }
}
