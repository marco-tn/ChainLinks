using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public ParticleSystem particle;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;

    public  void ChangeSprite(int c){
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(c == 1){
            spriteRenderer.color = Color.black;
        }else if(c == 2){
            spriteRenderer.color = Color.yellow;
        }else if(c == 3){
            spriteRenderer.color = Color.green;
        }else{
            spriteRenderer.color = Color.red;
        }
    }

    public void PlayParticle(int c){

        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        ParticleSystem.MainModule par = GetComponent<ParticleSystem>().main;

        if (c == 1)
        {
            par.startColor = Color.black;
        }
        else if (c == 2)
        {
            par.startColor = Color.yellow;
        }
        else if (c == 3)
        {
            par.startColor = Color.green;
        }
        else
        {
            par.startColor = Color.red;
        }

        particle.Play();
        audioSource.PlayOneShot(audioSource.clip);
    }
}
