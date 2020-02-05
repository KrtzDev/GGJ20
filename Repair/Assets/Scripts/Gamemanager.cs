using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public enum GameState
{
    NONE,
    STARTGAME,
    BOSSFIGHTPHASE1,
    BOSSFIGHTPHASE2,
    BOSSFIGHTPHASE3,
    DEATH,
    ENDGAME,
}    

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance = null;

    public GameState gameState = GameState.NONE;
    public PlayableDirector playableDirectorStart;
    public PlayableDirector playableDirectorEnd;
    public GameObject activationTrackStart;
    public GameObject activationTrackEnd;

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
        GetPlayableDirectors();
        gameState = GameState.STARTGAME;
        if (gameState == GameState.STARTGAME)
        {
            if (playableDirectorStart != null)
            {
                playableDirectorStart.Play();
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            GetPlayableDirectors();
            gameState = GameState.STARTGAME;
            if (gameState == GameState.STARTGAME)
            {
                if (playableDirectorStart != null)
                {
                    playableDirectorStart.Play();
                }
            }
        }
    }

    public void GetPlayableDirectors()
    {
        playableDirectorStart = GameObject.Find("StartTimeLine").GetComponent<PlayableDirector>();
        playableDirectorStart.SetGenericBinding(activationTrackStart,this.gameObject);
        playableDirectorEnd = GameObject.Find("EndTimeline").GetComponent<PlayableDirector>();
        playableDirectorEnd.SetGenericBinding(activationTrackEnd,this.gameObject);
    }

    public void StartState()
    {
        gameState = GameState.STARTGAME;
    }

    //public void BossFightPhase1()
    //{
    //    gameState = GameState.BOSSFIGHTPHASE1;
    //}

    public void EndGame()
    {
        gameState = GameState.ENDGAME;
        if (playableDirectorEnd != null)
        {
            playableDirectorEnd.Play();
        }
    }

    //public void ChangeToEndScreen()
    //{
    //    SceneManagerx.instance.ChangeSceneCredits();
    //}
}
