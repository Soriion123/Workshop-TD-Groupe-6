using UnityEngine;

public class A_AutoDeath : MonoBehaviour
{
    [Header("AutoDeath Settings")]
    public float damage = 5000f;
    public float duration = 0.1f; // Temps très court, juste pour détecter

    private void OnEnable()
    {
        // Dès que la zone est activée → explosion
        Explode();
        Invoke(nameof(DisableZone), duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        // On vérifie si c'est un ennemi
        Ground_Enemy enemy = other.GetComponent<Ground_Enemy>();
        if (enemy == null) return;

        // On applique les dégâts selon le type réel de l’ennemi
        if (other.TryGetComponent(out Ground_Fast fast))
        {
            fast.TakeDamage(damage);
        }
        else if (other.TryGetComponent(out Ground_Tank tank))
        {
            tank.TakeDamage(damage);
        }
        else if (other.TryGetComponent(out Ground_Basic basic))
        {
            basic.TakeDamage(damage);
        }
        /*else if (other.TryGetComponent(out Ground_Boss boss))
        {
            boss.TakeDamage(damage);
        }*/
    }

    private void Explode()
    {
        // Tu peux ajouter ici :
        // - particules
        // - son
        // - screen shake
    }

    private void DisableZone()
    {
        gameObject.SetActive(false);
    }
}
