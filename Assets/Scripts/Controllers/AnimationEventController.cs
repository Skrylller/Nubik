using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationEventController : MonoBehaviour
{
    [SerializeField] private UnityEvent _action;
    [SerializeField] private List<UnityEvent> _actions;
    private Animator _animator;

    [SerializeField] private List<SoundController.AudioObj> _audio = new List<SoundController.AudioObj>();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Action()
    {
        _action?.Invoke();
    }
    public void ActionIndex(int index)
    {
        _actions[index]?.Invoke();
    }

    public void SetAnimatorState(int value)
    {
        _animator.SetInteger("State", value);
    }

    public int GetState()
    {
        return _animator.GetInteger("State");
    }

    public void PlaySound(int soundNum)
    {
        SoundController.main.PlaySound(_audio[soundNum]);
    }
}
