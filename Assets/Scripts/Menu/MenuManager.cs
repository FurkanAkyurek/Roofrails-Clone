using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject SettingsButtonOff;
    public GameObject volumeOff, volumeOn;
    public GameObject vibOff, vibOn;
    public GameObject LevelCount;
    public GameObject Tutorial;
    public Animator PlayerAnim;
    public GameObject wood;
    public Animator NameAnim;
    public static int vibrat = 1, soundon=1;
    void Start()
    {
        vibrat = PlayerPrefs.GetInt("Vibrat");
        soundon = PlayerPrefs.GetInt("Soundon");
        StartCoroutine(TutorialDelay());   
    }
    void Update()
    {
        #region Vibration&Sound
        if (vibrat==0)
        {
            vibOn.SetActive(true);
            vibOff.SetActive(false);
        }
        else
        {
            vibOn.SetActive(false);
            vibOff.SetActive(true);
        }
        if(soundon==0)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
        }
        else
        {
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }
        #endregion
    }
    public IEnumerator TutorialDelay()
    {
        Tutorial.SetActive(true);
        yield return new WaitForSeconds(1.18f);
    }
    public IEnumerator GameObjectDelay()
    {
        yield return new WaitForSeconds(0.3f);
        wood.SetActive(true);
    }
    #region Play
    public void Play()
    {
        StartCoroutine(GameObjectDelay());
        PlayerAnim.SetBool("Run", true);
        CharacterMovement.startbool = true;
        Time.timeScale = 1f;
        NameAnim.SetBool("Name", true);
        Tutorial.SetActive(false);
        StartCoroutine(PlayDelay());   
    }
    public IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(1f);
        LevelCount.SetActive(true);
    }
    #endregion
    #region Settings
    public void Settings()
    {
        SettingsPanel.SetActive(true);
        SettingsButtonOff.SetActive(true);
    }
    public void SettingsClose()
    {
        SettingsPanel.SetActive(false);
        SettingsButtonOff.SetActive(false);
    }
    #endregion
    #region Vibration&Sound
    public void VolumeOn()
    {
        soundon = 1;
        volumeOn.SetActive(false);
        volumeOff.SetActive(true);

    }
    public void VolumeOff()
    {
        soundon = 0;
        volumeOn.SetActive(true);
        volumeOff.SetActive(false);
    }
    public void VibrationOn()
    {
        vibOn.SetActive(false);
        vibOff.SetActive(true);
        vibrat = 1;
    }
    public void VibrationOff()
    {
        vibrat = 0;
        vibOn.SetActive(true);
        vibOff.SetActive(false);
    }
    #endregion
}
