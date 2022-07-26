using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MoneyUiController : MonoBehaviour
{
        [SerializeField] private TMP_Text _moneysText;
        private void OnEnable()
        {
                PlayerObserverManeger.OnPlayerMoneysChanged += UpdateMoneysText;
        }

        private void OnDisable()
        {
                PlayerObserverManeger.OnPlayerMoneysChanged -= UpdateMoneysText;

        }

        private void UpdateMoneysText(int moneys)
        {
                _moneysText.text = moneys.ToString();
        }
}