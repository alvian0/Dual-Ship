using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public InputField P1Name, P2Name;

    void Start()
    {
        if (PlayerPrefs.GetString("P1") == "" || PlayerPrefs.GetString("P2") == "")
        {
            PlayerPrefs.SetString("P1", "Player 1");
            PlayerPrefs.SetString("P2", "Player 2");
        }

        P1Name.text = PlayerPrefs.GetString("P1");
        P2Name.text = PlayerPrefs.GetString("P2");
    }

    void Update()
    {
        PlayerPrefs.SetString("P1", P1Name.text);
        PlayerPrefs.SetString("P2", P2Name.text);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
