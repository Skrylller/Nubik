using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DammageObserver : MonoBehaviour
{
    [SerializeField] private int _dammageLayer;
    [SerializeField] private LayerMask _dammageLayerMask;
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private float _dammageFactor;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.collider);
    }

    private void CheckCollision(Collider2D collision)
    {
        if (collision.gameObject.layer != _dammageLayer)
            return;

        DammageArea dammageArea = collision.gameObject.GetComponent<DammageArea>();

        if (dammageArea != null)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(_collider.bounds.center, collision.gameObject.transform.position, 100, _dammageLayerMask);

            TakeDammage(dammageArea.Dammage, hit.point, Vector3.SignedAngle(collision.gameObject.transform.position, _collider.bounds.center, Vector3.right));
        }

        CaptureArea captureArea = collision.gameObject.GetComponent<CaptureArea>();

        if(captureArea != null)
        {
            Capture(captureArea.TargetPosition, captureArea.TapCount, () => captureArea.EndCapture());
        }
    }

    public void TakeDammage(uint dammage, Vector2 point, float angle)
    {
        _lifeController.TakeDammage((uint)Mathf.Abs(dammage * _dammageFactor), point, angle);
    }

    public void Capture(Transform position, int tapCount, Action EndAction)
    {
        _lifeController.Capture(position, tapCount, EndAction);
    }
}
