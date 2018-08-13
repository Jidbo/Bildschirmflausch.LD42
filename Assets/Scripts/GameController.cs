using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject UI;
    [SerializeField]
    GameObject ScoreText;
    [SerializeField]
    GameObject WasteNotification;
    [SerializeField]
    GameObject GameOverUI;
    [SerializeField]
    GameObject player;

    bool isDisplayed;
    float timeToDisplay;

    int gameScore = 0;

    enum GameState {PAUSED, PLAYING, GAMEOVER};

    GameState currentGameState = GameState.PLAYING;
    bool pausedPressed = false;

    public static GameController instance;
    public GameController()
    {
        instance = this;
    }

	private void Start() {
        Time.timeScale = 1;
        ScoreText.GetComponent<Text>().text = "Score: 0";
        isDisplayed = false;
        WasteNotification.SetActive(false);
        player.GetComponent<PlayerMovement>().ToggleControlledMovement(true);
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

        if (timeToDisplay > 0F) {
            timeToDisplay -= Time.deltaTime;
            if (!isDisplayed) {
                isDisplayed = true;
                WasteNotification.SetActive(true);
            }
            WasteNotification.transform.Rotate(0, 10, 0);
        } else {
            if (isDisplayed) {
                isDisplayed = false;
                WasteNotification.SetActive(false);
            }
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
        if (currentGameState != GameState.PLAYING) return;

        gameScore += toAdd;
        int spacePadding = 10 - gameScore.ToString().Length;
        ScoreText.GetComponent<Text>().text = "Score:" + new string(' ', spacePadding) + gameScore.ToString();
    }

    public float getCurrentScore() {
        return gameScore;
    }

    public void flashNotification() {
        timeToDisplay = 0.3F;
    }

    public void showGameOverUI() {
        player.GetComponent<PlayerMovement>().ToggleControlledMovement(false);
        UIController ui = GameOverUI.GetComponent<UIController>();
        currentGameState = GameState.GAMEOVER;
        if (ui != null) {
            ui.showGameOver();
        } else {
            Debug.Log("No GameOver UI specified in Gamecontroller");
        }
    }

    public AudioControl GetAudio() {
        return gameObject.GetComponent<AudioControl>();
    }
}