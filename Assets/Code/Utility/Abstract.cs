using System.Collections.Generic;
using UnityEngine;



public abstract class Abstract : MonoBehaviour
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    static private GameManagerComponent gameManager;
    public GameManagerComponent GameManager
    { get { return gameManager; } }

    private float startTime;
    public float StartTime
    { get { return startTime; } }



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    // Returns how long the object has been alive
    public float ElapsedTime
    {
        get { return Time.time - startTime; }
    }
    // Shortcut for reading current GamePhase
    public Phases GamePhase
    {
        get { return GameManager.PhaseManager.GamePhase; }
    }



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    // Called in editor whenever anything is changed in the inspector. Not really Mono, but let's pretend!
    virtual public void Rebuild() { }   

    // Called before the first Update()
    virtual protected void Start()
    {
        gameManager = FindObjectOfType<GameManagerComponent>();
        startTime = Time.time;
    }

    // Called an arbitraty number of times per second
    virtual protected void Update() { }

    // Called a fixed number of times per second, right before the physics update step
    virtual protected void FixedUpdate() { }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    // Shortcut for adding lines to the DebugLog
    protected void AddDebugLine(string text = "")
    { gameManager.Canvas.DebugLog.AddLine(text); }

    // Convenient transform lerps
    protected void LerpPositionTowards(Vector3 target, float speed)
    { this.transform.position = Vector3.Lerp(this.transform.position, target, speed); }

    protected void LerpRotationTowards(Quaternion target, float speed)
    { this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target, speed); }

    protected void LerpScaleTowards(Vector3 target, float speed)
    { this.transform.localScale = Vector3.Lerp(this.transform.localScale, target, speed); }

}