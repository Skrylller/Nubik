using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _deafualtQuantity;
    [SerializeField] private float _maxFactor;

    private void Update()
    {
        float factor = ((float)Screen.width / (float)Screen.height) / (1920f / 1080f);

        //Debug.Log($"{Screen.width} {Screen.height} {factor}");

        if (factor > _maxFactor)
            factor = _maxFactor;

        _camera.orthographicSize = _deafualtQuantity / factor;
    }
}
