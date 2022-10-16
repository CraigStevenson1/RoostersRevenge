using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

using TMPro;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text timer;
    [SerializeField] TMP_Text enemyHP;
    [SerializeField] TMP_Text playerHP;
    [SerializeField] Enemy enemy;
    [SerializeField] PlayerUtils player;
    [SerializeField] Timer gameTimer;
    [SerializeField] LayerMask projectileLayer;
    [SerializeField] GameObject endWindow;
    private int scoreCounter;
    private GameInteractions interactions;
    private bool enabled;
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
        //enemyScript = enemy.GetComponent<Enemy>();
        interactions.Touch.ScreenTap.started += ctx => updateScore(ctx);
        interactions.Touch.ScreenTap.canceled += ctx => releaseTap(ctx);
        scoreCounter = 0;
        enabled = true;

    }

    private void updateScore(InputAction.CallbackContext ctx)
    {
        if (enabled)
        {
            scoreCounter += 1;
            score.text = "Score: " + scoreCounter.ToString();

            if (!hitObject(interactions.Touch.TouchPosition.ReadValue<Vector2>()))
            {
                player.damageEnemy();
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
       //Debug.Log(scoreCounter.ToString());
    }

    private bool hitObject(Vector2 touchCords)
    {
        Vector2 worldTouchCords = Camera.main.ScreenToWorldPoint(touchCords);

        
        Ray ray = Camera.main.ScreenPointToRay(touchCords);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("is a hit");
            Projectile proj = hit.collider.GetComponent<Projectile>();
            if (proj != null)
           {
                proj.reduceClickCount();

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
            increaseScoreValue(scoreCounter);
            endWindow.SetActive(true);
            enabled = false;
        }
        else
        {
            enemyHP.text = hp;
        }
    }

    private void increaseScoreValue(int amount)
    {
        string scoreKey = "score";
        if (PlayerPrefs.HasKey(scoreKey))
        {
            int prevVal = PlayerPrefs.GetInt(scoreKey);
            int newVal = prevVal + amount;
            PlayerPrefs.SetInt(scoreKey, newVal);
        }
        else
        {
            PlayerPrefs.SetInt(scoreKey, amount);
        }
        
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

    public void addTimerPowerUp(float amount)
    {
        gameTimer.addTime(amount);
    }

    public void returnToMainMenu()
    {
        
        Debug.Log("Return to main menu called");
    }




}
