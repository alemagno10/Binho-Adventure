using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour {
    private FadeInOut fade;

    void Start(){
        fade = FindObjectOfType<FadeInOut>();
    }

    public IEnumerator ChangeScene(string sceneName){
        fade.FadeIn();
        yield return new WaitForSeconds(fade.TimeToFade);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Next(string sceneName){
        StartCoroutine(ChangeScene(sceneName));
    }
}
