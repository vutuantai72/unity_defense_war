using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private GameObject patrolPath;
    public float speed = 5;
    float distanceTravelled;
    Rigidbody2D m_Rigidbody;
    PathCreator pathCreator;

    private void Awake()
    {
        pathCreator = patrolPath.GetComponentInChildren<PathCreator>();
    }
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            m_Rigidbody.MovePosition(pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop));
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
