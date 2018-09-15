using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageController3 : MonoBehaviour
{

    public GameObject hexagonPrefab;

    public GameObject ob;

    public int[,] color = new int[19, 12];

    public Dictionary<GameObject, int[]> objectPosition = new Dictionary<GameObject, int[]>();

    int turn = 1;

    //最初に色付けするやつ
    public List<GameObject> list = new List<GameObject>();

    //色ごとに最初に生成する個数
    int startobj = 10;

    // 周りの色の数
    public int[] r = new int[3];

    //オブジェクトの名前
    public string n;

    // Use this for initialization
    void Start()
    {

        for (int i = 1; i < 18; i++)
        {
            if (i % 2 == 1)//1,3,5,7,9,11,13,15,17行目
            {

                for (int j = 1; j < 10; j++)
                {
                    GameObject go = Instantiate(hexagonPrefab) as GameObject;
                    go.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                    go.name = i.ToString() + "-" + j.ToString();
                    int[] p = { i, j };
                    objectPosition.Add(go, p);
                    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                    go.transform.position = new Vector2(-8.7f + 1.3f * j, 12.02f - 1.2f * i);

                    if(i == 1 || i == 17){
                        if(2 <= j && j <= 8){
                            list.Add(go);
                        }
                    }
                }
            }

            else//2,4,6,8,10,12,14,16行目
            {
                
                for (int j = 1; j < 11; j++)
                {
                    
                    GameObject go = Instantiate(hexagonPrefab) as GameObject;
                    go.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                    go.name = i.ToString() + "-" + j.ToString();
                    int[] p = { i, j };
                    objectPosition.Add(go, p);
                    go.transform.position = new Vector2(-8.7f + 1.3f * j - 0.6f, 12.02f - 1.2f * i);
                    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();

                    if(j == 1 || j == 10){
                        list.Add(go);
                    }
                 }
            }
        }

        Extensions.Shuffle(list);

        for (int k = 0; k < 30; k++){
            SpriteRenderer spriteRenderer = list[k].GetComponent<SpriteRenderer>();
            int[] p = objectPosition[list[k]];
            if(k < 10){
                spriteRenderer.color = Color.black;
                color[p[0], p[1]] = 1;
            }else if(k < 20){
                spriteRenderer.color = Color.yellow;
                color[p[0], p[1]] = 2;
            }else{
                spriteRenderer.color = Color.green;
                color[p[0], p[1]] = 3;
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {

            Touch _touch = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(_touch.position);

            if (_touch.phase == TouchPhase.Began)
            {

                Collider2D collider2d = Physics2D.OverlapPoint(worldPoint);

                if (collider2d)
                {
                    GameObject obj = collider2d.transform.gameObject;
                    SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                    int[] p = objectPosition[obj];

                    if (color[p[0], p[1]] == 0)
                    {

                        if (turn == 1)
                        {
                            spriteRenderer.color = Color.black;
                            color[p[0], p[1]] = 1;
                            turn++;
                        }
                        else if(turn == 2)
                        {
                            spriteRenderer.color = Color.yellow;
                            color[p[0], p[1]] = 2;
                            turn++;
                        }else{
                            spriteRenderer.color = Color.green;
                            color[p[0], p[1]] = 3;
                            turn = 1;
                        }


                    }

                }

            }
        }


        // 三面が接するところのアルゴリズム
        for (int s = 1; s < 4;s++){
            for (int i = 1; i < 18; i++){
                if (i % 2 == 1){
                    for (int j = 1; j < 10; j++){
                        if (color[i - 1, j] == s){
                            r[s - 1] += 1;
                        } 
                        if (color[i - 1, j + 1] == s){
                            r[s - 1] += 1;
                        }
                        if (color[i, j - 1] == s){
                            r[s - 1] += 1;
                        }
                        if (color[i, j + 1] == s){
                            r[s - 1] += 1;
                        }
                        if (color[i + 1, j] == s){
                            r[s - 1] += 1;
                        }
                        if (color[i + 1, j + 1] == s){
                            r[s - 1] += 1;
                        }
                        if (r[s - 1] >= 3 && color[i, j] == 0)
                        {
                            color[i, j] = s;
                            this.n = i.ToString() + "-" + j.ToString();
                            this.ob = GameObject.Find(this.n);
                            SpriteRenderer spriteRenderer = ob.GetComponent<SpriteRenderer>();
                            if(s == 1){
                                spriteRenderer.color = Color.black;
                            }else if(s == 2){
                                spriteRenderer.color = Color.yellow;
                            }else{
                                spriteRenderer.color = Color.green;
                            }
                           
                        }

                        r[s - 1] = 0;
                    }
                }
                else
                {
                    for (int j = 1; j < 11; j++)
                    {
                        if (color[i - 1, j - 1] == s)
                        {
                            r[s - 1] += 1;
                        }
                        if (color[i - 1, j] == s)
                        {
                            r[s - 1] += 1;
                        }
                        if (color[i, j - 1] == s)
                        {
                            r[s - 1] += 1;
                        }
                        if (color[i, j + 1] == s)
                        {
                            r[s - 1] += 1;
                        }
                        if (color[i + 1, j - 1] == s)
                        {
                            r[s - 1] += 1;
                        }
                        if (color[i + 1, j] == s)
                        {
                            r[s - 1] += 1;
                        }
                        if (r[s - 1] >= 3 && color[i, j] == 0)
                        {
                            color[i, j] = s;
                            this.n = i.ToString() + "-" + j.ToString();
                            this.ob = GameObject.Find(this.n);
                            SpriteRenderer spriteRenderer = ob.GetComponent<SpriteRenderer>();
                            if (s == 1)
                            {
                                spriteRenderer.color = Color.black;
                            }
                            else if (s == 2)
                            {
                                spriteRenderer.color = Color.yellow;
                            }
                            else
                            {
                                spriteRenderer.color = Color.green;
                            }

                        }

                        r[s - 1] = 0;
                    } 
                }
            }
        }
    }
}

public class Extensions
{
    //リストの要素をシャッフルする (Fisher-Yates shuffle)
    public static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);  //[0]～[i]
            Swap(list, i, j);
        }
    }

    //要素のスワップ
    public static void Swap<T>(List<T> list, int i, int j)
    {
        T tmp = list[i];
        list[i] = list[j];
        list[j] = tmp;
    }
}