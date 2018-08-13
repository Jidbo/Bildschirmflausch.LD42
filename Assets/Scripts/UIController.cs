using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void LoadSceneByIndex(int index) {
        Debug.Log("Load Scene: " + index);
        SceneManager.LoadScene(index);
    }

    public void showUI(bool show) {
        gameObject.SetActive(show);
		if (show) {
            GameController.instance.GetComponent<GameController>().Pause();
		} else {
            GameController.instance.GetComponent<GameController>().Play();
		}
    }

    public void showGameOver() {
        gameObject.SetActive(true);
    }
}