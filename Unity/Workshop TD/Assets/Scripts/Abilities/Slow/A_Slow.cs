using UnityEngine;

public class A_Slow : MonoBehaviour
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


        // On vérifie si c'est un Flying_Enemy
        Flying_Enemy ennemy = other.GetComponent<Flying_Enemy>();
        if (enemy == null) return;

        // On vérifie si le script possède une variable speed + originalSpeed
        if (other.TryGetComponent(out Flying_Fast fastF))
        {
            fastF.speed = fastF.originalSpeed * slowMultiplier;
        }
        else if (other.TryGetComponent(out Flying_Fast tank))
        {
            tank.speed = tank.originalSpeed * slowMultiplier;
        }
        else if (other.TryGetComponent(out Flying_Basic basic))
        {
            basic.speed = basic.originalSpeed * slowMultiplier;
        }
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
