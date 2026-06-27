using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Kick : MonoBehaviour
{
    [Header("Kick Settings")]
    [SerializeField] private float kickForce = 15f;
    [SerializeField] private float kickUpwardAngle = 15f;  // độ cao khi bóng bay

    private KickManager kickManager;

    public void Setup(KickManager manager)
    {
        kickManager = manager;
    }

    public void OnKickButtonPressed()
    {
        // if (kickManager == null)
        // {
        //     Debug.LogWarning("Kick: KickManager chưa được gán!");
        //     return;
        // }

        Collider ballCollider = kickManager.NearbyBall;
        if (ballCollider == null)
        {
            Debug.LogWarning("Kick: Không tìm thấy quả bóng gần nhân vật!");
            return;
        }

        Rigidbody ballRb = ballCollider.GetComponent<Rigidbody>();
        // if (ballRb == null)
        // {
        //     Debug.LogWarning("Kick: Quả bóng không có Rigidbody!");
        //     return;
        // }

        Transform nearestGoal = kickManager.GetNearestGoal();
        // if (nearestGoal == null)
        // {
        //     Debug.LogWarning("Kick: Không tìm thấy goal!");
        //     return;
        // }

        Vector3 directionToGoal = (nearestGoal.position - ballCollider.transform.position).normalized;

        directionToGoal.y += Mathf.Tan(kickUpwardAngle * Mathf.Deg2Rad);
        directionToGoal.Normalize();

        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.AddForce(directionToGoal * kickForce, ForceMode.Impulse);

        Debug.Log($"Kick: Bóng bay về goal '{nearestGoal.name}'");

        MainCamera mainCam = FindObjectOfType<MainCamera>();
        if (mainCam != null)
        {
            mainCam.FollowBall(ballCollider.transform);
        }

        Destroy(ballCollider.gameObject, 2f);
    }
}
