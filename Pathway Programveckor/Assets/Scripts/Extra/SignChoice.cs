using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignChoice : MonoBehaviour
{
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Musen har klickat");
            //Konvertera pixelkoordinater till världskoordinater
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePosition);
            //Skapa en variabel som vet om det finns en Collider2D vid mousePosition
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);
            Debug.Log(collider);
            if (collider != null && collider.gameObject == gameObject)
            {
                Debug.Log("Du ska till" + sceneToLoad);
                //Ladda Scen
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
