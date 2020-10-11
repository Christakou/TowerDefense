using UnityEngine;



public enum proType
{
    rock, arrow, fireballl
};
public class Projectile : MonoBehaviour
{
    [SerializeField] private int attackStrength;
    [SerializeField] private proType projectileType;


    public int AttackStrength
    {
        get
        {
            return attackStrength;
        }
    }

    public proType ProjectileType
    {
        get
        {
            return projectileType;
        }
    }
}
