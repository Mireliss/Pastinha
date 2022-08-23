using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    //referencia do texto

    private void OnEnable()
    {
        PlayerObserverManeger.OnPlayerCoinsChanged += UpdateCoinText;
    }

    private void OnDisable()
    {
        PlayerObserverManeger.OnPlayerCoinsChanged -= UpdateCoinText;

    }
// atualiza o texto (chamar a função sempre que o valor das moedas mudar)
    private void UpdateCoinText(int coins)
    {
        _coinText.text = coins.ToString();
    }
}
