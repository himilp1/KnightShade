using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointsTracker : MonoBehaviour
{
    public int currentPoints;

    private PointsUI pointsUI;
    public GameObject HUD;

    void Start()
    {
        pointsUI = HUD.GetComponent<PointsUI>();
        currentPoints = 0;
    }

    public void AddPoints(int points)
    {
        //call this function after an enemy has died to recieve those enemies points
        Debug.Log("in addpoints");
        currentPoints += points;
        pointsUI.SetPoints(currentPoints);
    }

    public void SpendPoints(int points)
    {
        //call this function for when you spend your points on weapons or map exploration
        currentPoints -= points;
        pointsUI.SetPoints(currentPoints);
    }
}