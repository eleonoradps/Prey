using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
   [SerializeField] int ennemiToSpawn = 0;
    int ennemiNumber;
    int currentWave = 0;
    [SerializeField] float waveTimer = 0;
    float waveTime = 0;
    [SerializeField] GameObject prefabEnnemi;
    [SerializeField] Transform ennemiSpawnPoint;
    [SerializeField] WaveDisplayer waveDisplayer;
    bool allDead = false;
    void Start()
    {
    }
    enum State
    {
        ORIGINAL_WAVE,
        PREPARING_NEXT_WAVE,
        SPAWN_ENNEMIS,
        CHECK_ENNEMIS_DEATH
    }
    State state = State.ORIGINAL_WAVE;
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.ORIGINAL_WAVE:
                currentWave = 1;
                waveDisplayer.DisplayCurrentWave(currentWave);
                ennemiToSpawn = 1;
                waveTime += waveTimer;
                state = State.SPAWN_ENNEMIS;
                break;
            case State.PREPARING_NEXT_WAVE:
                currentWave += 1;
                waveDisplayer.DisplayCurrentWave(currentWave);
                ennemiToSpawn++;
                waveTime += waveTimer;
                allDead = false;
                state = State.SPAWN_ENNEMIS;
                break;
            case State.SPAWN_ENNEMIS:
               // Debug.Log(ennemiToSpawn);
                for (int i = 0; i < ennemiToSpawn; i++)
                {
                    GameObject ennemi = Instantiate(prefabEnnemi, ennemiSpawnPoint);
                    ennemiNumber++;
                }
                state = State.CHECK_ENNEMIS_DEATH;
                break;
            case State.CHECK_ENNEMIS_DEATH:
               // Debug.Log(ennemiNumber);
                if (allDead)
                {
                    waveTime -= Time.deltaTime;
                    if (waveTime <= 0)
                    {
                        state = State.PREPARING_NEXT_WAVE;
                    }
                }
                break;

        }
    }

    public void ennemiDeath()
    {
        ennemiNumber--;
        if(ennemiNumber<=0)
        {
            allDead = true;
        }
    }
}
