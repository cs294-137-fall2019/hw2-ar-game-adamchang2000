using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int[] board;
    public bool playerTurn;
    private int frameWaitBeforeOpp;

    public GameObject victoryText;
    public GameObject defeatText;
    public GameObject drawText;
    public GameObject restartText;

    public bool gameOver;
    private bool restartOffered;


    void Start()
    {
        board = new int[9];
        playerTurn = true;
        frameWaitBeforeOpp = 0;
        gameOver = false;
        restartOffered = false;
    }

    // Update is called once per frame
    // Update here controls CPU player
    void Update()
    {
        if (!gameOver)
        {
            if (!playerTurn && frameWaitBeforeOpp == 0)
            {
                System.Random rnd = new System.Random();
                while (true)
                {
                    int i = rnd.Next(0, 9);
                    if (board[i] == 0)
                    {
                        board[i] = 2;
                        transform.Find("button" + (i + 1)).gameObject.GetComponent<ARButton1>().placeObject(2);
                        playerTurn = true;
                        checkStatus();
                        break;
                    }
                } 
            }
            else if (frameWaitBeforeOpp > 0)
            {
                frameWaitBeforeOpp--;
            }
        } else if (!restartOffered)
        {
            offerRestart();
        } else
        {
            if (Input.touchCount > 0)
            {
                Debug.Log("Trying to reset scene");
                SceneManager.LoadScene("SampleScene");
            }


        }
    }

    public void playerButton(int buttonNum)
    {
        board[buttonNum - 1] = 1;
        playerTurn = false;
        frameWaitBeforeOpp = 35;
        checkStatus();
    }

    private void checkStatus()
    {
        

        int[,] possibleWins = { { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };

        //check for player win
        for (int i = 0; i < 8; i++)
        {
            int c = 0;
            for (int k = 0; k < 3; k++)
            {
                if (board[possibleWins[i, k]] == 1)
                {
                    c++;
                }
            }
            if (c == 3)
            {
                victory(1);
                return;
            }
        }

        //check for cpu win
        for (int i = 0; i < 8; i++)
        {
            int c = 0;
            for (int k = 0; k < 3; k++)
            {
                if (board[possibleWins[i, k]] == 2)
                {
                    c++;
                }
            }
            if (c == 3)
            {
                victory(2);
                return;
            }
        }

        bool full = true;
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == 0)
            {
                full = false;
                break;
            }
        }
        if (full)
        {
            draw();
            return;
        }


    }

    private float waitForRestart;

    private void draw()
    {
        gameOver = true;
        drawText.SetActive(true);
        waitForRestart = 2;
        offerRestart();
    }
    
    private void victory(int winner)
    {
        gameOver = true;
        if (winner == 1)
        {
            victoryText.SetActive(true);
        } else
        {
            defeatText.SetActive(true);
        }
        waitForRestart = 2;
        offerRestart();
    }

    private void offerRestart()
    {
        waitForRestart -= Time.deltaTime;
        if (waitForRestart < 0)
        {
            drawText.SetActive(false);
            victoryText.SetActive(false);
            defeatText.SetActive(false);
            restartText.SetActive(true);
            restartOffered = true;
        } 
    }

    public int statusButton(int x)
    {
        return board[x - 1];
    }
}
