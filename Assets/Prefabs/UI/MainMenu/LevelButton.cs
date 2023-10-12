using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : PullableObj
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image icon;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Button button;

    [SerializeField] List<GameObject> stars = new List<GameObject>();

    Location _location;

    [SerializeField] LocalizationData textdata;

    public void Init(Location location, int star, bool open)
    {
        _location = location;
        text.text = $" {textdata.GetLocalization(Localizator.main.SelectedLaunguage)} {location.levelNum}";

        for(int i = 0; i < stars.Count; i++)
        {
            if(star > i)
            {
                stars[i].SetActive(true);
                icon.sprite = location.locationImage;
                button.interactable = true;
            }
            else
            {
                stars[i].SetActive(false);
            }
        }

        if (open)
        {
            button.interactable = true;
            icon.sprite = location.locationImage;
        }
        else
        {
            icon.sprite = defaultSprite;
            button.interactable = false;
        }
    }

    public void StartLevel()
    {
        _location.StartLocation();
        NewUIController.main.StartGame();
    }
}
