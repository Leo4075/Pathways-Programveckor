using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    public int level;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            level += 1;
            Debug.Log("level " + level);
        }
    }

    public void Button1()
    {
        if (level >= 1)
        {
            button2.interactable = true;
            button1.interactable = false;
            level -= 1;
            Debug.Log("level " + level);
        }
        
    }

    public void Button2()
    {
        if (level >= 1)
        {
            button3.interactable = true;
            button2.interactable = false;
            level -= 1;
            Debug.Log("level " + level);
        }
       
    }

    public void Button3()
    {
        if (level >= 1)
        {
            button3.interactable = false;
            level -= 1;
            Debug.Log("level " + level);
        }
    }
}
