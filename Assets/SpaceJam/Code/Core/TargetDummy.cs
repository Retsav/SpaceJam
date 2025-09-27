using System;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] private float speed  = 2f;
    [SerializeField] [Range(0.1f, 5f)] private float size = 1f;

    private Transform _currentDestination;


    private void Start()
    {
        throw new NotImplementedException();
    }
}
