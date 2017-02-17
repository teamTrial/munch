﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poeple : MonoBehaviour {
    public class Count {
        public int limit;
        public float counter;
        public bool countdowsflag;
        public Count (int _limit = 10) {
            countdowsflag = false;
            limit = _limit;
            counter = 0;
        }
    }
    Count count;
    public float speed = 1f;
    private InstanceEnemy Right, Left;
    private StageManager StageManager;
    private bool Battleflag;
    void Start () {
        Battleflag=false;
        count = new Count ();
        StageManager = GameObject.Find ("Manager").GetComponent<StageManager> ();
        speed = speed * Random.Range (0.3f, 1.0f);
        Right = GameObject.Find ("CreatePeople_Right").GetComponent<InstanceEnemy> ();
        Left = GameObject.Find ("CreatePeople_Left").GetComponent<InstanceEnemy> ();
    }

    void Update () {
        if (!Battleflag) {
            walk ();
            if (count.countdowsflag) {
                count.counter += Time.deltaTime;
            }
            if (count.limit < count.counter) {
                Destroy (this.gameObject);
            }
        }
    }
    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "MainCamera") {
            count.counter = 0;
            count.countdowsflag = false;
        }
        if (other.tag == "controller") return;

        //至近距離でプレイヤーに接触した場合
        if (other.tag == "hand") {
            // Destroy(this.gameObject);
            // StageManager.EnemyNum=StageManager.EnemyNum+1;
            // StageManager.UpdateNum();
            print("当たった");
            Battleflag=true;
            GetComponent<Battle>().enabled=true;
        }
        if (other.tag == "stand") {
            print ("構えるモーション接触");
        }
    }
    //見えなくなったら
    void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "MainCamera") {
            count.countdowsflag = true;
        }
    }

    void walk () {
        int direction = 1;
        //左
        if (this.transform.localScale.x < 0) {
            direction = -1;
        }
        //右
        else {
            direction = 1;
        }
        this.transform.Translate (direction * speed * Time.deltaTime, 0, 0);
    }
    void OnDestroy () {
        Right.enemyCounter--;
        Left.enemyCounter--;
    }
}