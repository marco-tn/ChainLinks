using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController2 : MonoBehaviour
{

    public GameObject hexagonPrefab;

    public GameObject ob;

    public int[,] color = new int[17, 11];

    public int[,] r1color = new int[17, 11];

    public int[,] r2color = new int[17, 11];

    public Dictionary<GameObject, int[]> objectPosition = new Dictionary<GameObject, int[]>();

    int turn = 1;

    //ランダム生成時のその段階でのそれぞれの個数
    int s1 = 0;

    int s2 = 0;

    //色ごとに最初に生成する個数
    int startobj = 13;

    // 周りの色の数
    int r1 = 0;

    int r2 = 0;

    //オブジェクトの名前
    public string n;

    //処理間隔
    private float timeleft;

    // Use this for initialization
    void Start()
    {

        for (int i = 1; i < 16; i++)
        {
            if (i % 2 == 1)//1,3,5,7,9,11,13,15行目
            {

                for (int j = 1; j < 9; j++)
                {
                    
                    GameObject go = Instantiate(hexagonPrefab) as GameObject;
                    Piece piece = go.GetComponent<Piece>();
                    go.name = i.ToString() + "-" + j.ToString();
                    int[] p = { i, j };
                    objectPosition.Add(go, p);
                    go.transform.position = new Vector2(-9.1f + 1.52f * j, 12.02f - 1.37f * i);


                        
                    if (i == 1 || i == 15)
                    {
                        int m = Random.Range(1, 3);

                        if (2 <= j && j <= 7)
                        {
                            if (s1 >= startobj)
                            {
                                piece.ChangeSprite(2);
                                s2 += 1;
                                color[i, j] = 2;
                            }
                            else if (s2 >= startobj)
                            {
                                piece.ChangeSprite(1);
                                s1 += 1;
                                color[i, j] = 1;
                            }else{
                                if (m == 1)
                                {
                                    piece.ChangeSprite(1);
                                    s1 += 1;
                                    color[i, j] = 1;
                                }
                                else
                                {
                                    piece.ChangeSprite(2);
                                    s2 += 1;
                                    color[i, j] = 2;
                                }
                            }
                         }
                      }
                  }
            }

            else//2,4,6,8,10,12,14行目
            {

                for (int j = 1; j < 10; j++)
                {

                    GameObject go = Instantiate(hexagonPrefab) as GameObject;
                    go.name = i.ToString() + "-" + j.ToString();
                    int[] p = { i, j };
                    objectPosition.Add(go, p);
                    go.transform.position = new Vector2(-9.1f + 1.52f * j - 0.75f, 12.02f - 1.37f * i);
                    Piece piece = go.GetComponent<Piece>();

                    if (j == 1 || j == 9)
                    {

                        if (s1 >= startobj)
                        {
                            piece.ChangeSprite(2);
                            s2 += 1;
                            color[i, j] = 2;
                        }
                        else if (s2 >= startobj)
                        {
                            piece.ChangeSprite(1);
                            s1 += 1;
                            color[i, j] = 1;
                        }
                        else
                        {

                            int m = Random.Range(1, 3);

                            if (m == 1)
                            {
                                piece.ChangeSprite(1);
                                s1 += 1;
                                color[i, j] = 1;
                            }
                            else
                            {
                                piece.ChangeSprite(2);
                                s2 += 1;
                                color[i, j] = 2;
                            }
                        }
                    }
                }
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
                    Piece piece = obj.GetComponent<Piece>();
                    int[] p = objectPosition[obj];

                    if (color[p[0], p[1]] == 0)
                    {
                        
                        if (turn == 1)
                        {
                            piece.ChangeSprite(1);
                            piece.PlayParticle(1);
                            color[p[0], p[1]] = 1;
                            turn += 1;
                        }
                        else
                        {
                            piece.ChangeSprite(2);
                            piece.PlayParticle(2);
                            color[p[0], p[1]] = 2;
                            turn = 1;
                        }

                    }

                }

            }
        }


        // 三面が接するところのアルゴリズム
        //だいたい1秒ごとに処理を行う
        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            timeleft = 1.0f;

            //ここに処理
            for (int i = 1; i < 16; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 1; j < 10; j++)
                    {
                        if (color[i - 1, j - 1] == 1)
                        {
                            r1color[i,j] += 1;
                        }
                        else if (color[i - 1, j - 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i - 1, j] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i - 1, j] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i, j - 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i, j - 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i, j + 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i, j + 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i + 1, j] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i + 1, j] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i + 1, j - 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i + 1, j - 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                    }
                }

                if (i % 2 == 1)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        if (color[i - 1, j] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i - 1, j] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i - 1, j + 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i - 1, j + 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i, j - 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i, j - 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i, j + 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i, j + 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i + 1, j] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i + 1, j] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                        if (color[i + 1, j + 1] == 1)
                        {
                            r1color[i, j] += 1;
                        }
                        else if (color[i + 1, j + 1] == 2)
                        {
                            r2color[i, j] += 1;
                        }

                    }
                }
            }

            for (int i = 1; i < 16; i++){
                for (int j = 1; j < 10; j++){
                    if (r1color[i,j] >= 3 && color[i, j] == 0)
                    {

                        color[i, j] = 1;
                        this.n = i.ToString() + "-" + j.ToString();
                        this.ob = GameObject.Find(this.n);
                        Piece piece = ob.GetComponent<Piece>();
                        piece.ChangeSprite(1);
                        piece.PlayParticle(1);
                    }

                    if (r2color[i,j] >= 3 && color[i, j] == 0)
                    {

                        color[i, j] = 2;
                        this.n = i.ToString() + "-" + j.ToString();
                        this.ob = GameObject.Find(this.n);
                        Piece piece = ob.GetComponent<Piece>();
                        piece.ChangeSprite(2);
                        piece.PlayParticle(2);

                    }

                    r1color[i,j] = 0;
                    r2color[i,j] = 0;
                }
            }


        }

    }
}
