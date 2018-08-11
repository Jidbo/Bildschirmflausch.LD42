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
}