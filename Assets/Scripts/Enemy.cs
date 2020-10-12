using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int target = 0;
    [SerializeField] private Transform waypointObject;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float navigationUpdate;
    [SerializeField] private Vector2[] waypoints;
    [SerializeField] private float waitTime = 2.5f;
    [SerializeField] private int healthPoints = 100;
    [SerializeField] private int rewardAmt;

    private Animator anim;
    private Collider2D enemyCollider;
    private Transform enemy;
    private bool isDead = false;
    private float navigationTime = 0;



    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    private void Awake()
    {
        waypoints = new Vector2[waypointObject.childCount];
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        GameManager.Instance.RegisterEnemy(this);
        for (int i = 0; i < waypointObject.transform.childCount; i++)
        {
            waypoints[i] = waypointObject.transform.GetChild(i).position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null && !isDead)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                if (target < waypoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, waypoints[target], navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {

            StartCoroutine(waitAndUpdateTarget());
            ;
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnregisterEnemy(this);
            GameManager.Instance.isWaveOver();
        }

        else if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log(gameObject + "was hit by projectile");
            Projectile newP = collision.GetComponent<Projectile>();
            EnemyHit(newP.AttackStrength);
            Destroy(collision.gameObject);
        }
    }


    public void EnemyHit(int hitpoints)
    {
        if (healthPoints - hitpoints > 0)
        {

            healthPoints -= hitpoints;
            anim.Play("Hurt");

            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
            // call hurt animation
        }
        else
        {
            // call die animation
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        anim.SetTrigger("didDie");
        GameManager.Instance.TotalKilled += 1;
        GameManager.Instance.addMoney(rewardAmt);
        GameManager.Instance.isWaveOver();
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
    }

    IEnumerator waitAndUpdateTarget()
    {
        yield return new WaitForSeconds(waitTime);
        target += 1;

    }
}
