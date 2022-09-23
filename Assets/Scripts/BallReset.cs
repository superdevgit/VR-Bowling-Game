using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallReset : MonoBehaviour
{
    public GameObject m_ball;
    public GameObject m_currentBall;
    public Transform m_ballSpawn;
    public GameObject m_pinSet;
    private GameObject curPinSet;

    public int score = 0;
    public int chance = 0;
    public GameObject scorePanel;
    public GameObject faulirePanel;
    public GameObject sparePanel;
    public GameObject strikePanel;
    public GameObject gutterPanel;

    private void Start()
    {
        curPinSet = Instantiate(m_pinSet, m_pinSet.transform.position, Quaternion.identity) as GameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ball") return;
        chance++;
        ResetGame();
    }

    public void ExitBtnClicked(string result) 
    {
        switch (result) 
        {
            case "score":
                scorePanel.SetActive(false);
                break;
            case "faulire":
                faulirePanel.SetActive(false);
                break;
            case "spare":
                sparePanel.SetActive(false);
                break;
            case "strike":
                strikePanel.SetActive(false);
                break;
            case "gutter":
                gutterPanel.SetActive(false);
                break;
        }
    }

    public void ResetPinSet() 
    {
        chance = 0;
        Destroy(curPinSet);
        curPinSet = Instantiate(m_pinSet, m_pinSet.transform.position, Quaternion.identity) as GameObject;
    }

    public void CheckPinScore() 
    {
        score = 0;
        GameObject[] pinList = GameObject.FindGameObjectsWithTag("Pin");

        foreach(GameObject g in pinList) 
        {
            if(g.transform.position.y < 0.19f) 
            {
                score++;
            }
        }
    }

    public void SetGutter(Vector3 ballPos) 
    {
        if(ballPos.z > 18.5f) 
        {
            gutterPanel.SetActive(true);
        }
    }

    public void ResetGame() 
    {
        Destroy(m_currentBall);
        m_currentBall = Instantiate(m_ball, m_ballSpawn.position, Quaternion.identity);

        if (chance == 1 && score == 10)
        {
            strikePanel.SetActive(true);
            ResetPinSet();
            return;
        }
        else if (chance == 2 && score < 10)
        {
            faulirePanel.SetActive(true);
            faulirePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "YourScore: " + score;
            ResetPinSet();
            return;
        }
        else if (chance == 2 && score == 10)
        {
            sparePanel.SetActive(true);
            ResetPinSet();
            return;
        }
        else if (chance == 1)
        {
            scorePanel.SetActive(true);
            scorePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "YourScore: " + score;
        }
    }
}
