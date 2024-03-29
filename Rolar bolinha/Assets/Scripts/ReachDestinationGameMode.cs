using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/GameModes/ReachDestination", fileName = "ReachDestinationGameModeSO")]
public class ReachDestinationGameMode : GameModeSO

{
    public bool useTimer;
    public float timeToLose;
    
    public override void UpdateGameState([Optional] int intValue, [Optional] float floarValue, [Optional] bool boolValue)
    {
        if (boolValue) GameState = GameState.Victory;
        if (useTimer && floarValue >= timeToLose) GameState = GameState.GameOver;
    }
}
