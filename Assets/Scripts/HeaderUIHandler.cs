using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeaderUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreValue;

    public void UpdateScoreValue(int value)
    {
        ScoreValue.text = value.ToString();
    }
}
