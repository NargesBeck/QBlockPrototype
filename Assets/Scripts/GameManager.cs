using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance  =  FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public BoardManager BoardManager;
    public TouchManager TouchManager;
    public HeaderUIHandler HeaderUIHandler;

    [HideInInspector] public Pattern CurrentlySelectedPattern;
    [HideInInspector] public Profile Profile = new Profile();
}