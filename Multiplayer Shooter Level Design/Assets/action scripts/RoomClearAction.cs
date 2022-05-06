using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.FPS.Gameplay;

public class RoomClearAction : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public UnityEvent OnClearEvent;
    private bool triggered;

    public bool isBears;

    private void Update()
    {
        if (triggered) return;
        var enemiesTemp = enemies.ToArray();
        foreach (var item in enemiesTemp)
        {
            if (item == null)
            {
                enemies.Remove(item);
                 if(isBears)
                {
                     CallBearDeath();   
                }
            }
        }
        
        if (enemies.Count == 0)
        {
            triggered = true;
            OnClearEvent.Invoke();
        }
        
    }

    void CallBearDeath()
    {
GetComponent<ObjectiveDestroyObjects>().SetAmountOfBears(enemies.Count);
    }

}
