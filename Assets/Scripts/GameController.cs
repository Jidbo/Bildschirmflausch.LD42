using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject UI;
    [SerializeField]
    GameObject ScoreText;

    float lastTime = 0;
    int gameScore = 0;

    enum GameState {PAUSED, PLAYING};

    GameState currentGameState = GameState.PLAYING;
    bool pausedPressed = false;

    public static GameController instance;
    public GameController()
    {
        instance = this;
    }

	private void Start() {
        lastTime = Time.time;
        ScoreText.GetComponent<Text>().text = "Score: 0";
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Pause") > 0) {
            if (currentGameState == GameState.PLAYING && !pausedPressed) {
				UIController controller = UI.GetComponent<UIController>();
                if (controller != null) {
                    controller.showUI(true);
                } else {
                    Debug.Log("Cant find UI controller");
                }

            } else if (currentGameState == GameState.PAUSED && !pausedPressed) {
                
                UIController controller = UI.GetComponent<UIController>();
                if (controller != null) {
                    controller.showUI(false);
                } else {
                    Debug.Log("Cant find UI controller");
                }
            }

			pausedPressed = true;
        } else {
            pausedPressed = false;
        }

        updateScore((int) Time.deltaTime);    
	}

    public void Pause() {
        currentGameState = GameState.PAUSED;
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
        currentGameState = GameState.PLAYING;
    }

    public void updateScore(int toAdd) {
        gameScore += toAdd;
        int spacePadding = 10 - gameScore.ToString().Length;
        ScoreText.GetComponent<Text>().text = "Score:" + new string(' ', spacePadding) + gameScore.ToString();
    }

    public float getCurrentScore() {
        return gameScore;
    }
}