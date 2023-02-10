using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //Set Game Manager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Set button and check click
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked.");
        gameManager.StartGame(difficulty);
    }
}