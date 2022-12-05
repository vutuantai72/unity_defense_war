using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Core
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 0f;
        [SerializeField] float moveSpeed = 0.1f;

        Vector2 guardLocation;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int? currentWaypointIndex = 0;
        // Start is called before the first frame update
        private void Start()
        {
            guardLocation = transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            PatrolBehaviour();
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector2 nextPosition = guardLocation;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                if (currentWaypointIndex != null)
                    nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector2.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint <= waypointTolerance;
        }

        private Vector2 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
    }
}