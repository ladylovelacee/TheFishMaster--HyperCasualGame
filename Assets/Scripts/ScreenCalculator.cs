using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCalculator : MonoBehaviour
{
    #region Singleton class : ScreenCalculator 
    public static ScreenCalculator Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ScreenCalculating();
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    float screenWeight;

    public float ScreenWeight
    {
        get
        {
            return screenWeight;
        }
    }

    void ScreenCalculating()
    {
        screenWeight = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }
}
