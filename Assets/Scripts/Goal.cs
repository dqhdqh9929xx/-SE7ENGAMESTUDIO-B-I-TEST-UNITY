using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("Delay trước khi camera quay về nhân vật")]
    [SerializeField] private float cameraReturnDelay = 2f;
    
    [Header("VFX Ghi Bàn")]
    [SerializeField] private GameObject goalVfxPrefab;
    [SerializeField] private float vfxDestroyDelay = 3f;

    /// <summary>
    /// Khi bóng (tag "Ball") va chạm với Goal,
    /// thông báo MainCamera đợi 2s rồi quay về nhân vật.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log($"[Goal] Bóng '{collision.gameObject.name}' đã vào khung thành '{gameObject.name}'!");
            
            // Tạo VFX nếu đã gán prefab
            if (goalVfxPrefab != null)
            {
                Vector3 contactPoint = collision.GetContact(0).point;
                GameObject vfx = Instantiate(goalVfxPrefab, contactPoint, Quaternion.identity);
                Destroy(vfx, vfxDestroyDelay); // Tự động hủy VFX sau vài giây
                
                // log lại vị trí và xem vfx đã thực sự xuất hiện chưa
                Debug.Log($"[Goal] Đã tạo VFX '{vfx.name}' tại vị trí {contactPoint}");
            }
            
            MainCamera mainCam = FindObjectOfType<MainCamera>();
            if (mainCam != null)
            {
                mainCam.ReturnToOriginalTarget(cameraReturnDelay);
            }
        }
    }
}
