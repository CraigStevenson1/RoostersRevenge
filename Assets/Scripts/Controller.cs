using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using TMPro;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text timer;
    [SerializeField] TMP_Text enemyHP;
    [SerializeField] TMP_Text playerHP;
    [SerializeField] GameObject shopWindow;
    [SerializeField] PlayerUtils player;
    [SerializeField] Timer gameTimer;
    [SerializeField] LayerMask projectileLayer;
    [SerializeField] GameObject endWindow;
    [SerializeField] GameObject lackeyPrefab;
    [SerializeField] string passLevelKey;
    [SerializeField] float levelScoreScaling;
    [SerializeField] GameObject defeatWindow;
    [SerializeField] float scoreDecrease;
    [SerializeField] string levelpassKey;
    [SerializeField] AudioSource startSound;
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource punchSound;
    [SerializeField] GameObject lackeySpawnZone;
    // private int scoreCounter;
    private GameInteractions interactions;
    private bool enabled;
    private EnemyAttack atk;
    private Enemy enemy;
    string scoreKey;
    private string levelScoreKey;
    private Camera mainCam;
    string currentLackeyUpgradesKey;
    //private string passLevelKey;

    //public InputAction pos;

    private void Awake()
    {
        interactions = new GameInteractions();
    }
    private void OnEnable()
    {
        interactions.Enable();
    }

    private void OnDisable()
    {
        interactions.Disable();
    }
    void Start()
    {
        
        mainCam = Camera.main;
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        atk = GameObject.Find("Enemy").GetComponent<EnemyAttack>();
        //enemyScript = enemy.GetComponent<Enemy>();
        interactions.Touch.ScreenTap.started += ctx => updateScore(ctx);
        interactions.Touch.ScreenTap.canceled += ctx => releaseTap(ctx);
        //scoreCounter = 0;
        scoreKey = "score";
        levelScoreKey = "lscore";
        enabled = true;
        PlayerPrefs.SetFloat(levelScoreKey, 0);
        //passLevelKey = "leveltwo";
        PlayerPrefs.SetInt(passLevelKey, 0);
        currentLackeyUpgradesKey = "lackeyUpgrades";

        if (PlayerPrefs.GetInt(currentLackeyUpgradesKey) > 0)
        {
            createLackey();
        }

        unpause();
        startSound.Play();


    }
   
    private void updateScore(InputAction.CallbackContext ctx)
    {
        if (enabled)
        {
            //scoreCounter += 1;
            
            Debug.Log(interactions.Touch.TouchPosition.ReadValue<Vector2>());
            if (!hitObject(interactions.Touch.TouchPosition.ReadValue<Vector2>()))
            {
                //punchSound.Play();
                player.damageEnemy();
                increaseLevelScore(1 * levelScoreScaling);
                score.text = "Score: " + PlayerPrefs.GetFloat(levelScoreKey);

            }
        }
        //Debug.Log(interactions.Touch.TouchPosition.ReadValue<Vector2>());
        //hitObject(interactions.Touch.TouchPosition.ReadValue<Vector2>());
        //Debug.Log(pos.ReadValue<Vector3>());
        
      //  Debug.Log(scoreCounter.ToString());
    }

    private void releaseTap(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Tap rel");
         //click.started += ctx => updateScore(ctx); ;
    }

    private void Update()
    {
        if(!startSound.isPlaying && !levelMusic.isPlaying &&enabled)
        {
            levelMusic.Play();
            levelMusic.loop = true;
        }
       //Debug.Log(scoreCounter.ToString());
    }

    private bool hitObject(Vector2 touchCords)
    {

        Debug.Log(touchCords);
        Ray ray = mainCam.ScreenPointToRay(touchCords);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("is a hit");
            Projectile proj = hit.collider.GetComponent<Projectile>();
            PlayerUtils shop = hit.collider.GetComponent<PlayerUtils>();

            if (proj != null)
            {
                proj.reduceClickCount();

                return true;
            }

            else if (shop != null)
            {
                shopWindow.SetActive(true);
                pause();
                return true;
            }

            else if (hit.collider.tag == "shield")
            {
                shopWindow.SetActive(true);
                pause();
                return true;
            } 
        }
       

        return false;
    }

    public void updateTime(string time)
    {
        timer.text = time;
    }

    public void reduceEnemyHP(int amount)
    {
        enemy.reduceHP(amount);
    }
    public void updateEnemyHP(string hp)
    {
        if (hp == "defeated")
        {
            enemyHP.text = hp;
            endLevel(false);
         
        }
        else
        {
            enemyHP.text = hp;
        }
    }

    public void updateScoreAfterPurchase()
    {
        score.text = "Score: " + PlayerPrefs.GetFloat(levelScoreKey);
    }
    private void increaseScoreValue(float amount)
    {
        
        if (PlayerPrefs.HasKey(scoreKey))
        {
            float prevVal = PlayerPrefs.GetFloat(scoreKey);
            float newVal = prevVal + amount;
            PlayerPrefs.SetFloat(scoreKey, newVal);
        }
        else
        {
            PlayerPrefs.SetFloat(scoreKey, amount);
        }
        
    }

    private void increaseLevelScore(float amount)
    {
        float currentValue = PlayerPrefs.GetFloat(levelScoreKey);
        float newVal = currentValue + amount;
        PlayerPrefs.SetFloat(levelScoreKey, newVal);
    }
    public void updatePlayerHP(string hp)
    {
        playerHP.text = hp;
    }

    public void enableDamagePowerUp()
    {
        player.enableDamagePowerUp();
    }

    public void enableShieldPowerUp()
    {
        player.enableShieldPowerUp();
    }

    public void unoReverse()
    {
        atk.UnoReverseActivate();
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Return to main menu called");
    }

    private void pause()
    {
        Time.timeScale = 0;
        enabled = false;
    }

    public void unpause()
    {
        Time.timeScale = 1;
        enabled = true;
    }

    

    public void createLackey()
    {
        Vector2 cords = lackeySpawnZone.transform.position;
        /*playerPos.x -= 1.5f;
        playerPos.y -= 1.0f;*/
        Instantiate(lackeyPrefab, cords, Quaternion.identity);
    }

    public void endLevel(bool dead)
    {
        
        if (dead)
        {
            PlayerPrefs.SetFloat(levelScoreKey, 0);
            defeatWindow.SetActive(true);
            enabled = false;
            Time.timeScale = 0;
            //punishment();
        }

        else
        {
            PlayerPrefs.SetInt(levelpassKey, 0);
            increaseScoreValue(PlayerPrefs.GetFloat(levelScoreKey));
            PlayerPrefs.SetFloat(levelScoreKey, 0);
            //PlayerPrefs.SetInt(passLevelKey, 0);
            endWindow.SetActive(true);
            enabled = false;
            Time.timeScale = 0;
        }

        levelMusic.Stop();
        levelMusic.loop = false;
    }

    public void healPlayer(int amount)
    {
        player.healHP(amount);
    }

    public void playerDead()
    {
        endLevel(true);
    }

    private void punishment()
    {
        float currentScore = PlayerPrefs.GetFloat(scoreKey);
        float newScore = currentScore - scoreDecrease;
        PlayerPrefs.SetFloat(scoreKey, newScore);
    }

}
