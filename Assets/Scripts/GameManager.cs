using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [Header("Money")]
    public static int actualMoney;

    [Header("UI")]

    [SerializeField]public TMP_Text moneyTest;
    [SerializeField] public TMP_Text pileText;

    [SerializeField] public TMP_Text fpsText;
    [SerializeField] private float fps;

    public GameObject colorPanel;

    [Header("Character Color")]
    [SerializeField] public static Color playerColor = Color.white;
    [SerializeField] public string[] colorsBuyed;
    [SerializeField] public int colorsBuyedIndex;
    [SerializeField] public Transform buttonColorSelect;
    [SerializeField] public Material playerColorMaterial;

    void Start()
    {
        actualMoney = 3;

        colorsBuyed = new string[7];

        for (int i = 0; i < 6; i++)
        {
            colorsBuyed[i] = null;
        }
        colorsBuyed[6] = "White";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(actualMoney);

        if(actualMoney < 0) actualMoney = 0;

        moneyTest.text = "Money: " + actualMoney + " coin(s)";
        pileText.text = "Pile: " + PileHandler.instance.itemList.Count + "/" + PileHandler.maxPileCapacity;

        playerColorMaterial.color = playerColor;

        Application.targetFrameRate = 60;

        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = "FPS: " + fps;
    }

    public void BuyPileCapacity()
    {
        if (actualMoney > 0)
        {
            PileHandler.maxPileCapacity++;
            actualMoney--;
        }   
        Debug.Log(PileHandler.maxPileCapacity);
    }

    public void OpenColorPane()
    {
        colorPanel.SetActive(true);
    }

    public void CloseColorPane()
    {
        colorPanel.SetActive(false);
    }

    public void BuyColor(string color)
    {
        bool haveColor = false;
        foreach (string i in colorsBuyed)
        {
            if(i == color) haveColor = true;
        }
        if(haveColor)
        {
            switch (color)
            {
                case "Blue":
                    playerColor = Color.blue;
                    break;
                case "Yellow":
                    playerColor = Color.yellow;
                    break;
                case "Red":
                    playerColor = Color.red;
                    break;
                case "Black":
                    playerColor = Color.black;
                    break;
                case "Pink":
                    playerColor = Color.magenta;
                    break;
                case "Green":
                    playerColor = Color.green;
                    break;
                case "White":
                    playerColor = Color.white;
                    break;
            }
        } else
        {
            if (actualMoney > 0)
            {
                buttonColorSelect.GetComponentInChildren<TextMeshProUGUI>().text = color;
                colorsBuyed[colorsBuyedIndex] = color;
                actualMoney--;
                colorsBuyedIndex++;
            }
        }
    }

    public void SetButton(Transform button)
    {
        buttonColorSelect = button;
    }
}
