using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{

    public GameObject hexagonPrefab;

    int b0 = 0;

    int b6 = 0;

    int y0 = 0;

    int y6 = 0;

    int[,] color = new int[9, 13];

    public Dictionary<GameObject, int[]> objectPosition = new Dictionary<GameObject, int[]>();

    bool myTurn = true;

    int sumb = 0;

    int sumy = 0;

    public GameObject ob;

    public string name;

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < 7; i++)
        {
            if (i % 2 == 0)//1,3,5,7行目
            {

                for (int j = 0; j < 10; j++)
                {
                    // 六角形の生成
                    GameObject go = Instantiate(hexagonPrefab) as GameObject;
                    go.name = (i + 1).ToString() + (j + 1).ToString();
                    int[] p = { i + 1, j + 1 };
                    objectPosition.Add(go, p);
                    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                    go.transform.position = new Vector2(-7.5f + 1.5f * j, 4 - 1.35f * i);

                    if (i == 0 || i == 6)
                    {
                        int n = Random.Range(1, 3);
                        if (1 <= j && j <= 8)
                        {
                            if (n == 1)
                            {
                                if (i == 0)
                                {
                                    if (b0 < 4)
                                    {
                                        spriteRenderer.color = Color.black;
                                        b0 += 1;
                                        color[i + 1, j + 1] = 1;
                                    }
                                    else
                                    {
                                        spriteRenderer.color = Color.yellow;
                                        y0 += 1;
                                        color[i + 1, j + 1] = 2;
                                    }
                                }
                                else
                                {
                                    if (b6 < 4)
                                    {
                                        spriteRenderer.color = Color.black;
                                        b6 += 1;
                                        color[i + 1, j + 1] = 1;
                                    }
                                    else
                                    {
                                        spriteRenderer.color = Color.yellow;
                                        y6 += 1;
                                        color[i + 1, j + 1] = 2;
                                    }
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    if (y0 < 4)
                                    {
                                        spriteRenderer.color = Color.yellow;
                                        y0 += 1;
                                        color[i + 1, j + 1] = 2;
                                    }
                                    else
                                    {
                                        spriteRenderer.color = Color.black;
                                        b0 += 1;
                                        color[i + 1, j + 1] = 1;
                                    }
                                }
                                else
                                {
                                    if (y6 < 4)
                                    {
                                        spriteRenderer.color = Color.yellow;
                                        y6 += 1;
                                        color[i + 1, j + 1] = 2;
                                    }
                                    else
                                    {
                                        spriteRenderer.color = Color.black;
                                        b6 += 1;
                                        color[i + 1, j + 1] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else//2,4,6行目
            {
                bool b = false;

                for (int j = 0; j < 11; j++)
                {

                    GameObject go = Instantiate(hexagonPrefab) as GameObject;
                    go.name = (i + 1).ToString() + (j + 1).ToString();
                    int[] p = { i + 1, j + 1 };
                    objectPosition.Add(go, p);
                    go.transform.position = new Vector2(-7.5f + 1.5f * j - 0.75f, 4 - 1.35f * i);
                    SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                    int m = Random.Range(1, 3);

                    if (j == 0 && m == 1)
                    {
                        spriteRenderer.color = Color.black;
                        b = true;
                        color[i + 1, j + 1] = 1;
                    }
                    else if (j == 0 && m == 2)
                    {
                        spriteRenderer.color = Color.yellow;
                        color[i + 1, j + 1] = 2;
                    }
                    if (j == 10)
                    {
                        if (b)
                        {
                            spriteRenderer.color = Color.yellow;
                            color[i + 1, j + 1] = 2;
                        }
                        else
                        {
                            spriteRenderer.color = Color.black;
                            color[i + 1, j + 1] = 1;
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
                        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
                        int[] p = objectPosition[obj];

                        if (color[p[0], p[1]] == 0)
                        {
                            if (myTurn)
                            {
                                spriteRenderer.color = Color.black;
                                color[p[0], p[1]] = 1;
                                myTurn = false;
                            }
                            else
                            {
                                spriteRenderer.color = Color.yellow;
                                color[p[0], p[1]] = 2;
                                myTurn = true;
                            }
                        }
                    }

                }
            }


            // 三面が接するところのアルゴリズム
            for (int i = 1; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 1; j < 12; j++)
                    {
                        if (color[i - 1, j - 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i - 1, j - 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i - 1, j] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i - 1, j] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i, j - 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i, j - 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i, j + 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i, j + 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i + 1, j] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i + 1, j] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i + 1, j - 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i + 1, j - 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (sumb >= 3 && color[i, j] == 0)
                        {

                            color[i, j] = 1;
                            this.name = i.ToString() + j.ToString();
                            this.ob = GameObject.Find(this.name);
                            SpriteRenderer spriteRenderer = ob.GetComponent<SpriteRenderer>();
                            spriteRenderer.color = Color.black;
                        }

                        if (sumy >= 3 && color[i, j] == 0)
                        {

                            color[i, j] = 2;
                            this.name = i.ToString() + j.ToString();
                            this.ob = GameObject.Find(this.name);
                            SpriteRenderer spriteRenderer = ob.GetComponent<SpriteRenderer>();
                            spriteRenderer.color = Color.yellow;

                        }

                        sumb = 0;
                        sumy = 0;

                    }
                }

                if (i % 2 == 1)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        if (color[i - 1, j + 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i - 1, j + 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i - 1, j] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i - 1, j] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i, j - 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i, j - 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i, j + 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i, j + 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i + 1, j] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i + 1, j] == 2)
                        {
                            sumy += 1;
                        }

                        if (color[i + 1, j + 1] == 1)
                        {
                            sumb += 1;
                        }
                        else if (color[i + 1, j + 1] == 2)
                        {
                            sumy += 1;
                        }

                        if (sumb >= 3 && color[i, j] == 0)
                        {
                            color[i, j] = 1;
                            this.name = i.ToString() + j.ToString();
                            this.ob = GameObject.Find(this.name);
                            SpriteRenderer spriteRenderer = ob.GetComponent<SpriteRenderer>();
                            spriteRenderer.color = Color.black;

                        }

                        if (sumy >= 3 && color[i, j] == 0)
                        {
                            color[i, j] = 2;
                            this.name = i.ToString() + j.ToString();
                            this.ob = GameObject.Find(this.name);
                            SpriteRenderer spriteRenderer = ob.GetComponent<SpriteRenderer>();
                            spriteRenderer.color = Color.yellow;

                        }

                        sumb = 0;
                        sumy = 0;
                    }
                }
            }

        }
}