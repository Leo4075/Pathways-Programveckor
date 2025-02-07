using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPmanager : MonoBehaviour
{
    private int xp;
    private int xpCurrentLevel = 1;
    public int xpLevelUp = 20;
    public int coefficient = 10;
    public int initialXPCost = 20;
    public float xpMultiplier = 1.2f; // A multiplier to increase XP requirement progressively

    // Start is called before the first frame update
    void Start()
    {
        xp = 0;
        xpLevelUp = 20; // Starting XP needed for the first level-up
    }

    // Update is called once per frame
    void Update()
    {
        if (xp >= xpLevelUp)
        {
            xp -= xpLevelUp; // Deduct XP for leveling up
            xpCurrentLevel += 1; // Increase level
            Debug.Log("Total XP = " + xp + " | Current level: " + xpCurrentLevel + " | XP needed for next level: " + xpLevelUp);

            // Update XP required for the next level, with exponential growth
            xpLevelUp = Mathf.FloorToInt(RequiredXPIncrease()); // Round to the nearest integer
        }
    }

    // Increases XP required for the next level using exponential scaling
    public int RequiredXPIncrease()
    {
        // Exponential growth function for the XP required to level up
        return Mathf.FloorToInt(initialXPCost * Mathf.Pow(xpMultiplier, xpCurrentLevel));
    }

    // Function to add XP when something (e.g., an event) happens, like death
    public void MyrDeath()
    {
        xp += 10; // Add 10 XP when Myr dies
        Debug.Log("10 XP added, total XP = " + xp + " | Current level: " + xpCurrentLevel);
    }
}
