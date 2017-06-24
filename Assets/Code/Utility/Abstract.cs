using System.Collections.Generic;
using UnityEngine;



public abstract class Abstract : MonoBehaviour
{
    // ---------------------------------------------------------------- FIELDS
    // READ-ONLY
    static private GameManagerComponent gameManager;
    public GameManagerComponent GameManager
    { get { return gameManager; } }

    private float startTime;
    public float StartTime
    { get { return startTime; } }

    // PROPERTIES
    public float ElapsedTime
    {
        get { return Time.time - startTime; }
    }
    public Phases GamePhase
    {
        get { return GameManager.PhaseManager.GamePhase; }
    }



    // ---------------------------------------------------------------- METHODS
    // MONOBEHAVIOUR
    virtual protected void Start()
    {
        gameManager = FindObjectOfType<GameManagerComponent>();
        startTime = Time.time;
    }

    virtual protected void Update() { }

    virtual protected void FixedUpdate() { }

    // OTHER
    virtual public void Rebuild() { }

    protected void AddDebugLine(string text = "")
    { gameManager.Canvas.DebugLog.AddLine(text); }

    protected void LerpPositionTowards(Vector3 target, float speed)
    { this.transform.position = Vector3.Lerp(this.transform.position, target, speed); }

    protected void LerpRotationTowards(Quaternion target, float speed)
    { this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target, speed); }

    protected void LerpScaleTowards(Vector3 target, float speed)
    { this.transform.localScale = Vector3.Lerp(this.transform.localScale, target, speed); }

}