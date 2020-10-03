using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreensManager : MonoBehaviour {

    public static ScreensManager Instance;

    private GameObject currentSceen;

    public GameObject endScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    public Button lengthButton;
    public Button strengthButton;
    public Button offlineButton;

    public Text gameScreenMoney;
    public Text lengthCostText;
    public Text lengthValueText;
    public Text strengthCostText;
    public Text strengthValueText;
    public Text offlineCostText;
    public Text offlineValueText;
    public Text endScreenMoney;
    public Text returnScreenMoney;

    private int gameCount;

	void Awake ()
    {
        if (ScreensManager.Instance)
            Destroy(base.gameObject);
        else
            ScreensManager.Instance = this;

        currentSceen = mainScreen;
	}
	
    void Start()
    {
        CheckIdles();
        UpdateTexts();
    }

    public void ChangeScreen(Screens screen)
    {
        currentSceen.SetActive(false);
        switch (screen)
        {
            case Screens.MAIN:
                currentSceen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;

            case Screens.GAME:
                currentSceen = gameScreen;
                gameCount++;
                break;

            case Screens.END:
                currentSceen = endScreen;
                SetEndScreenMoney();
                break;

            case Screens.RETURN:
                currentSceen = returnScreen;
                SetReturnScreenMoney();
                break;
        }
        currentSceen.SetActive(true);
    }

    public void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + GameManager.Instance.totalGain;
    }

    public void SetReturnScreenMoney()
    {
        returnScreenMoney.text = "$" + GameManager.Instance.totalGain + " gained while waiting!";
    }

    public void UpdateTexts()
    {
        gameScreenMoney.text = "$" + GameManager.Instance.wallet;
        lengthCostText.text = "$" + GameManager.Instance.lengthCost;
        lengthValueText.text = -GameManager.Instance.length + "m";
        strengthCostText.text = "$" + GameManager.Instance.strengthCost;
        strengthValueText.text = GameManager.Instance.strength + " fishes.";
        offlineCostText.text = "$" + GameManager.Instance.offlineEarningsCost;
        offlineValueText.text = "$" + GameManager.Instance.offlineEarnings + "/min";
    }

    public void CheckIdles()
    {
        int lengthCost = GameManager.Instance.lengthCost;
        int strengthCost = GameManager.Instance.strengthCost;
        int offlineEarningsCost = GameManager.Instance.offlineEarningsCost;
        int wallet = GameManager.Instance.wallet;

        if (wallet < lengthCost)
            lengthButton.interactable = false;
        else
            lengthButton.interactable = true;

        if (wallet < strengthCost)
            strengthButton.interactable = false;
        else
            strengthButton.interactable = true;

        if (wallet < offlineEarningsCost)
            offlineButton.interactable = false;
        else
            offlineButton.interactable = true;
    }
}
