using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoKick : MonoBehaviour
{
    [Header("Balls (5 quả bóng trong scene)")]
    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;
    public GameObject ball4;
    public GameObject ball5;

    [Header("Nhân vật Jammo")]
    public GameObject jammo;

    [Header("2 Goal trong scene")]
    public GameObject goal1;
    public GameObject goal2;

    [Header("Nút AutoKick (Canvas UI)")]
    public Button btnAutoKick;

    // Start is called before the first frame update
    void Start()
    {
        // Đăng ký sự kiện khi bấm nút AutoKick
        if (btnAutoKick != null)
        {
            btnAutoKick.onClick.AddListener(OnClickedBtnAutoKick);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Hàm được gọi khi bấm nút AutoKick.
    /// Tìm quả bóng xa Jammo nhất, sau đó đá nó vào goal gần quả bóng đó nhất.
    /// </summary>
    public void OnClickedBtnAutoKick()
    {
        // --- Bước 1: Kiểm tra độ dài khoảng cách 5 quả bóng so với Jammo ---
        GameObject[] balls = new GameObject[] { ball1, ball2, ball3, ball4, ball5 };

        GameObject farthestBall = null;
        float maxDistance = -1f;

        foreach (GameObject ball in balls)
        {
            if (ball == null) continue;

            float dist = Vector3.Distance(jammo.transform.position, ball.transform.position);
            Debug.Log($"[AutoKick] Khoảng cách từ Jammo đến {ball.name}: {dist:F2}m");

            if (dist > maxDistance)
            {
                maxDistance = dist;
                farthestBall = ball;
            }
        }

        if (farthestBall == null)
        {
            Debug.LogWarning("[AutoKick] Không tìm thấy quả bóng hợp lệ!");
            return;
        }

        Debug.Log($"[AutoKick] Quả bóng xa Jammo nhất: {farthestBall.name} (khoảng cách: {maxDistance:F2}m)");

        // --- Bước 2: Tìm goal gần quả bóng xa nhất ---
        GameObject nearestGoal = null;
        float minGoalDistance = float.MaxValue;

        GameObject[] goals = new GameObject[] { goal1, goal2 };
        foreach (GameObject goal in goals)
        {
            if (goal == null) continue;

            float distToGoal = Vector3.Distance(farthestBall.transform.position, goal.transform.position);
            Debug.Log($"[AutoKick] Khoảng cách từ {farthestBall.name} đến {goal.name}: {distToGoal:F2}m");

            if (distToGoal < minGoalDistance)
            {
                minGoalDistance = distToGoal;
                nearestGoal = goal;
            }
        }

        if (nearestGoal == null)
        {
            Debug.LogWarning("[AutoKick] Không tìm thấy goal hợp lệ!");
            return;
        }

        Debug.Log($"[AutoKick] Goal gần quả bóng nhất: {nearestGoal.name} (khoảng cách: {minGoalDistance:F2}m)");

        // --- Bước 3: Đá quả bóng xa nhất vào goal gần nhất ---
        KickBallToGoal(farthestBall, nearestGoal);
    }

    /// <summary>
    /// Đá quả bóng bay về phía goal bằng Rigidbody.
    /// </summary>
    private void KickBallToGoal(GameObject ball, GameObject goal)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning($"[AutoKick] {ball.name} không có Rigidbody, không thể đá!");
            return;
        }

        // Tính hướng từ bóng đến goal
        Vector3 direction = (goal.transform.position - ball.transform.position).normalized;

        // Đặt lại vận tốc cũ trước khi tác dụng lực mới
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Tác dụng lực đẩy bóng về phía goal
        float kickForce = 10f;
        rb.AddForce(direction * kickForce, ForceMode.Impulse);

        Debug.Log($"[AutoKick] Đá {ball.name} về phía {goal.name} với lực {kickForce}!");
    }
}
