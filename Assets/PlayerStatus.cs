﻿using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour {
   public  Image HP_ui {
        get {
            return GameObject.Find ("UI/HP").GetComponent<Image> ();
        }
    }
    public float PlayerHP = 10;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () { }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update () { }
    public void Heel (int interval = 5) {
        this.UpdateAsObservable ()
            .TakeWhile (NotCentor => !PlayerController.BattleFlag)
            .Delay (TimeSpan.FromSeconds (interval))
            .Subscribe (_ => {
                HP_ui.fillAmount += 0.01f * Time.deltaTime;
            }).AddTo(this.gameObject);
    }
	public void Damage(){
		var random=UnityEngine.Random.Range(0.005f,0.02f);
		HP_ui.fillAmount -= random;
		PlayerHP-=random;
	}

}