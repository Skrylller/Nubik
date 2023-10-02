using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu main;

    [HideInInspector] public List<Location> _levels = new List<Location>();

    [SerializeField] LevelButton LevelButton;

    [SerializeField] PullObjects levelsPull;

    private void Awake()
    {
        main = this;
    }

    public void Init(List<Location> levels, List<int> stars)
    {
        _levels = levels;
        levelsPull.Clear();
        for(int i = 0; i < levels.Count; i++)
        {
            LevelButton button = levelsPull.AddObj() as LevelButton;
            button.Init(levels[i], stars[i], i == 0 || stars[i-1] > 0 ? true : false);
        }
    }
}
