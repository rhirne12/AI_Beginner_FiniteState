using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;  // allows us to order the checkpoitns

public sealed class GameEnvironment
{
    private static GameEnvironment instance;
    private List<GameObject> checkpoints = new List<GameObject>();

    public List<GameObject> Checkpoints {  get { return checkpoints; } }

    public static GameEnvironment Singleton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnvironment();
                instance.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                // put checkpoints in order so the patrol is in a circuit around the building
                instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList();

            }
            return instance;
        }
    }
}
