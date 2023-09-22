using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<Location> levels = new List<Location>();

    [SerializeField] LevelButton LevelButton;

    [SerializeField] PullObjects levelsPull;

    private void Start()
    {
        levelsPull.Clear();
        for(int i = 0; i < levels.Count; i++)
        {
            LevelButton button = levelsPull.AddObj() as LevelButton;
            button.Init(levels[i]);
        }
    }
}
