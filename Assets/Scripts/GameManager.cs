// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public int world {get; private set;}
    public int stage {get; private set;}
    public int lives {get; private set;}

    private void Awake() {
        if(Instance != null) {
            DestroyImmediate(gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy() {
        if(Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        NewGame();
    }

    private void NewGame() {
        lives = 3;
        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage) {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    private void ResetLevel(int delay) {
        Invoke(nameof(ResetLevel), delay);
    }

    private void ResetLevel() {
        lives--;

        if(lives > 0) {
            LoadLevel(world, stage);
        }else {
            GameOver();
        }
    }

    private void NewLevel() {
        // To load world after clear all stages
        // if(world == 1 && stage == /*Number of stage*/) {
        //     LoadLevel(world + 1, 1);
        // }
        //Load stages
        LoadLevel(world, stage + 1);
    }

    private void GameOver() {
        NewGame();
    }
}
