using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToAttackCheck : MonoBehaviour
{
    private AttackState attackState;

    private bool isAlreadyHit;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log(collision.name);
        if ((1 << collision.gameObject.layer).Equals(LayerMasks.Player) && !isAlreadyHit)
        {        
            isAlreadyHit = true;
            attackState.HandleAttack(collision);
        }
    }
    
    public void TriggerAttack()
    {
        isAlreadyHit = false;
        gameObject.SetActive(true);
    }
    public void FinishAttack()
    {
        gameObject.SetActive(false);
    }

    public void InitializedAttackCheck(AttackState attackState)
    {
        this.attackState = attackState;
    }
}
