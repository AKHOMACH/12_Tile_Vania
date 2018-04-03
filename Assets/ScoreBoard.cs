﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

    public static int score;  // still need to discuss and figure out this notion of statics

    Text scoreBoardText;

    void Start()
    {
        scoreBoardText = GetComponent<Text>();
        scoreBoardText.text = score.ToString();
    }

    void Update()
    {
        scoreBoardText.text = score.ToString();    
    }

    public static void AddPoints (int pointsToAdd)
    {
        score += pointsToAdd;
    }
}
