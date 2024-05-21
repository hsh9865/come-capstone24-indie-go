using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject[] deathParticles;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);
    private ParticleManager particleManager;
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private Stats stats;


    private void Die()
    {
        foreach (var particles in deathParticles)
        {
            ParticleManager.StartParticles(particles);
        }
        core.transform.parent.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Stats.OnHealthZero -= Die;
        Stats.OnHealthZero += Die;
    }
    private void OnDisable()
    {
        Stats.OnHealthZero -= Die; ;
    }
}
