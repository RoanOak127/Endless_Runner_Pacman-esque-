using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * This script controls the game flow. 
 * This is where the major variables that affect the whole game are created and saved
 * This is where the game pauses, restarts, and quits
 * This is also where the game goes to the next level
 */
public class GameControlScript : MonoBehaviour
{
    // initialize in the game editor
    [SerializeField] private int level;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Text timerText;
    [SerializeField] private Text levelText;
    [SerializeField] private int timer;

    //public variables manipulated in other scripts
    public float jmpIntensity;
    public bool isPaused = false;

    //these are in playerPrefs
    public int health;
    public float speed;
    public int numEnemies;
    public int numWalls;
    public int points = 0; //the game wanted this initialized?

    // text fields
    public Text healthText;
    public Text pointsText;

    private GameObject player;

    private void Start()
    {
        //find the player and makes sure they have gravity and their controller enabled
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<CharacterController>().enabled = true;

        //makes sure the pause panel isn't active and that time is flowing
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        

        // initialzies all the saved variables 
        if (PlayerPrefs.HasKey("PlayerHealth"))
            health = PlayerPrefs.GetInt("PlayerHealth");
        else
            PlayerPrefs.SetInt("PlayerHealth", health);

        if (PlayerPrefs.HasKey("PlayerSpeed"))
            speed = PlayerPrefs.GetFloat("PlayerSpeed");
        else
            PlayerPrefs.SetFloat("PlayerSpeed", speed);

        if (PlayerPrefs.HasKey("NumberEnemies"))
            numEnemies = PlayerPrefs.GetInt("NumberEnemies");
        else
            PlayerPrefs.SetInt("NumberEnemies", numEnemies);

        if (PlayerPrefs.HasKey("NumberWalls"))
            numWalls = PlayerPrefs.GetInt("NumberWalls");
        else
            PlayerPrefs.SetInt("NumberWalls", numWalls);

        if (PlayerPrefs.HasKey("Level"))
            level = PlayerPrefs.GetInt("Level");
        else
            PlayerPrefs.SetInt("Level", level);

        if (PlayerPrefs.HasKey("Points"))
            points = PlayerPrefs.GetInt("Points");
        else
            PlayerPrefs.SetInt("Points", points);


        //intializes the various texts on the screen
        //these are directly on the game instead of a panel because I want to see them still in the pause panel
        levelText.text = "Level " + level.ToString();
        timerText.text = timer.ToString();
        healthText.text = health.ToString();
        pointsText.text = "Points: " + points.ToString();
        
        //start's the timer countdown
        StartCoroutine("CountDown");
    }
    
    //each level only lasts however long the timer is
    //this waits 1 second before changing the time
    //both internally and for the player to see
    IEnumerator CountDown()
    {
        //while there's still time in the level
        //decrease time every second and display it
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            if(!isPaused)
                timer--;
            timerText.text = timer.ToString();
        }

        //once there is no more time//

        //disable the enemy movement script
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyController>().enabled = false;
        }

        //player can't fall to their death if jumping
        //or move
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<CharacterController>().enabled = false;

        //wait two seconds before starting the next level
        yield return new WaitForSeconds(2);

        ChangeLevel();

    }

    //changes the level of the game by reloading the same level
    //but with different stats
    private void ChangeLevel()
    {
        //returns speed to original for that level in case they hit a power down
        speed = PlayerPrefs.GetFloat("PlayerSpeed", speed); 
        
        //saves current stats
        PlayerPrefs.SetInt("PlayerHealth", health);
        PlayerPrefs.SetInt("Level", level+1);
        PlayerPrefs.SetInt("Points", points);

        //there are 5 different levels
        switch ((level+1)%5)
        {
            //level starting at 6 / every level a multiple of 5 +1
            case 1:
                //speed increases, walls and enemies gets reset to 1
                PlayerPrefs.SetFloat("PlayerSpeed", speed+3);
                PlayerPrefs.SetInt("NumberWalls", 1);
                PlayerPrefs.SetInt("NumberEnemies", 1);
                break;
            //level 2 (7, 12, etc)
            case 2:
                //same as the level next
            //level 3 (8, 13, etc)
            case 3:
                PlayerPrefs.SetInt("NumberEnemies", numEnemies + 1);
                PlayerPrefs.SetInt("NumberWalls", numWalls + 1);
                break;
            //level 4 (9, 14, etc) doesn't increase the enemies
            case 4:
                PlayerPrefs.SetInt("NumberEnemies", numEnemies);
                PlayerPrefs.SetInt("NumberWalls", numWalls+1);
                break;
            //level 5 (10, 15) doesn't increase the walls
            default:
                PlayerPrefs.SetInt("NumberEnemies", numEnemies+1);
                PlayerPrefs.SetInt("NumberWalls", numWalls);
                break;

        }
        //loads the same scene with new stats
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //check if player health is 0 or lower
    private void Update()
    {
        if (health <= 0)
            GameOver();
    }

    //ends the game
    //TODO pull up game over menu show current score, make a high a score and show it, ask if they want to replay the game
    public void GameOver()
    {
#if  UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //quits the application but not the editor

#endif
    }

    //deletes the saved stats 
    //TODO when making a high score do NOT delete here it should save throughout closure
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
        PlayerPrefs.DeleteKey("PlayerSpeed");
        PlayerPrefs.DeleteKey("NumberEnemies");
        PlayerPrefs.DeleteKey("NumberWalls");
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Points");
    }

    //pauses the game and stops time and pulls up pause panel
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    //sends pause panel back, starts time and resumes game
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    //restarts the game, since nothing gets saved until change level
    //rebuilding the level resets the stats to what it was when it was first loaded
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
