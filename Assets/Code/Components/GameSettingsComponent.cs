﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class GameSettingsComponent : Abstract
{
    // -------------------------------------------------------------------------------------------------------------------------------- FIELDS
    public float[] DirectionAngles = new float[4] { 0, 30, 150, 270 };
    public float MaxNeighborDistance = Mathf.Sqrt(3) / 3 * 1.1f;
    public float MaxAngleHalfError = 15;

    public int MaxActions = 3;
    public Phases StartingGamePhase = Phases.Queue;

    public AssetBundle cardSprites;






    // -------------------------------------------------------------------------------------------------------------------------------- PROPERTIES



    // -------------------------------------------------------------------------------------------------------------------------------- MONO
    protected override void Start()
    {
        base.Start();

        cardSprites = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, @"AssetBundles/Cards"));
    }



    // -------------------------------------------------------------------------------------------------------------------------------- METHODS
    public Sprite LoadCardSprite(string path)
    {
        return cardSprites.LoadAsset<Sprite>(path);
    }



}
