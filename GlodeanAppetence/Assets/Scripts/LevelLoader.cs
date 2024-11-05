using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float animationTime = 1f;

    // Start is called before the first frame update

    public IEnumerator LoadLevel(string loadLevelName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(loadLevelName);
    }
}
