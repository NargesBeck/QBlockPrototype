using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Profile
{
    public int Score;

    public void ScoreAdder(int value)
    {
        Score += value;
        GameManager.Instance.HeaderUIHandler.UpdateScoreValue(Score);
    }
}
