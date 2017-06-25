using System.Collections.Generic;
using UnityEngine;



public class PhaseManagerComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    private Phases gamePhase;
    public new Phases GamePhase
    { get { return gamePhase; } }

    private bool isResolvingAction;



    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    override protected void Start()
    {
        base.Start();
        gamePhase = GS.StartingGamePhase;

    }

    override protected void Update()
    {
        base.Update();

        switch(gamePhase)
        {
            case Phases.Queue:
                if(AllPlayersFinishedQueueing())
                    gamePhase = Phases.Resolve;
                break;

            case Phases.Resolve:
                if(!isResolvingAction)
                    StartCoroutine(ResolveAllActions());
                break;

            case Phases.Realtime:
                gamePhase = GS.StartingGamePhase;
                break;
        }

        // Debug
        AddDebugLine(gamePhase.ToString().ToUpper() + " PHASE");
        AddDebugLine();
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    private bool AllPlayersFinishedQueueing()
    {
        foreach(PlayerComponent player in GameManager.Players)
            if(!player.IsReadyForResolvePhase)
                return false;
        return true;
    }

    private System.Collections.IEnumerator ResolveAllActions()
    {
        int longestActionQueue = 0;
        foreach(PlayerComponent player in GameManager.Players)
            if(player.Actions.Count > longestActionQueue)
                longestActionQueue = player.Actions.Count;

        for(int i = 0; i < longestActionQueue; i++)
            foreach(PlayerComponent player in GameManager.Players)
                if(player.Actions.Count > i)
                {
                    isResolvingAction = true;
                    player.ResolveAction(player.Actions[i]);
                    yield return new WaitForSeconds(0.5f);
                    isResolvingAction = false;
                }

        foreach(PlayerComponent player in GameManager.Players)
            player.ResetActionsQueue();

        gamePhase = Phases.Queue;
    }

}
