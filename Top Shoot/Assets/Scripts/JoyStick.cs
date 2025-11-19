using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Joystick Setup")]
    public RectTransform background;  // پس‌زمینه جوی‌استیک
    public RectTransform handle;      // دکمه کوچک
    [Range(0.1f, 1f)]
    public float handleLimit = 1f;    // محدوده حرکت دسته (نسبت به شعاع)

    private Vector2 inputVector = Vector2.zero;
    private Camera cam;
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("JoyStick must be inside a Canvas!");
        }

        if (background == null)
            background = GetComponent<RectTransform>();

        cam = canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);  // دسته به جای لمس شده مستقیم برود
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, cam, out localPoint))
        {
            float radius = (background.sizeDelta.x / 2) * handleLimit;
            Vector2 clampedPos = Vector2.ClampMagnitude(localPoint, radius);

            handle.anchoredPosition = clampedPos;
            inputVector = clampedPos / radius;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public Vector2 Direction => inputVector;
    public float Horizontal => inputVector.x;
    public float Vertical => inputVector.y;
}
