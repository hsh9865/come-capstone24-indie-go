using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetup
{
    private Skill currentSkill;
    private SkillDataManager skillDataManager;
    private SkillManager skillManager;
    private GameObject player;

    public SkillSetup(GameObject player)
    {
        this.player = player;

        skillDataManager = new SkillDataManager();
        skillManager = new SkillManager();

        Initialize();
    }

    private void Initialize()
    {
        skillDataManager.Initialize();
        skillManager.LoadSkillData(skillDataManager);
        RegisterPlayerSkills();
        skillManager.InitializeSkill(SkillNames.SpearSkill); // 초기 스킬 설정
    }

    private void RegisterPlayerSkills()
    {
        currentSkill = player.GetComponent<Player>().skill;

        SpearGenerator spearGenerator = new SpearGenerator();
        ExplosionGenerator explosionGenerator = new ExplosionGenerator();

        skillManager.RegisterSkill(SkillNames.SpearSkill, currentSkill, new SkillInitializer<SpearSkill, SpearGenerator>(currentSkill, spearGenerator, SkillNames.SpearSkill));
        skillManager.RegisterSkill(SkillNames.ExplosionSkill, currentSkill, new SkillInitializer<ExplosionSkill, ExplosionGenerator>(currentSkill, explosionGenerator, SkillNames.ExplosionSkill));

        currentSkill.gameObject.name = SkillNames.SpearSkill;
    }

    public Skill GetCurrentSkill()
    {
        return currentSkill;
    }
}
