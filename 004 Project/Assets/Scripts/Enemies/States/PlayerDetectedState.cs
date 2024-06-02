using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : MonsterState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSeses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;


    public PlayerDetectedState(Entity entity, MonsterStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = CollisionSeses.LedgeVertical;
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0.0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}