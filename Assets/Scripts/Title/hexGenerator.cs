using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hexGenerator : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public GameObject hexPrefab;

    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            GameObject go = Instantiate(hexPrefab) as GameObject;
            items.Add(go);
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            go.transform.position = new Vector2(Random.Range(45, 350), Random.Range(35, 610));

            int n = Random.Range(1, 5);

            if(n == 1){
                spriteRenderer.color = Color.yellow;
            }else if(n == 2){
                spriteRenderer.color = Color.white;
            }else if(n == 3){
                spriteRenderer.color = Color.green;
            }else{
                spriteRenderer.color = Color.red;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var i in items.ToArray()){
            
            this.rb = i.GetComponent<Rigidbody2D>();

            if(rb.velocity.magnitude < 10){
                items.Remove(i);
                Destroy(i);
                GameObject go = Instantiate(hexPrefab) as GameObject;
                items.Add(go);
                SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                go.transform.position = new Vector2(Random.Range(45, 350), Random.Range(35, 610));

                int n = Random.Range(1, 5);

                if (n == 1)
                {
                    spriteRenderer.color = Color.yellow;
                }
                else if (n == 2)
                {
                    spriteRenderer.color = Color.white;
                }
                else if (n == 3)
                {
                    spriteRenderer.color = Color.green;
                }
                else
                {
                    spriteRenderer.color = Color.red;
                }
            }
        }
       
    }
}