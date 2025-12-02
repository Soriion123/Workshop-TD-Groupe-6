using UnityEngine;

public class Ground_Enemy : MonoBehaviour
{
    public Transform target;
}
public interface ISlowable
{
    void ModifySpeed(float multiplier);
}
