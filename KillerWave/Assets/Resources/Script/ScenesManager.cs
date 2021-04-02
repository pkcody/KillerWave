using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    float gameTimer = 0;
    float[] endLevelTimer = { 30, 30, 45 };
    int currentSceneNumber = 0;
    bool gameEnding = false;

    Scenes scenes;
    public enum Scenes
    {
        bootUp,
        title,
        shop,
        level01,
        level02,
        level03,
        gameOver
    }
    void Update()
    {
        if (currentSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }
        GameTimer();
    }
    void GetScene()
    {
        scenes = (Scenes)currentSceneNumber;
    }
    void GameTimer()
    {
        switch (scenes)
        {
            case Scenes.level01 : case Scenes.level02 : case Scenes.level03:
            {
                if (gameTimer < endLevelTimer[currentSceneNumber - 3])
                {
                    //if level has not completed
                    gameTimer += Time.deltaTime;
                }
                else
                {
                    //if level is completed
                    if (!gameEnding)
                    {
                        gameEnding = true;
                        if (SceneManager.GetActiveScene().name != "level03")
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().LevelEnds = true;
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().GameCompleted = true;
                        }
                        Invoke("NextLevel", 4);
                    }
                }
                break;
            }
        }
    }
    public void ResetScene()
    {
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
    }

    void NextLevel()
    {
        gameEnding = false;
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }

    public void GameOver()
    {
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayerScore);
        SceneManager.LoadScene("gameOver");
    }
    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }
}
