using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public int wavesSurvived = 0;
    public int totalDamageDone = 0;
    public int totalDamageTaken = 0;
    public int totalPointsEarned = 0;
    public int totalPointsSpent = 0;

    public void SurvivedWave()
    {
        wavesSurvived += 1;
    }

    public void AddDamageDone(int damage)
    {
        totalDamageDone += damage;
    }

    public void AddDamageTaken(int damage)
    {
        totalDamageTaken += damage;
    }

    public void AddPointsEarned(int points)
    {
        totalPointsEarned += points;
    }

    public void AddPointsSpent(int points)
    {
        totalPointsSpent += points;
    }
}
