using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {

    public CanvasGroup canvasGroup;
    public bool fadeOut = false;
    public bool fadeIn = false;

    public float TimeToFade;

    void Update(){
        if(fadeOut){
            if(canvasGroup.alpha > 0){
                canvasGroup.alpha -= Time.deltaTime * TimeToFade;
            }
            if(canvasGroup.alpha <= 0){
                fadeOut = false;
            }
        }

        if(fadeIn){
            if(canvasGroup.alpha < 1){
                canvasGroup.alpha += Time.deltaTime * TimeToFade;
            }
            if(canvasGroup.alpha >= 1){
                fadeIn = false;
            }
        }
    }

    public void FadeIn(){
        fadeIn = true;
    }

    public void FadeOut(){
        fadeOut = true;
    }
}
