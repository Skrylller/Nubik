using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using System.Linq;

public class MainGameController : MonoBehaviour
{
    public static MainGameController main;

    public bool openAll;
    public List<Location> locations;
    public List<int> stars;
    public Location location;
    public WeaponModel weapon;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        LoadAllLevels();

        NewUIController.main.ExitMenu();
        MainMenu.main.Init(locations, stars);
    }

    public void YandexLoadUpdateData()
    {
        if (stars.ToArray() == YandexGame.savesData.stars)
            return;

        for (int i = 0; i < locations.Count; i++)
        {
            if (stars[i] < YandexGame.savesData.stars[i])
            {
                stars = YandexGame.savesData.stars.ToList();
                return;
            }
            if (i == locations.Count - 1)
            {
                YandexGame.SaveProgress();
            }
        }
        MainMenu.main.Init(locations, stars);
    }

    public void DeactivateAllLevels()
    {
        for(int i = 0; i < locations.Count; i++)
        {
            locations[i].gameObject.SetActive(false);
        }
    }

    public void SetLevel(Location loca)
    {
        location = loca;
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

    public void CheckLoose()
    {
        if(weapon.BulletInClip <= 0)
        {
            if(!location.CheckEndLevel())
                HudUI.main.Lose();
        }
    }

    public void LoadAllLevels()
    {
        for (int i = 0; i < locations.Count; i++)
        {
            stars[i] = PlayerPrefs.GetInt($"{locations[i].name}", 0);
        }
    }

    public void SuccessLevel(Location level, int star)
    {
        if(stars[level.levelNum - 1] < star)
        {
            stars[level.levelNum - 1] = star;
            PlayerPrefs.SetInt($"{level.name}", star);

            YandexGame.savesData.stars = stars.ToArray();

            YandexGame.SaveProgress();
            MainMenu.main.Init(locations, stars);
        }
    }
}
