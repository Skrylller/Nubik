using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu main;

    [HideInInspector] public List<Location> _levels = new List<Location>();
    [HideInInspector] private List<int> _stars = new List<int>();

    [SerializeField] LevelButton LevelButton;

    [SerializeField] PullObjects levelsPull;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Localizator.main.OnChangeLaunguage += UpdateButtons;
    }

    public void Init(List<Location> levels, List<int> stars)
    {
        _levels = levels;
        _stars = stars;
        levelsPull.Clear();
        for(int i = 0; i < levels.Count; i++)
        {
            LevelButton button = levelsPull.AddObj() as LevelButton;
            button.Init(levels[i], stars[i], i == 0 || stars[i-1] > 0 || MainGameController.main.openAll ? true : false);
        }
    }

    public void UpdateButtons(Localizator.Launguage laung)
    {
        levelsPull.Clear();
        for (int i = 0; i < _levels.Count; i++)
        {
            LevelButton button = levelsPull.AddObj() as LevelButton;
            button.Init(_levels[i], _stars[i], i == 0 || _stars[i - 1] > 0 || MainGameController.main.openAll ? true : false);
        }
    }
}
