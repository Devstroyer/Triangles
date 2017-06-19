using System.Collections.Generic;
using UnityEngine;



public class Abstract : MonoBehaviour
{
    // FIELDS
    private float startTime;
    private GameManager gameManager;



    // PROPERTIES
    public float StartTime
    {
        get { return startTime; }
    }

    public float ElapsedTime
    {
        get { return Time.time - startTime; }
    }

    public GameManager GameManager
    {
        get { return gameManager; }
    }



    // OVERRIDES
    virtual protected void OnValidate() { }

    virtual protected void Awake() { }

    virtual protected void Start()
    {
        startTime = Time.time;
        gameManager = FindObjectOfType<GameManager>();
    }

    virtual protected void Update() { }

    virtual protected void LateUpdate() { }

    virtual protected void FixedUpdate() { }

}