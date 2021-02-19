using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Mechanic")]
    public GameObject FusionForms;
    public GameObject DefusionEffect;
    public AudioSource FusionSFX, HurtSFX;
    public float ScoreEarn = 100;
    public float Score = 0;
    public float multiple = 1;
    public float ComboTimer = 7.5f;

    [Range(1, 100)]
    public float Hp1 = 100;
    [Range(1, 100)]
    public float Hp2 = 100;

    [Header("Menu GUI")]
    public GameObject PauseScreen;
    public GameObject InGameUI;
    public GameObject GameOverScreen;
    public Text ScoreBar, ScoreMultipleIndicator, EndScreenScore;
    public Image Hpbar1, HpBar2;

    bool IsPaused = false;
    float ComboTime;
    bool IsInCombo;
    GameObject p1, p2;
    float HpLimit, HplImit2;

    void Start()
    {
        Time.timeScale = 1f;
        HpLimit = Hp1;
        HplImit2 = Hp2;
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");
    }

    void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 9);
        Physics2D.IgnoreLayerCollision(11, 10);

        Hpbar1.fillAmount = Hp1 / HpLimit;
        HpBar2.fillAmount = Hp2 / HplImit2;

        ScoreBar.text = Score.ToString();

        EndScreenScore.text = "Score\n" + Score.ToString();

        if (multiple > 1)
        {
            ScoreMultipleIndicator.text = "Score " + multiple.ToString() + "X";
        }

        else ScoreMultipleIndicator.text = "Score ";

        if (Hp1 <= 0 || Hp2 <= 0)
        {
            DestryPlayer();
            GameOverScreen.SetActive(true);

            if (PlayerPrefs.GetFloat("HS") <= Score)
            {
                PlayerPrefs.SetFloat("HS", Score);
            }
        }

        if (IsInCombo)
        {
            ComboTime -= Time.deltaTime;

            if (ComboTime <= 0)
            {
                multiple = 1;
                IsInCombo = false;
            }
        }

        PauseNavigations();
    }

    void PauseNavigations()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOverScreen.activeInHierarchy)
        {
            if (IsPaused)
            {
                UnPause();
            }

            else
            {
                Pause();
            }
        }
    }

    public void DestryPlayer()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");

        Destroy(p1);
        Destroy(p2);
    }

    public void Summon()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1");
        p2 = GameObject.FindGameObjectWithTag("Player2");

        float XPos = (p1.transform.position.x + p2.transform.position.x) / 2;
        float YPos = (p1.transform.position.y + p2.transform.position.y) / 2;

        FusionSFX.Play();
        Instantiate(DefusionEffect, new Vector2(XPos, YPos), Quaternion.identity);
        Instantiate(FusionForms, new Vector2(XPos, YPos), Quaternion.identity);
    }

    public void GetScore()
    {
        if (IsInCombo)
        {
            multiple++;
            ComboTime = ComboTimer;
        }

        else
        {
            ComboTime = ComboTimer;
            IsInCombo = true;
        }

        Score += ScoreEarn * multiple;
    }

    public void Pause()
    {
        IsPaused = true;
        InGameUI.SetActive(false);
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        IsPaused = false;
        InGameUI.SetActive(true);
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
