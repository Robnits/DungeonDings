using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private bool receivedNumber = false;
    public int myNumber;

    public GameObject connector;
    private bool isPlayerInRange;
    private GameObject player;
    private InputHandler inputHandler;

    public void WhatIsMyNumber(int number)
    {
        if (receivedNumber == false)
        {
            receivedNumber = true;
            myNumber = number;
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inputHandler = GameObject.Find("LevelLoader").GetComponent<InputHandler>();

        GetConnection();
    }

    private void GetConnection()
    {
        GameObject[] stairsArray;
        if (CompareTag("DOWN"))
        {
            stairsArray = GameObject.FindGameObjectsWithTag("UP");
        }
        else
        {
            stairsArray = GameObject.FindGameObjectsWithTag("DOWN");
        }

        foreach (var item in stairsArray)
        {
            if (item.GetComponent<Stairs>().GetMyNumber() >= 3)
            {
                if (item.GetComponent<Stairs>().GetMyNumber() - 3 == myNumber)
                    connector = item;
            }
            else
            {
                if (item.GetComponent<Stairs>().GetMyNumber() + 3 == myNumber)
                    connector = item;
            }
        }
    }

    private void Update()
    {
        if (isPlayerInRange && inputHandler.IsPlayerInteracting())
            player.transform.position = connector.transform.position; 
    }
    

    public int GetMyNumber()
    {
        return myNumber;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().SprechblasePressE(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().SprechblasePressE(false);
            isPlayerInRange = false;
        }
    }
}