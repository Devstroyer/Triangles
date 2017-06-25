using System.Collections.Generic;
using UnityEngine;



public class PlayerComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public KeyCode Red = KeyCode.LeftArrow;
    public KeyCode Green = KeyCode.DownArrow;
    public KeyCode Blue = KeyCode.UpArrow;

    private List<Action> actions;
    public List<Action> Actions
    { get { return actions; } }

    private Cards cardInput;
    private TileComponent activeTile;
    private bool isMoving;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES
    public bool IsReadyForResolvePhase
    { get { return actions.Count >= GS.MaxActions; } }



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();
        GameManager.Players.Add(this);
        actions = new List<Action>();
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
            AddDebugLine("  " + i + ". " + (actions.Count > i ? actions[i].ToString() : "..."));
        AddDebugLine();
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    public void TryMoveTo(TileComponent targetTile)
    {
        if(targetTile != null)
            StartCoroutine(SlowlyMoveTo(targetTile));
    }

    public void ResolveAction(Action action)
    {
        switch(action.A)
        {
            case Cards.Red:
                break;

            case Cards.Green:
                TryMoveTo(activeTile.GetNeighbors()[(int)action.B]);
                break;

            case Cards.Blue:
                break;
        }
    }

    public void ResetActionsQueue()
    { actions.Clear(); }

    private void ReceiveInput()
    {
        if(Input.GetKeyDown(Red))
            cardInput = Cards.Red;
        if(Input.GetKeyDown(Green))
            cardInput = Cards.Green;
        if(Input.GetKeyDown(Blue))
            cardInput = Cards.Blue;
    }

    private void ConsumeInput()
    {
        if(cardInput != Cards.None)
            switch(GamePhase)
            {
                case Phases.Queue:
                    TryEnqueue(Cards.Green, cardInput);
                    break;

                case Phases.Realtime:
                    if(!isMoving)
                        TryMoveTo(activeTile.GetNeighbors()[(int)cardInput]);
                    break;
            }

        cardInput = Cards.None;
    }

    private void TryEnqueue(Cards a, Cards b)
    {
        if(actions.Count < GS.MaxActions)
            actions.Add(new Action(a, b));
    }

    private System.Collections.IEnumerator SlowlyMoveTo(TileComponent targetTileComponent)
    {
        if(targetTileComponent != null)
        {
            isMoving = true;
            for(int i = 0; i < 15; i++)
            {
                LerpPositionTowards(targetTileComponent.transform.position, 0.2f);
                yield return new WaitForSeconds(0.005f);
            }
            isMoving = false;

            activeTile = targetTileComponent;
            this.transform.position = activeTile.transform.position;
        }
    }


}
