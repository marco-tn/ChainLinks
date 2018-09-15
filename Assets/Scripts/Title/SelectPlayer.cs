using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{

    public GameObject startObj;
    public GameObject title;
    public GameObject selectObj;
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        startObj.SetActive(true);
        title.SetActive(true);
        selectObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPressStart(){
        
        startObj.SetActive(false);
        title.SetActive(false);
        selectObj.SetActive(true);
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void OnPressSelect(int players){

        audioSource.PlayOneShot(audioSource.clip);
        SceneManager.LoadScene("Scene" + players);

    }


}
