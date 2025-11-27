using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Application.isMobilePlatform)
        {
            OnMobileClick();
        }
        else
        {
            OnDesktopClick();
        }
    }

    void OnMobileClick()
    {
        SceneManager.LoadScene(1);
    }

    void OnDesktopClick()
    {
        SceneManager.LoadScene(1);
    }
}
