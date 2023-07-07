using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI LevelCountText;
    public static int levelcount;
    public GameObject Lv1, Lv2, Lv3, Lv4;
    int switchcount;
    void Start()
    {
        levelcount = PlayerPrefs.GetInt("Level");
        switchcount = PlayerPrefs.GetInt("LevelSwitch");
    }
    void Update()
    {
        PlayerPrefs.SetInt("Level", levelcount);
        PlayerPrefs.SetInt("LevelSwitch", switchcount);

        LevelCountText.text = levelcount.ToString();

        if (levelcount == 0)
        {
            levelcount = 1;
        }
        if (switchcount == 0)
        {
            switchcount = 1;
        }

        switchlevel();
    }

    public void switchlevel()
    {
        switch (switchcount)
        {
            case 1:
                Lv1.SetActive(true);
                Lv2.SetActive(false);
                Lv3.SetActive(false);
                Lv4.SetActive(false);

                return;
            case 2:
                Lv1.SetActive(false);
                Lv2.SetActive(true);
                Lv3.SetActive(false);
                Lv4.SetActive(false);
                return;
            case 3:
                Lv1.SetActive(false);
                Lv2.SetActive(false);
                Lv3.SetActive(true);
                Lv4.SetActive(false);
                return;
            case 4:
                Lv1.SetActive(false);
                Lv2.SetActive(false);
                Lv3.SetActive(false);
                Lv4.SetActive(true);
                return;

            default:
                switchcount = 1;
                return;
        }
    }

    public void Restart()
    {
        CharacterMovement.coinvalueround = 0;
        SceneManager.LoadScene("Game");
    }
    public void NextLevel()
    {
        CharacterMovement.coinvalueround += CharacterMovement.coinvalue;
        CharacterMovement.coinvalueround = 0;
        levelcount++;
        switchcount++;
        CharacterMovement.startbool = false;
        SceneManager.LoadScene("Game");
    }
}
