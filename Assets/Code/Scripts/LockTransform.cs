using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : Abstract
{
    public bool PositionX, PositionY, PositionZ;
    public bool RotationX, RotationY, RotationZ;
    public bool LocalScaleX, LocalScaleY, LocalScaleZ;

    private float initialPositionX, initialPositionY, initialPositionZ;
    private float initialRotationX, initialRotationY, initialRotationZ;
    private float initialLocalScaleX, initialLocalScaleY, initialLocalScaleZ;

    protected override void Start()
    {
        base.Start();
        initialPositionX = transform.position.x;
        initialPositionY = transform.position.y;
        initialPositionZ = transform.position.z;

        initialRotationX = transform.eulerAngles.x;
        initialRotationY = transform.eulerAngles.y;
        initialRotationZ = transform.eulerAngles.z;

        initialLocalScaleX = transform.localScale.x;
        initialLocalScaleY = transform.localScale.y;
        initialLocalScaleZ = transform.localScale.z;
    }

    protected override void Update()
    {
        base.Update();
        transform.position = new Vector3(PositionX ? initialPositionX : transform.position.x,
                                             PositionY ? initialPositionY : transform.position.y,
                                             PositionZ ? initialPositionZ : transform.position.z);

        transform.eulerAngles = new Vector3(RotationX ? initialRotationX : transform.eulerAngles.x,
                                            RotationY ? initialRotationY : transform.eulerAngles.y,
                                            RotationZ ? initialRotationZ : transform.eulerAngles.z);

        transform.localScale = new Vector3(LocalScaleX ? initialLocalScaleX : transform.localScale.x,
                                           LocalScaleY ? initialLocalScaleY : transform.localScale.y,
                                           LocalScaleZ ? initialLocalScaleZ : transform.localScale.z);
    }
}
