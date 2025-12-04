using UnityEngine;

public class SlowZone : MonoBehaviour
{
    [Range(0.1f, 1f)]
    public float slowMultiplier = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est un Ground_Enemy
        Ground_Enemy enemy = other.GetComponent<Ground_Enemy>();
        if (enemy == null) return;

        // On vérifie si le script possède une variable speed + originalSpeed
        if (other.TryGetComponent(out Ground_Fast fast))
        {
            fast.speedF = fast.originalSpeed * slowMultiplier;
        }
        else if (other.TryGetComponent(out Ground_Tank tank))
        {
            tank.speedT = tank.originalSpeed * slowMultiplier;
        }
        else if (other.TryGetComponent(out Ground_Basic basic))
        {
            basic.speedB = basic.originalSpeed * slowMultiplier;
        }
        /*else if (other.TryGetComponent(out Ground_Boss boss))
        {
            boss.speed = boss.originalSpeed * slowMultiplier;
        }*/

        // ⚠️ Ajoute une ligne par type d’ennemi, si tu en as plusieurs
    }

    private void OnTriggerExit(Collider other)
    {
        Ground_Enemy enemy = other.GetComponent<Ground_Enemy>();
        if (enemy == null) return;

        if (other.TryGetComponent(out Ground_Fast fast))
        {
            fast.speedF = fast.originalSpeed;
        }
        else if (other.TryGetComponent(out Ground_Tank tank))
        {
            tank.speedT = tank.originalSpeed;
        }
        else if (other.TryGetComponent(out Ground_Basic basic))
        {
            basic.speedB = basic.originalSpeed;
        }
        /*else if (other.TryGetComponent(out Ground_Boss boss))
        {
            boss.speed = boss.originalSpeed;
        }*/
    }
}
