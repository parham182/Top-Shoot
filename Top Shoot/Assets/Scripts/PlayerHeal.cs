using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerHeal : MonoBehaviour
{
    [SerializeField] float playerHeal = 3f;
    [SerializeField] TMP_Text youLoseText;

    public float playerHealnow = 0;
    public bool isalive = true;

    void Start()
    {
        youLoseText.enabled = false;
        playerHealnow = playerHeal;
    }

    void Update()
    {
        if (playerHealnow <= 0)
        {
            youLoseText.enabled = true;
            Invoke("exploadPlayer", 3f);
            isalive = false; 
        }
    }

    void exploadPlayer()
    {
        SceneManager.LoadScene(0);
    }
}
