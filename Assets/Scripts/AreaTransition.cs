using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Vector2 newSpawn = spawnPosition;
        newSpawn.y = other.transform.position.y;

        PlayerSpawnManager.spawnPosition = newSpawn;
        SceneManager.LoadScene(sceneToLoad);
    }
}
}