using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private Transform originalTarget;
    private Coroutine followCoroutine;

    void Start()
    {
        if (target != null)
        {
            originalTarget = target;
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
        }
    }

    public void FollowBallTemporary(Transform tempTarget, float duration)
    {
        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
        }
        followCoroutine = StartCoroutine(FollowTemporaryCoroutine(tempTarget, duration));
    }

    private IEnumerator FollowTemporaryCoroutine(Transform tempTarget, float duration)
    {
        target = tempTarget;
        yield return new WaitForSeconds(duration);
        target = originalTarget;
    }
}
