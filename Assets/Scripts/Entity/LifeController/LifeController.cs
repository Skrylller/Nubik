using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, ISliderVisitor
{
    [SerializeField] private PullableObj _particleDeath;
    [SerializeField] private GameObject _capturedObject;
    private ICapturedObject _IcapturedObject;
    [SerializeField] private PullableObj _particleDammage;
    private PullObjects _partPull;
    [SerializeField] private float _immortalTime = 1f;

    [SerializeField] private EnemyModel _model;

    private Transform[] children;

    private bool _isCapture;
    private int _tapCounterCapture;
    private Action OnEndAction;

    public bool IsCapture => _isCapture;

    [SerializeField] private uint _health;
    public uint HealthGet { get { return _health; } }
    private uint Health 
    { 
        set 
        {
            _health = value;
            sliderValue = (float)_health / _model.MaxHealth;
        } 
    }

    public Action OnDammage;
    public Action OnHill;
    public Action OnDeath;

    public Action<float> OnChange { get; set; }
    public float sliderValue 
    {
        set 
        {
            OnChange?.Invoke(value);
        } 
    }

    private void Awake()
    {
        _IcapturedObject = _capturedObject.GetComponent<ICapturedObject>();
        children = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        _isCapture = false;
        _partPull = PullsController.main.GetPull(_particleDammage);
    }

    public void Init(EnemyModel model)
    {
        _model = model;
        Health = model.MaxHealth;
    }

    public void Restart()
    {
        if (_model == null)
            return;

        Health = _model.MaxHealth;
    }

    public void TakeDammage(uint dammage, Vector2 point, float angle)
    {
        PullableObj part = _partPull.AddObj();
        part.SetTransform(point, angle);

        if (_health <= dammage)
        {
            Health = 0;

            PullsController.main.GetPull(_particleDeath).AddObj().SetTransform(transform.position);
            gameObject.SetActive(false);
            OnDeath?.Invoke();
        }
        else
        {
            StartCoroutine(CourotineImmortal());
            Health = HealthGet - dammage;
            OnDammage?.Invoke();
        }
    }

    public void Hill(uint hill)
    {
        if (_model.MaxHealth <= hill + _health)
        {
            Health = _model.MaxHealth;
        }
        else
        {
            Health = HealthGet + hill;
        }

        OnHill?.Invoke();
    }

    public void Capture(Transform position, int tapCounter, Action EndAction)
    {
        _isCapture = true;
        OnEndAction = EndAction;
        _tapCounterCapture = tapCounter;
        _IcapturedObject.Capture(position);
    }

    public void TapCapture()
    {
        if (!_isCapture)
            return;
        _tapCounterCapture--;
        Debug.Log(_tapCounterCapture);
        if (_tapCounterCapture <= 0)
            StopCapture();
    }

    public void StopCapture()
    {
        _isCapture = false;
        OnEndAction.Invoke();
        OnEndAction = null;
        _IcapturedObject.EndCapture();
    }

    private IEnumerator CourotineImmortal()
    {
        gameObject.layer = 4;

        foreach(Transform child in children)
        {
            child.gameObject.layer = 4;
        }

        yield return new WaitForSeconds(_immortalTime);


        foreach (Transform child in children)
        {
            child.gameObject.layer = 6;
        }

        gameObject.layer = 6;
    }
}

public interface ICapturedObject
{
    void Capture(Transform position);
    void EndCapture();
}

public interface ILifeModel
{
    uint MaxHealth { get; }  
}
