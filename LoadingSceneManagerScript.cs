using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class InitialLoadingScreen : MonoBehaviour
{
    public Image loadingImage; // Reference to the Image component
    public float minDisplayTime = 2f; // Minimum display time for the image

    void Start()
    {
        StartCoroutine(LoadMainScene()); // Start the coroutine to load the main scene
    }

    private IEnumerator LoadMainScene()
    {
        // Show the loading image
        loadingImage.gameObject.SetActive(true); // Make sure the image is visible

        // Wait for the minimum display time
        float startTime = Time.time;
        while (Time.time - startTime < minDisplayTime)
        {
            yield return null; // Wait until the next frame
        }

        // Load the main scene
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");

        // While the scene is still loading
        while (!operation.isDone)
        {
            // Optionally, you can add loading progress updates here
            yield return null; // Wait until the next frame
        }
    }
}
