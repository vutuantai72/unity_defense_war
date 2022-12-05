using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Core
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmosRadius = 0.15f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int? j = GetNextIndex(i);
                if (j == null)
                    break;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmosRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int? GetNextIndex(int? i)
        {
            if (i + 1 >= transform.childCount)
            {
                return null;
            }

            return i + 1;
        }

        public Vector2 GetWaypoint(int? i)
        {
            if (i != null)
                return transform.GetChild(i.Value).position;
            else
                return transform.GetChild(transform.childCount - 1).position;
        }
    }
}
