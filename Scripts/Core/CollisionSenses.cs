using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectName
{
    public const string GroundCheck = "GroundCheck";
}

public class LayerMasks
{
    public const int Player = 1 << 7;
    public const int Ground = 1 << 8; 
}

public class CollisionSenses : CoreComponent
{
    public Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    #region Check Transforms
    public Transform GroundCheck { get => groundCheck; set => groundCheck = value; }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public int WhatIsGround { get => whatIsGround; set => whatIsGround = value; }


    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private int whatIsGround = LayerMasks.Ground;
    //LayerMasks.Ground
    #endregion

    public bool Ground { get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround); }

    protected override void Awake()
    {
        groundCheck = GameObject.Find(GameObjectName.GroundCheck).transform;
    }

    
}
