using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Joystick joystick;
    public ParticleSystem CoinParticle;

    public float speed = 40f;
    public float speedforward = 40f;
    private float _lastFrameFingerPointX, _moveFactorX;
    public float MoveFactorX => _moveFactorX;
    public float maxSwerveAmount = 1f;

    public static bool startbool = false;

    public GameObject JoystickObj;
    public GameObject fevereffect;
    public GameObject winpanel,losepanel;
    public GameObject HandWood;
    public GameObject LevelCount;
    public GameObject touchlocked;

    public GameObject Confetti1, Confetti2, Confetti3;

    public TextMeshProUGUI cointext;
    public TextMeshProUGUI cointextfinal;

    public static int coinvalue;
    public static int coinvalueround;

    public Animator CameraAnim;
    public Animator PlayerAnim;

    void Start()
    {
        Time.timeScale = 1f;
        JoystickObj.SetActive(true);
        rb = GetComponent<Rigidbody>();
        coinvalue = PlayerPrefs.GetInt("Coin");
    }
    private void Update()
    {
        PlayerPrefs.SetInt("Coin", coinvalue);
        cointext.text = coinvalue.ToString();
        cointextfinal.text = coinvalueround.ToString();
        if (startbool == true)
        {
            transform.Translate(new Vector3(0, 0, speedforward) * speedforward * Time.deltaTime);
            if (gameObject.transform.position.x < -18.4f)
            {
                this.gameObject.transform.position = new Vector3(-18.4f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (gameObject.transform.position.x > 18.4f)
            {
                this.gameObject.transform.position = new Vector3(18.4f, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            if (gameObject.transform.position.z > 830)
            {
                this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 830);
            }
            if (Input.GetMouseButtonDown(0))
            {
                _lastFrameFingerPointX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                _moveFactorX = Input.mousePosition.x - _lastFrameFingerPointX;
                _lastFrameFingerPointX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _moveFactorX = 0;
            }
            float swerveAmount = Time.deltaTime * speed * MoveFactorX;
            swerveAmount = Mathf.Clamp(value: swerveAmount, min: -maxSwerveAmount, maxSwerveAmount);
            transform.Translate(x: swerveAmount, y: 0, z: 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "3-2")
        {
            StartCoroutine(FeverDelay());
            CameraAnim.SetBool("3-2", true);
        }
        if (other.gameObject.tag == "2-1")
        {
            StartCoroutine(FeverDelay());
            CameraAnim.SetBool("2-1", true);
        }
        if (other.gameObject.tag == "Coin")
        {
            CoinParticle.Play();
            coinvalue += 1;
            coinvalueround += 1;
            if (MenuManager.vibrat == 0)
            {
                Haptic.MediumTaptic();
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Saw")
        {
            touchlocked.SetActive(true);
            startbool = false;
            StartCoroutine(LoseDelay());
            PlayerAnim.SetBool("Dead", true);
            HandWood.SetActive(false);
            LevelCount.SetActive(false);
        }
        if (other.gameObject.tag == "Final")
        {
            touchlocked.SetActive(true);      
            HandWood.SetActive(false);
            LevelCount.SetActive(false);
            PlayerAnim.SetBool("Dance", true);
            CameraAnim.SetBool("Dance", true);
            StartCoroutine(WinDelay());
        }
    }

    #region Coroutin's
    public IEnumerator WinDelay()
    {
        Confetti1.SetActive(true);
        Confetti2.SetActive(true);
        Confetti3.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        startbool = false;
        yield return new WaitForSeconds(4.35f);
        touchlocked.SetActive(false);
        winpanel.SetActive(true);
    }
    public IEnumerator LoseDelay()
    {
        yield return new WaitForSeconds(2f);
        touchlocked.SetActive(false);
        losepanel.SetActive(true);
    }
    public IEnumerator FeverDelay()
    {
        yield return new WaitForSeconds(0.3f);
        fevereffect.SetActive(true);
        yield return new WaitForSeconds(2f);
        fevereffect.SetActive(false);
    }
    #endregion
}
