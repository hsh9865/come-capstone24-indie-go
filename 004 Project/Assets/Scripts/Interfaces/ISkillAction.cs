using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillAction
{
    void Initialize(Skill skill);
    void Enter();
    void Exit();
}