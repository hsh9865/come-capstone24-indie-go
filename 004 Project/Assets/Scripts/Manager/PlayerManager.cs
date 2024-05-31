using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    private SkillSetup skillSetup;

    public void Initialize()
    {
        CreatePlayer();
        SetSkills();
    }

    private void CreatePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            player = GameManager.Resource.Instantiate("Player");



        Camera.main.gameObject.GetComponent<MainCameraController>().SetPlayer(player);
        // 플레이어 초기화 로직
    }

    private void SetSkills()
    {
        skillSetup = new SkillSetup(player);
    }

    public Skill GetCurrentSkill()
    {
        return skillSetup.GetCurrentSkill();
    }

    // 기타 플레이어 관련 관리 로직
}
