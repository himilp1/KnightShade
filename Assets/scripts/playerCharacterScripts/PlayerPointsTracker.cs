using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointsTracker : MonoBehaviour
{
    public int currentPoints;

    void Start()
    {
        currentPoints = 0;
    }

    public void AddPoints(int points)
    {
        //call this function after an enemy has died to recieve those enemies points
        Debug.Log("in addpoints");
        currentPoints += points;
    }

    public void SpendPoints(int points)
    {
        //call this function for when you spend your points on weapons or map exploration
        currentPoints -= points;
    }
}