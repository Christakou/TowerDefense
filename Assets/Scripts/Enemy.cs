using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int target = 0;
    [SerializeField] private Transform waypointObject;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float navigationUpdate;
    [SerializeField] private Vector2[] waypoints;
    [SerializeField] private float waitTime = 2.5f;

    private Transform enemy;
    private float navigationTime = 0;

    private void Awake()
    {
        waypoints = new Vector2[waypointObject.childCount];
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i< waypointObject.transform.childCount; i++)
        {
            waypoints[i] = waypointObject.transform.GetChild(i).position;
        }
       
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null) {
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
        Debug.Log("collided: " + collision);
        if (collision.gameObject.tag == "Checkpoint")
        {

            StartCoroutine(waitAndUpdateTarget());
;        }
        else if (collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.removeEnemyFromScreen();
            Destroy(gameObject);
        }
    }

    IEnumerator waitAndUpdateTarget()
    {
        yield return new WaitForSeconds(waitTime);
        target += 1;

    }
}
