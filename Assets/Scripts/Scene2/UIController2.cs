using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController2 : MonoBehaviour
{


    private GameObject bscoreText;

    private GameObject yscoreText;

    private int bscore = -13;

    private int yscore = -13;

    private int remaining = 127;

    GameObject hexagon;

    public StageController2 stageController2;

    // Use this for initialization
    void Start()
    {
        // シーンビューからオブジェクトの実体を検索する
        this.bscoreText = GameObject.Find("bscoreText");

        this.yscoreText = GameObject.Find("yscoreText");

        hexagon = GameObject.Find("hexagonGenerator");

        stageController2 = hexagon.GetComponent<StageController2>(); 
    }

    // Update is called once per frame
    void Update()
    {
        int[,] score = stageController2.color;
        foreach(var i in score){
            if(i == 1){
                this.bscore += 1;
                this.remaining -= 1;
            }else if(i == 2){
                this.yscore += 1;
                this.remaining -= 1;
            }
        }

        this.bscoreText.GetComponent<Text>().text = "Player1: " + this.bscore + "pt";

        this.yscoreText.GetComponent<Text>().text = "Player2: " + this.yscore + "pt";

        bscore = -13;
        yscore = -13;
    }

}