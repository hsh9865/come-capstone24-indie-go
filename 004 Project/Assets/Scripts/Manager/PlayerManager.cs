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
        player = GameObject.Find("Player");

        if (player == null)
            player = GameManager.Resource.Instantiate("Player");



        Camera.main.gameObject.GetComponent<MainCameraController>().SetPlayer(player);
    }

    private void SetSkills()
    {
        skillSetup = new SkillSetup(player);
    }

    public Skill GetCurrentSkill()
    {
        return skillSetup.GetCurrentSkill();
    }
}
