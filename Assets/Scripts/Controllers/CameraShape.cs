using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShape : MonoBehaviour
{
    public static CameraShape main;
    [SerializeField] private PositionTarget _target;

    [SerializeField] private Vector3 _shapeForce;
    [SerializeField] private float _shapeTimer;

    private bool _isShape = false;

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        if(_isShape)
            _target.SetDeviation(new Vector3(Random.Range(-_shapeForce.x, _shapeForce.x), Random.Range(-_shapeForce.y, _shapeForce.y), Random.Range(-_shapeForce.z, _shapeForce.z)));
    }

    public void Shape()
    {
        Shape(_shapeTimer);
    }

    public void Shape(float timer)
    {
        StartCoroutine(ShapeCourotine(timer));
    }

    private IEnumerator ShapeCourotine(float timer)
    {
        _isShape = true;
        yield return new WaitForSeconds(timer);
        _isShape = false;
        _target.SetDeviation(Vector3.zero);
    }
}
