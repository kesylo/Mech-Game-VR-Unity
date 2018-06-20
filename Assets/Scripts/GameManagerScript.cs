using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public GameObject Gameover;
    bool GameHasEnded = false;
    public float timBtwRestart = 3f;

    public void EndGame()
    {
        //print("GameOver");
        if (GameHasEnded == false)
        {
            GameHasEnded = true;
            //cut all sounds
            Gameover.SetActive(true);

            // invoke calls a fonction with the guiven delay
            Invoke("restart", timBtwRestart);
        }
    }

    void restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
