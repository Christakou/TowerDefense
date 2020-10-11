using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRadius;
    [SerializeField] private Projectile projectile;
    private Enemy targetEnemy = null;
    private float attackCounter;
    private bool isAttacking = true;


    // Start is called before the first frame update
    void Start()
    {
        attackCounter = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null || targetEnemy.IsDead) {
            Enemy nearestEnemy = GetNearestEnemyInRange();
            Debug.Log(nearestEnemy);
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
            {
                targetEnemy = nearestEnemy;
            }
        }
        else
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else
            {
                isAttacking = false;
            }

            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }
        }


    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            Attack();
        }
    }

    public void Attack()
    {
        isAttacking = false;
        Projectile newProjectile = Instantiate(projectile);


        if (targetEnemy == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            newProjectile.transform.localPosition = transform.localPosition;
            StartCoroutine(MoveProjectile(newProjectile));
        }


    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while(getTargetDistance(targetEnemy) > 0.2f && projectile!= null && targetEnemy != null)
        {
            var dir = (targetEnemy.transform.localPosition - transform.localPosition).normalized;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }
        if (projectile != null || targetEnemy == null) {
            Destroy(projectile);
        }
    }


    private float getTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearestEnemyInRange();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach(Enemy enemy in GameManager.Instance.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }


    private Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach(Enemy enemy in GetEnemiesInRange())
        {
            if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance && !enemy.IsDead)
            {
                nearestEnemy = enemy;
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
            }
        }
        return nearestEnemy;
    }
}
