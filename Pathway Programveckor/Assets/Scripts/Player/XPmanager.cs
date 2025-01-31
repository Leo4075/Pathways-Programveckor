using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPmanager : MonoBehaviour
{
    private int xp;
    private int xpCurrentLevel = 1;
    public int xpLevelUp = 20;

    // Start is called before the first frame update
    void Start()
    {
        xp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(xp >= xpLevelUp)
        {
            xp -= xpLevelUp;
            xpCurrentLevel += 1;
            Debug.Log("total xp = " + xp + " Current level " + xpCurrentLevel);
        }
    }

    public void MyrDeath()
    {
        xp += 10;
        Debug.Log("10 XP added, total xp = " + xp + " Current level " + xpCurrentLevel);
    }
}
