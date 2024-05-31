public abstract class SkillInitializer
{
    protected Skill skill;
    protected SkillGenerator generator;
    protected string skillName;

    public SkillInitializer(Skill skill, SkillGenerator generator, string skillName)
    {
        this.skill = skill;
        this.generator = generator;
        this.skillName = skillName;
    }

    public void Initialize(SkillDataEx data)
    {
        // 스킬 데이터를 설정
        generator.InitializeSkill(skill, data);
        
        // 구체화된 스킬을 등록하고 이벤트 할당
        RegisterEvents();
    }

    public abstract void RegisterEvents();
    public abstract void UnregisterEvents();
}

public class SkillInitializer<TSkill, TGenerator> : SkillInitializer
    where TSkill : ISkillAction, new()
    where TGenerator : SkillGenerator
{
    public SkillInitializer(Skill skill, TGenerator generator, string skillName)
        : base(skill, generator, skillName) { }

    public override void RegisterEvents()
    {
        TSkill concreteSkill = new TSkill();
        concreteSkill.Initialize(skill);
        skill.OnEnter += concreteSkill.Enter;
        skill.OnExit += concreteSkill.Exit;
    }

    public override void UnregisterEvents()
    {
        TSkill concreteSkill = new TSkill();
        skill.OnEnter -= concreteSkill.Enter;
        skill.OnExit -= concreteSkill.Exit;
    }
}
