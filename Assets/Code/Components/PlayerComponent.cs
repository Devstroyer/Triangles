using System.Collections.Generic;
using UnityEngine;



public class PlayerComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public KeyCode Red = KeyCode.LeftArrow;
    public KeyCode Green = KeyCode.DownArrow;
    public KeyCode Blue = KeyCode.UpArrow;

    private List<Order> orders;
    public List<Order> Orders
    { get { return orders; } }

    private Directions directionInput;
    private TileComponent activeTile;
    private bool isMoving;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    public bool IsReadyForResolvePhase
    { get { return orders.Count >= GS.MaxActions; } }



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();
        GameManager.Players.Add(this);
        orders = new List<Order>();
        TryMoveTo(GameManager.Grids.Find(o => o != null).GetTileClosestTo(this.transform.position));
    }

    protected override void Update()
    {
        base.Update();

        // Manage player input
        ReceiveInput();
        ConsumeInput();

        // Debug
        AddDebugLine(name);
        for(int i = 0; i < GS.MaxActions; i++)
            AddDebugLine("  " + i + ". " + (orders.Count > i ? orders[i].ToString() : "..."));
        AddDebugLine();

        // Draw 
        /*
        if(activeTile != null)
        {
            TileComponent[] neighbors = activeTile.GetNeighbors();
            for(int i = 1; i < GS.DirectionAngles.Length; i++)
                if(neighbors[i] != null)
                    for(int j = -5; j <= 5; j++)
                    {
                        Vector3 rotatedDiff = Quaternion.AngleAxis(GS.MaxAngleHalfError * j / 5, Vector3.forward) * (neighbors[i].transform.position - activeTile.transform.position);
                        Debug.DrawLine(activeTile.transform.position, activeTile.transform.position + rotatedDiff, GS.DirectionColors[i]);
                    }
        }
        */
    }




    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    public void TryMoveTo(TileComponent targetTile)
    {
        if(targetTile != null)
            StartCoroutine(SlowlyMoveTo(targetTile));
    }

    public void ResolveAction(Order action)
    {
        switch(action.A)
        {
            case Actions.Attack:
                break;

            case Actions.Move:
                TryMoveTo(activeTile.GetNeighbors()[(int)action.B]);
                break;

            case Actions.Draw:
                break;
        }
    }

    public void ResetActionsQueue()
    { orders.Clear(); }

    private void ReceiveInput()
    {
        if(Input.GetKeyDown(Red))
            directionInput = Directions.Red;
        if(Input.GetKeyDown(Green))
            directionInput = Directions.Green;
        if(Input.GetKeyDown(Blue))
            directionInput = Directions.Blue;
    }

    private void ConsumeInput()
    {
        if(directionInput != Directions.None)
            switch(GamePhase)
            {
                case Phases.Queue:
                    TryEnqueue(Actions.Move, directionInput);
                    break;

                case Phases.Realtime:
                    if(!isMoving)
                        TryMoveTo(activeTile.GetNeighbors()[(int)directionInput]);
                    break;
            }

        directionInput = Directions.None;
    }

    private void TryEnqueue(Actions a, Directions b)
    {
        if(orders.Count < GS.MaxActions)
            orders.Add(new Order(a, b));
    }

    private System.Collections.IEnumerator SlowlyMoveTo(TileComponent targetTile)
    {
        if(targetTile != null)
        {
            isMoving = true;
            for(int i = 0; i < 15; i++)
            {
                LerpPositionTowards(targetTile.transform.position, 0.2f);
                yield return new WaitForSeconds(0.005f);
            }
            isMoving = false;

            activeTile = targetTile;
            this.transform.position = activeTile.transform.position;
        }
    }
}