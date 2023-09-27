using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : PullableObj
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image icon;

    Location _location;

    public void Init(Location location)
    {
        _location = location;
        text.text = $"Level {location.levelNum}";
        icon.sprite = location.locationImage;
    }

    public void StartLevel()
    {
        _location.StartLocation();
        NewUIController.main.StartGame();
    }
}
