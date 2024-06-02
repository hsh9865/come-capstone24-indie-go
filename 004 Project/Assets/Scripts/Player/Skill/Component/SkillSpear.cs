using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpear : SkillComponent<SkillSpearData>
{
    public float GetSpearThrowSpeed()
    {
        return currentSkillData.ThrowSpeed;
    }
    public float GetSpearThrowDistance()
    {
        return currentSkillData.ThrowDistance;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

}
