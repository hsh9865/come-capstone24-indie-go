using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PlayerAnimStatesName
{
    public const string Idle = "Idle";
    public const string Move = "Move";
    public const string Jump = "jump";
    public const string Land = "Land";
    public const string InAir = "InAir";
    public const string Attack = "Attack";
}
public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    // 각종 STATE
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }


    [SerializeField]
    private PlayerData playerData;

    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    //public Transform DashDirectionIndicator { get; private set; }
    public BoxCollider2D PlayerCollider { get; private set; }
    //test
    public PlayerInventory Inventory { get; private set; }
    #endregion



    private Vector2 workspace;

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        playerData = new PlayerData();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, PlayerAnimStatesName.Idle);
        MoveState = new PlayerMoveState(this, StateMachine, playerData, PlayerAnimStatesName.Move);
        JumpState = new PlayerJumpState(this, StateMachine, playerData, PlayerAnimStatesName.InAir); //jump 아님
        LandState = new PlayerLandState(this, StateMachine, playerData, PlayerAnimStatesName.Land);
        InAirState = new PlayerInAirState(this, StateMachine, playerData, PlayerAnimStatesName.InAir);
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, PlayerAnimStatesName.Attack);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, PlayerAnimStatesName.Attack);

        Inventory = GetComponent<PlayerInventory>();

        PrimaryAttackState.SetWeapon(Inventory.weapon[(int)CombatInputs.primary]);
        //SecondaryAttackState.SetWeapon(Inventory.weapon[(int)CombatInputs.primary]);
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate(); //행동Comopnent관련
        StateMachine.CurrentState.LogicUpdate(); //상태State관련
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }


    public void SetColliderSize(float? width = null, float? height = null)
    {
        Vector2 size = PlayerCollider.size;
        Vector2 center = PlayerCollider.offset;

        // 너비 조정
        if (width.HasValue)
        {
            center.x += (width.Value - size.x) / 2;
            size.x = width.Value;
        }

        // 높이 조정
        if (height.HasValue)
        {
            center.y += (height.Value - size.y) / 2;
            size.y = height.Value;
        }

        PlayerCollider.size = size;
        PlayerCollider.offset = center;
    }

    // AnimationEvent
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

}
