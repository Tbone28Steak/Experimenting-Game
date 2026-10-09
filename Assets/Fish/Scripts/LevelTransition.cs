using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string transitionScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider obj) {
        Debug.Log("entered:" + obj);
        if(obj.tag == "Player") {
            SceneManager.LoadScene(transitionScene, LoadSceneMode.Single);
        }
        
    }
}
