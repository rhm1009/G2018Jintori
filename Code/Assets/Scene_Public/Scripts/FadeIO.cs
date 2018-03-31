using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIO : MonoBehaviour {
    // In = true, Out = false
    public bool InOut;

    public float FadeTime;

    public Scene Before;

    Image Fade;

    GameObject Parent;

	void Start () {
        //InOut = false;
        //FadeTime = 1f;
        Fade = GetComponent<Image>();
        Before = SceneManager.GetActiveScene();

        Parent = GameObject.Find("Canvas(DontDestroy)");

    }

    void Update()
    {
        Scene After = SceneManager.GetActiveScene();
        
        if(name == "FadeIO(Clone)" || name == "FadeIO_Net(Clone)")
            transform.SetParent(Parent.transform);

        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);


        if(Before == After)
        {
            // What is this Scene? Fade Time is Changing for SceneIndex
            //  TO MENU SCENE
            if (After.buildIndex == 0)
            {
                // GETTING ERROR : Start Scene Fade In...
                //FadeTime = 4.0f;
            }
            //  TO GAME SCENE
            if (After.buildIndex == 1)
            {
                FadeTime = 4.0f;
            }
            //  TO RESULT SCENE
            if (After.buildIndex == 2)
            {
                FadeTime = 5.0f;
            }
        }
        // Changed Scene!
        else
        {
            InOut = true;

            // What is this Scene? Fade Time is Changing for SceneIndex
            //  TO MENU SCENE
            if (After.buildIndex == 0)
            {
                FadeTime = 3.0f;
            }
            //  TO GAME SCENE
            if (After.buildIndex == 1)
            {
                FadeTime = 5.0f;
            }
            //  TO RESULT SCENE
            if (After.buildIndex == 2)
            {
                FadeTime = 4.0f;
            }
        }

        // Fade Out
        if (!InOut)
        {
            Fade.color = new Color(
                Fade.color.r,
                Fade.color.g,
                Fade.color.b,
                Fade.color.a + FadeTime * 0.01f // Time.deltaTime
                );
        }
        // Fade In
        if (InOut)
        {
            if (Fade.color.a <= 0) Destroy(gameObject);
            else
            {
                Fade.color = new Color(
                    Fade.color.r,
                    Fade.color.g,
                    Fade.color.b,
                    Fade.color.a - FadeTime * 0.01f// Time.deltaTime
                    );
            }
        }
    }
}
