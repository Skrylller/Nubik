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

    public void DeactivateAllLevels()
    {
        for(int i = 0; i < locations.Count; i++)
        {
            locations[i].gameObject.SetActive(false);
        }
    }

    public void NextLevel(Location level)
    {

        if (level.levelNum == locations.Count)
        {
            HudUI.main.YesExit();
        }
        else
        {
            locations[level.levelNum].StartLocation();
        }
    }
}
