using System.Collections.Generic;
using UnityEngine;



public abstract class Abstract : MonoBehaviour
{
    // FIELDS
    static private GameManagerComponent gameManager;
    private float startTime;



    // PROPERTIES
    public float StartTime
    {
        get { return startTime; }
    }

    public float ElapsedTime
    {
        get { return Time.time - startTime; }
    }

    public GameManagerComponent GameManager
    {
        get { return gameManager; }
    }



    // OVERRIDES
    virtual public void Rebuild() { }

    virtual protected void Start()
    {
        gameManager = FindObjectOfType<GameManagerComponent>();
        startTime = Time.time;
    }

    virtual protected void Update() { }

    virtual protected void FixedUpdate() { }



    // METHODS
    protected void AddDebugLine(string text)
    {
        gameManager.Canvas.DebugLog.AddLine(text);
    }

    protected void LerpPositionTowards(Vector3 target, float speed)
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target, speed);
    }
}