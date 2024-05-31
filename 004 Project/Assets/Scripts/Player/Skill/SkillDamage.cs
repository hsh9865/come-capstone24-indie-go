using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : SkillComponent<SkillDamageData>, IAttackable
{
    private CollisionHandler collisionHandler;

    protected override void Awake()
    {
        base.Awake();
        // 스킬 오브젝트 또는 Prefab 오브젝트에서 CollisionHandler를 찾아 참조
        collisionHandler = gameObject.GetComponent<CollisionHandler>();
        Debug.Log(collisionHandler != null);
    }
    protected override void Start()
    {

        collisionHandler.OnColliderDetected += CheckAttack;
    }
    public void CheckAttack(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(currentSkillData.Damage);
        }
        /*
        if(collision.TryGetComponent(out IKnockbackable knockable))
        {
            knockable.Knockback(currentSkillData.)
        }
        */
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        collisionHandler.OnColliderDetected -= CheckAttack;
    }
}