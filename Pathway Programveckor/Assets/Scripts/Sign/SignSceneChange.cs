using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignSceneChange : MonoBehaviour
{
    private bool isInsideRadius = false;

    // Start is called before the first frame update
    void Start()
    {
        isInsideRadius = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInsideRadius == true && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Skylt A");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered area");
        isInsideRadius = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited area");
        isInsideRadius = false;
    }
}
