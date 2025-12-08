using UnityEngine;

public class A_Slow : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float slowMultiplier = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        // =========================
        // ✅ GROUND ENEMIES
        // =========================
        Ground_Enemy ground = other.GetComponent<Ground_Enemy>();
        if (ground != null)
        {
            if (other.TryGetComponent(out Ground_Fast fast))
                fast.speedF = fast.originalSpeed * slowMultiplier;

            else if (other.TryGetComponent(out Ground_Tank tank))
                tank.speedT = tank.originalSpeed * slowMultiplier;

            else if (other.TryGetComponent(out Ground_Basic basic))
                basic.speedB = basic.originalSpeed * slowMultiplier;

            return;
        }

        // =========================
        // ✅ FLYING ENEMIES
        // =========================
        Flying_Enemy flying = other.GetComponent<Flying_Enemy>();
        if (flying != null)
        {
            if (other.TryGetComponent(out Flying_Fast fastF))
                fastF.speed = fastF.originalSpeed * slowMultiplier;

            else if (other.TryGetComponent(out Flying_Tank tankF))
                tankF.speed = tankF.originalSpeed * slowMultiplier;

            else if (other.TryGetComponent(out Flying_Basic basicF))
                basicF.speed = basicF.originalSpeed * slowMultiplier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // =========================
        // ✅ RESTORE GROUND
        // =========================
        Ground_Enemy ground = other.GetComponent<Ground_Enemy>();
        if (ground != null)
        {
            if (other.TryGetComponent(out Ground_Fast fast))
                fast.speedF = fast.originalSpeed;

            else if (other.TryGetComponent(out Ground_Tank tank))
                tank.speedT = tank.originalSpeed;

            else if (other.TryGetComponent(out Ground_Basic basic))
                basic.speedB = basic.originalSpeed;

            return;
        }

        // =========================
        // ✅ RESTORE FLYING
        // =========================
        Flying_Enemy flying = other.GetComponent<Flying_Enemy>();
        if (flying != null)
        {
            if (other.TryGetComponent(out Flying_Fast fastF))
                fastF.speed = fastF.originalSpeed;

            else if (other.TryGetComponent(out Flying_Tank tankF))
                tankF.speed = tankF.originalSpeed;

            else if (other.TryGetComponent(out Flying_Basic basicF))
                basicF.speed = basicF.originalSpeed;
        }
    }
}
