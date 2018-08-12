using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField]
    GameObject UI;

    enum GameState {PAUSED, PLAYING};

    GameState currentGameState = GameState.PLAYING;
    bool pausedPressed = false;

    public static GameController instance;
    public GameController()
    {
        instance = this;
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
	}

    public void Pause() {
        currentGameState = GameState.PAUSED;
        Time.timeScale = 0;
    }

    public void Play() {
        Time.timeScale = 1;
        currentGameState = GameState.PLAYING;
    }
}