using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    [HideInInspector]
    public float timeLeft;
    public TMPro.TextMeshProUGUI timer;
    public TMPro.TextMeshProUGUI loseText;
    private bool isCoroutineExecuting = false;
    private RabbitMovement rabmov;
    public change_sprite spr;
    public GameObject rabbit;

    private void Awake()
    {
        StartCoroutine(ExecuteAfterTime(18f));
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 5f;
        timer.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        loseText.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        rabmov = rabbit.GetComponent<RabbitMovement>();
        spr = GetComponent<change_sprite>();
    }

    // Update is called once per frame
    // use rabmov.isHidden to check for hidden rabbit to avoid lose condition
    void Update()
    {
        if (!rabmov.isHidden && spr.isBroken)
        {
            Debug.Log("is hidden and timer activate");
            timer.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            timeLeft -= Time.deltaTime;
            string tStr = "Time Remaining: " + timeLeft.ToString("F0");
            if (timeLeft > 0)
            {
                timer.text = tStr;
            }
            else
            {
                timer.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
                loseText.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                loseText.text = "You've been caught!";
            }

        }
        else {
            timeLeft = 5f;
        }
    }
        IEnumerator ExecuteAfterTime(float time)
        {
            if (isCoroutineExecuting)
                yield break;

            if (timeLeft == 0)
            {
                isCoroutineExecuting = true;
                yield return new WaitForSeconds(time);
                SceneManager.LoadScene("LoseScene");
            }

                isCoroutineExecuting = false;
        }
}
