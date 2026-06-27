using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickManager : MonoBehaviour
{
    [SerializeField] private GameObject btnKick;
    [SerializeField] private float detectRadius = 1.5f;

    [Header("Goals")]
    [SerializeField] public Transform goal1;
    [SerializeField] public Transform goal2;

    private bool isBallNearby = false;

    public Collider NearbyBall { get; private set; }

    // void Start()
    // {
    //     if (btnKick != null)
    //         btnKick.SetActive(false);
    // }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius);

        bool foundBall = false;
        Collider ballCollider = null;

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Ball"))
            {
                foundBall = true;
                ballCollider = col;
                break;
            }
        }

        NearbyBall = ballCollider;

        if (foundBall != isBallNearby)
        {
            isBallNearby = foundBall;

            if (btnKick != null)
            {
                btnKick.SetActive(isBallNearby);

                if (isBallNearby)
                {
                    Kick kickScript = btnKick.GetComponentInChildren<Kick>();
                    if (kickScript != null)
                        kickScript.Setup(this);
                }
            }
        }
    }

    public Transform GetNearestGoal()
    {
        if (goal1 == null && goal2 == null) return null;
        if (goal1 == null) return goal2;
        if (goal2 == null) return goal1;

        float dist1 = Vector3.Distance(transform.position, goal1.position);
        float dist2 = Vector3.Distance(transform.position, goal2.position);

        return dist1 < dist2 ? goal1 : goal2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
