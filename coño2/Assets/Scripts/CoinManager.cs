using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
     public TMP_Text  coinText;
    public int coinCount = 0;
    public int coinsToActivate = 10; // Número de monedas necesarias para activar el objeto
    public GameObject objectToActivate; // El objeto que se activará al recolectar las monedas necesarias

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinText();
        CheckCoins();
    }

    private void UpdateCoinText()
    {
        coinText.text = "Llaves: " + coinCount;
    }

    private void CheckCoins()
    {
        if (coinCount >= coinsToActivate)
        {
            objectToActivate.SetActive(true);
        }
    }
} 
