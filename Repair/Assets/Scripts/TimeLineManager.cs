using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
   public void SetStartState()
    {
        Gamemanager.instance.gameState = GameState.BOSSFIGHTPHASE1;
    }

    public void SetEndState()
    {
        SceneManagerx.instance.ChangeSceneCredits();
    }
}
