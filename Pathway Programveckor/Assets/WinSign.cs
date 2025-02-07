using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSign : MonoBehaviour
{
    public bool isPlayerTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerTouching == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("WinningScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerTouching = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerTouching = false;
    }
}
