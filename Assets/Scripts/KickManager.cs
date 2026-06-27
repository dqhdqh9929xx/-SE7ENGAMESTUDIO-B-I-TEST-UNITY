using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickManager : MonoBehaviour
{
    [SerializeField] private GameObject btnKick;

    [SerializeField] private float detectRadius = 1.5f;

    private bool isBallNearby = false;

    void Start()
    {
        if (btnKick != null)
            btnKick.SetActive(false);
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius);

        bool foundBall = false;
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Ball"))
            {
                foundBall = true;
                break;
            }
        }

        if (foundBall != isBallNearby)
        {
            isBallNearby = foundBall;
            if (btnKick != null)
                btnKick.SetActive(isBallNearby);
        }
    }
}
