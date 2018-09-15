using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexController : MonoBehaviour {

    public Rigidbody2D rb;

    public Vector3 newVel;

	// Use this for initialization
	void Start () {

        this.rb = GetComponent< Rigidbody2D > ();

        this.newVel = rb.velocity;

        newVel.x = Random.Range(-100f, 100f);

        int n = Random.Range(1, 3);

        if(n == 1){

            newVel.y = Mathf.Sqrt(10000 - Mathf.Pow(newVel.x, 2));
        }else{
            
            newVel.y = -Mathf.Sqrt(10000 - Mathf.Pow(newVel.x, 2));
        }

        rb.velocity = newVel;


	}
	
	// Update is called once per frame
	void Update () {
        
        if (rb.transform.position.x > 320|| rb.transform.position.x < 25)
        {

            this.newVel = rb.velocity;

            newVel.x *= -1;

            rb.velocity = newVel;

        }

        if(rb.transform.position.y > 630 || rb.transform.position.y < 15){

            this.newVel = rb.velocity;

            newVel.y *= -1;

            rb.velocity = newVel;
        }

	}
}
