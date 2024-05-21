using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_LookForPlayerState lookForPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }
    public E1_StunState stunState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
//    [SerializeField]
//    private D_StunState stunStateData;
//    [SerializeField]
//    private D_DeadState deadStateData;


    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform meleeAttackCheck;

    public override void Awake()
    {
        base.Awake();

        moveState = new E1_MoveState(this, stateMachine, "Move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "Idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "PlayerDetected", playerDetectedData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "Charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "SearchPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        //      stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
        //       deadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

        meleeAttackState.SetAattackCheck(meleeAttackCheck.GetComponent<AnimationToAttackCheck>());

    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }
}
