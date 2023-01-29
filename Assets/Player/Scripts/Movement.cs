using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        direction.Normalize();
        Vector2 offset = direction * _speed * Time.deltaTime;
        
        _rigidbody.MovePosition(_rigidbody.position += offset);
    }
}
