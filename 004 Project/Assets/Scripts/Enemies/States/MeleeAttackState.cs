using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    AnimationToAttackCheck attackCheck;

    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    private D_MeleeAttackState stateData;

    public float AttackDamage => stateData.attackDamage; // 공격력 프로퍼티
    public Vector2 KnockbackAngle => stateData.knockbackAngle; // 넉백 각도 프로퍼티
    public float KnockbackStrength => stateData.knockbackStrength; // 넉백 강도 프로퍼티
    public int FacingDirection => Movement.FacingDirection;

    public MeleeAttackState(Entity entity, MonsterStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        attackCheck = entity.transform.GetComponentInChildren<AnimationToAttackCheck>();
        this.stateData = stateData;
    }


    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        attackCheck.TriggerAttack();        
    }
    public override void FinishAttack()
    {
        base.FinishAttack();
        attackCheck.FinishAttack();
    }

    //방패로직 다바꿔야함
    public override void HandleAttack(Collider2D collision)
    {
        base.HandleAttack(collision);
        if (collision != null)
        {
            IDamageable damageable = collision.GetComponentInChildren<IDamageable>();
            IKnockbackable knockbackable = collision.GetComponentInChildren<IKnockbackable>();

            DefensiveWeapon defensiveWeapon = collision.GetComponentInChildren<DefensiveWeapon>();
            ShieldWeaponHitboxToWeapon hitdox = collision.GetComponentInChildren<ShieldWeaponHitboxToWeapon>();
            //hitbox.isDefending 이거 변경할 수 잇으면 변경, 패링 성공 시 패링 키 홀드하고 있으면 어떻게 할지 고민하기.
            //defensiveWeapon을 가능하면 안가져오면 가장 베스트
            if (defensiveWeapon != null && hitdox.isDefending && IsShieldBlockingAttack(collision.transform, defensiveWeapon.transform, defensiveWeapon.GetPlayerShieldState().Movement.FacingDirection))
            {
                Debug.Log("방패로 막음!");
                hitdox.isDefending = false;
                defensiveWeapon.CheckShield(entity.gameObject, AttackDamage, KnockbackAngle, KnockbackStrength, FacingDirection);
                return;
            }
            else
            {
                if (damageable != null)
                {
                    damageable.Damage(stateData.attackDamage);
                }


                if (knockbackable != null)
                {
                    knockbackable.Knockback(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection);
                }
            }
        }
    }
    private bool IsShieldBlockingAttack(Transform playerTransform, Transform shieldTransform, int playerFacingDirection)
    {
        Vector2 attackDirection = entity.transform.position - playerTransform.position;
        Vector2 shieldDirection = playerFacingDirection == 1 ? Vector2.right : Vector2.left; // Assuming the shield faces up

        // Calculate the angle between the attack direction and the shield direction
        float angle = Vector2.Angle(attackDirection, shieldDirection);

        // Define a blocking angle threshold
        float blockingAngle = 90f; // Example: 45 degrees

        return angle <= blockingAngle;
    }
}
