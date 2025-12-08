using UnityEngine;

public class Mecha_AbilityManager : MonoBehaviour
{
    public AbilityType currentAbility = AbilityType.None;

    public SlowAbility slowAbility;
    public AOE_Ability aoeAbility;
    public AutoDeath_Ability autoDeathAbility;
    public A_Jump teleportAbility;

    private void Start()
    {
        RefreshAbilities();
    }

    public void SetAbility(AbilityType newAbility)
    {
        currentAbility = newAbility;
        RefreshAbilities();
    }

    // ✅ À appeler quand une ability est utilisée
    public void ConsumeAbility()
    {
        currentAbility = AbilityType.None;
        RefreshAbilities();
    }

    private void RefreshAbilities()
    {
        // Désactive tout
        slowAbility.enabled = false;
        aoeAbility.enabled = false;
        autoDeathAbility.enabled = false;
        teleportAbility.enabled = false;

        // Active seulement l’ability actuelle
        switch (currentAbility)
        {
            case AbilityType.Slow:
                slowAbility.enabled = true;
                break;

            case AbilityType.AOE:
                aoeAbility.enabled = true;
                break;

            case AbilityType.AutoDeath:
                autoDeathAbility.enabled = true;
                break;

            case AbilityType.Teleport:
                teleportAbility.enabled = true;
                break;
        }
    }
}
