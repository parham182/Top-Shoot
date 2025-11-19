using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public JoyStick joystick;
    public float speed = 5f;

    [Header("Clamp Settings")]
    public Vector2 minPosition = new Vector2(-5f, -3f); // پایین-چپ
    public Vector2 maxPosition = new Vector2(5f, 3f);   // بالا-راست

    void Update()
    {
        // حرکت از جوی‌استیک
        Vector2 move = joystick.Direction * speed * Time.deltaTime;
        transform.Translate(move);

        // محدود کردن موقعیت پلیر
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(pos.y, minPosition.y, maxPosition.y);
        transform.position = pos;
    }
}
