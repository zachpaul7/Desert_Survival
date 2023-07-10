using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponParent;
    PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        GetComponent<PlayerMove>().enabled = false;
        pauseManager.PauseGame();
        gameOverPanel.SetActive(true);
        weaponParent.SetActive(false);
    }
}
