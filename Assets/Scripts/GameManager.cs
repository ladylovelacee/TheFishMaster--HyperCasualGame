using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton class: GameManager
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

         GetDatas();
        //PlayerPrefs.DeleteAll();
    }
    #endregion

    [HideInInspector] public int length;
    [HideInInspector] public int lengthCost;

    [HideInInspector] public int strength;
    [HideInInspector] public int strengthCost;

    [HideInInspector] public int offlineEarnings;
    [HideInInspector] public int offlineEarningsCost;

    [HideInInspector] public int wallet;
    [HideInInspector] public int totalGain;

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("Date", now.ToString());
            MonoBehaviour.print(now.ToString());
        }
        else
        {
            string @string = PlayerPrefs.GetString("Date", string.Empty);
            if (@string != string.Empty)
            {
                DateTime d = DateTime.Parse(@string);
                totalGain = (int)((DateTime.Now - d).TotalMinutes * offlineEarnings + 1.0);
                ScreensManager.Instance.ChangeScreen(Screens.RETURN);
            }
        }
    }

    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }

    public void BuyLength()
    {
        length -= 10;
        SpentMoney(lengthCost);
        lengthCost += (int)lengthCost / 2;

        PlayerPrefs.SetInt("lengthCost", lengthCost);
        PlayerPrefs.SetInt("length", -length);

        ScreensManager.Instance.ChangeScreen(Screens.MAIN);
    }

    public void BuyStrength()
    {
        strength++;
        SpentMoney(strengthCost);
        strengthCost += (int)strengthCost / 2;

        PlayerPrefs.SetInt("strengthCost", strengthCost);
        PlayerPrefs.SetInt("strength", strength);

        ScreensManager.Instance.ChangeScreen(Screens.MAIN);
    }

    public void BuyOfflineEarning()
    {
        offlineEarnings++;
        SpentMoney(offlineEarningsCost);
        offlineEarningsCost += (int)offlineEarningsCost / 2;

        PlayerPrefs.SetInt("offlineEarningsCost", offlineEarningsCost);
        PlayerPrefs.SetInt("offlineEarnings", offlineEarnings);

        ScreensManager.Instance.ChangeScreen(Screens.MAIN);
    }

    public void CollectMoney()
    {
        wallet += totalGain;
        PlayerPrefs.SetInt("wallet", wallet);
        ScreensManager.Instance.ChangeScreen(Screens.MAIN);
    }

    public void IncreaseTheCollectedMoney()
    {
        wallet += totalGain * 2;
        PlayerPrefs.SetInt("wallet", wallet);
        ScreensManager.Instance.ChangeScreen(Screens.MAIN);
    }

    void SpentMoney(int cost)
    {
        wallet -= cost;
        PlayerPrefs.SetInt("wallet", wallet);
    }

    void GetDatas()
    {
        length = -PlayerPrefs.GetInt("length", 30);
        lengthCost = PlayerPrefs.GetInt("lengthCost", 120);

        strength = PlayerPrefs.GetInt("strength", 3);
        strengthCost = PlayerPrefs.GetInt("strengthCost", 120);

        offlineEarnings = PlayerPrefs.GetInt("offlineEarnings", 3);
        offlineEarningsCost = PlayerPrefs.GetInt("offlineEarningsCost", 120);

        wallet = PlayerPrefs.GetInt("wallet", 0);
    }
}
