using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SS_Boundary : ScriptableObject
{
    [SerializeField]
    public Boundary DeleteBoundary;
    [SerializeField]
    public Boundary CollisionBoundary;

    [System.Serializable]
    public class Boundary
    {
        public float width, height;
        public Vector2 center;
    }
}
