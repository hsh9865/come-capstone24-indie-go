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

    public float AttackDamage => stateData.attackDamage; // ���ݷ� ������Ƽ
    public Vector2 KnockbackAngle => stateData.knockbackAngle; // �˹� ���� ������Ƽ
    public float KnockbackStrength => stateData.knockbackStrength; // �˹� ���� ������Ƽ
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

    //���з��� �ٹٲ����
    public override void HandleAttack(Collider2D collision)
    {
        base.HandleAttack(collision);
        if (collision != null)
        {
            IDamageable damageable = collision.GetComponentInChildren<IDamageable>();
            IKnockbackable knockbackable = collision.GetComponentInChildren<IKnockbackable>();

            DefensiveWeapon defensiveWeapon = collision.GetComponentInChildren<DefensiveWeapon>();
            ShieldWeaponHitboxToWeapon hitdox = collision.GetComponentInChildren<ShieldWeaponHitboxToWeapon>();
            //hitbox.isDefending �̰� ������ �� ������ ����, �и� ���� �� �и� Ű Ȧ���ϰ� ������ ��� ���� �����ϱ�.
            //defensiveWeapon�� �����ϸ� �Ȱ������� ���� ����Ʈ
            if (defensiveWeapon != null && hitdox.isDefending && IsShieldBlockingAttack(collision.transform, defensiveWeapon.transform, defensiveWeapon.GetPlayerShieldState().Movement.FacingDirection))
            {
                Debug.Log("���з� ����!");
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