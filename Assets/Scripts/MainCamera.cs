using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private Transform originalTarget;
    private Coroutine returnCoroutine;

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

    /// <summary>
    /// Camera bắt đầu follow bóng. Không có timer — camera sẽ theo bóng
    /// cho đến khi ReturnToOriginalTarget() được gọi (bởi Goal khi bóng vào khung thành).
    /// </summary>
    public void FollowBall(Transform ballTransform)
    {
        // Nếu đang có coroutine quay về target cũ, hủy nó
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
        }

        target = ballTransform;
        Debug.Log($"[MainCamera] Bắt đầu follow bóng: {ballTransform.name}");
    }

    /// <summary>
    /// Được gọi bởi Goal khi bóng va chạm khung thành.
    /// Đợi delay giây rồi chuyển camera về target cũ (Jammo).
    /// </summary>
    public void ReturnToOriginalTarget(float delay)
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
        returnCoroutine = StartCoroutine(ReturnAfterDelay(delay));
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        target = originalTarget;
        returnCoroutine = null;
        Debug.Log("[MainCamera] Camera quay về nhân vật.");
    }
}
