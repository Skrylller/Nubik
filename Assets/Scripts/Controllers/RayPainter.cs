using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPainter : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit2D _hit;
    private float angle;

    private void Update()
    {
        CheckAngle();
    }

    private void CheckAngle()
    { 
        if (angle != transform.eulerAngles.z)
        {
            angle = transform.eulerAngles.z;

            _hit = Physics2D.Raycast(transform.position, transform.TransformDirection(transform.localEulerAngles.z == 0 ? Vector3.right : Vector3.left), 1000, _layerMask);

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _hit.point);
        }
    }
}
