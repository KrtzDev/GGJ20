using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public enum GameState
{
    NONE,
    STARTGAME,
    BOSSFIGHTPHASE1,
    BOSSFIGHTPHASE2,
    BOSSFIGHTPHASE3,
    ENDGAME,
}    

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance = null;

    public GameState gameState = GameState.NONE;
    public PlayableDirector playableDirectorStart;
    public PlayableDirector playableDirectorEnd;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        gameState = GameState.STARTGAME;
        if(gameState == GameState.STARTGAME)
        {
            playableDirectorStart.Play();
        }
    }  

    public void BossFightPhase1()
    {
        gameState = GameState.BOSSFIGHTPHASE1;
    } 

    public void EndGame()
    {
        gameState = GameState.ENDGAME;
        playableDirectorEnd.Play();
    }

    public void ChangeToEndScreen()
    {
        SceneManagerx.instance.ChangeSceneCredits();
    }
}
