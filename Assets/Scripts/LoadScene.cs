using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Slider prograssbar;
    public Text loadtext;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("loadScene");
    }

    IEnumerator loadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Play");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if (prograssbar.value < .9f){
                prograssbar.value = Mathf.MoveTowards(prograssbar.value, .9f, Time.deltaTime);
            }

            else if(prograssbar.value >= .9f){
                prograssbar.value = Mathf.MoveTowards(prograssbar.value, 1f, Time.deltaTime);
            }
            
            if (prograssbar.value >= 1f)
            {
                loadtext.text = "로딩 완료";
                if(operation.progress >= .9f)        {
                    operation.allowSceneActivation = true;
                }
            }
            
        }
    }
}
