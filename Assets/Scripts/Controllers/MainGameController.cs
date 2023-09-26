using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public static MainGameController main;

    public List<Location> locations;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        NewUIController.main.ExitMenu();
        MainMenu.main.Init(locations);
    }
}
