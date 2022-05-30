using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] Canvas resultCanvas;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI resultText2;
    [SerializeField] TextMeshProUGUI resultText3;
    [SerializeField] TextMeshProUGUI resultText4;
    private WanderingSoul[] _wanderingSouls;
    private Timer _tiemr;
    private Player _player;
    private bool _didWon;
    private bool _isFinished = false;
    private bool _runAway = false;

    private void Start() 
    {
        resultCanvas.enabled = false;
        _wanderingSouls = FindObjectsOfType<WanderingSoul>();    
        _tiemr = FindObjectOfType<Timer>();    
        _player = FindObjectOfType<Player>();    
    }

    private void Update()
    {
        EndConditions();
        if (_isFinished) WinConditions();
    }

    private void EndConditions()
    {
        if (_player.isDead) { _isFinished = true; }
        if (_tiemr.time >= 50) { _isFinished = true; }
        if (_player.reachedTheEnd) { _isFinished = true; }
        foreach (WanderingSoul soul in _wanderingSouls) { if (soul.didRunaway) { _isFinished = true; _runAway = true; } }

    }

    private void WinConditions()
    {
        if (_tiemr.time >= 50) { _didWon = false; }
        if (_player.reachedTheEnd) { _didWon = (_player.collectedSouls == _wanderingSouls.Length) ? true : false; }
        if (_player.isDead) { _didWon = false; }
        foreach (WanderingSoul soul in _wanderingSouls) { if (soul.didRunaway) { _didWon = false; } }

        resultCanvas.enabled = true; 
        resultText.text = (_didWon) ? "congratulations" : "Oops! maybe next time?!";
        if (_player.reachedTheEnd && _didWon)
        {
            resultText2.text = "you collect all wandering souls";
        }
        else if (_player.reachedTheEnd && !_didWon)
        {
            resultText2.text = "you didn't collect all wandering souls";
        }
        else if (_tiemr.time >= 50)
        {
            resultText2.text = "you didn't finish in time";
        }
        else if (_runAway)
        {
            resultText2.text = "wandering soul escaped";
        }
        else if (_player.isDead)
        {
            resultText2.text = "you died";
        }
        // resultText3.text = (_player.collectedSouls == _wanderingSouls.Length) ? "you collect all wandering souls" : "you didn't collect all wandering souls";
        // resultText4.text = (_player.collectedSouls == _wanderingSouls.Length) ? "finished on time" : "didn't finish on time";
    }
}
