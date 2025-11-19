using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // گرفتن دوربین اصلی
    }

    void Update()
    {
        if (!IsVisible())
        {
            Destroy(gameObject); // اگر خارج از دید بود، حذف شود
        }
    }

    bool IsVisible()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        // بررسی اینکه نقطه داخل محدوده 0 تا 1 در محور X و Y است
        bool onScreen = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                        viewportPos.y >= 0 && viewportPos.y <= 1 &&
                        viewportPos.z > 0; // داخل جلوی دوربین باشد

        return onScreen;
    }
}
