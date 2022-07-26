using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// esse seria o nosso Youtube com coisas do jogador.
public static class PlayerObserverManeger
{
    // esse vai ser o nosso canal para atualizações da quantidade de coisas do jogador.
    public static Action<int> OnPlayerCoinsChanged;
    
    // A segunda parte é como o player notifica seus inscritos que as moedas.
    public static void PlayerCoinsChanged(int value)
    {
        OnPlayerCoinsChanged?.Invoke(value);
    }
    
    // esse vai ser o nosso canal para atualizações da quantidade de coisas do jogador.
    public static Action<int> OnPlayerMoneysChanged;
    
    // A segunda parte é como o player notifica seus inscritos que as moedas.
    public static void PlayerMoneysChanged(int value)
    {
        OnPlayerMoneysChanged?.Invoke(value);
    }
}
