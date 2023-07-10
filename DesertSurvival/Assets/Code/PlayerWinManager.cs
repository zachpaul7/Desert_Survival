using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessgaePanel;
    PauseManager pauseManager;
    [SerializeField] DataContainer dataContainer;

    private void Awake()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    public void Win()
    {
        pauseManager.PauseGame();
        winMessgaePanel.SetActive(true);
        dataContainer.StageComplete(0);
    }
}
