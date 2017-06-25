using System.Collections.Generic;
using UnityEngine;


public class BoomComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public float Length = 10;
    public Vector3 WorldRotation = Vector3.zero;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    public Vector3 SocketPosition
    {
        get
        {
            Vector3 r = new Vector3(0, 0, -Length);
            r = transform.rotation * r;
            r += transform.position;
            return r;
        }
    }



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, SocketPosition);
        Gizmos.DrawSphere(SocketPosition, 0.1f);
    }

    public override void Rebuild()
    {
        base.Rebuild();
        Update();
    }

    protected override void Update()
    {
        base.Update();
        ApplyWorldRotation();
        SnapChildrenToSocket();
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    private void SnapChildrenToSocket()
    {
        foreach(Transform child in transform)
            child.position = SocketPosition;
    }

    private void ApplyWorldRotation()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.right, WorldRotation.x, Space.World);
        transform.Rotate(Vector3.up, WorldRotation.y, Space.World);
        transform.Rotate(Vector3.forward, WorldRotation.z, Space.World);

    }

}
