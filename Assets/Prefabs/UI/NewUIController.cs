using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUIController : MonoBehaviour
{
    public static NewUIController main;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private HudUI _hudUI;
    [SerializeField] private EndLevelUI _endLevelUI;

    private void Awake()
    {
        main = this;

        ExitMenu();
    }

    public void StartGame()
    {
        _mainMenu.gameObject.SetActive(false);
        _hudUI.gameObject.SetActive(true);
        _endLevelUI.gameObject.SetActive(false);
    }

    public void EndLevel()
    {
        _endLevelUI.gameObject.SetActive(true);
    }

    public void Restart()
    {
        _endLevelUI.gameObject.SetActive(false);
    }

    public void ExitMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        _hudUI.gameObject.SetActive(false);
        _endLevelUI.gameObject.SetActive(false);
    }
}
