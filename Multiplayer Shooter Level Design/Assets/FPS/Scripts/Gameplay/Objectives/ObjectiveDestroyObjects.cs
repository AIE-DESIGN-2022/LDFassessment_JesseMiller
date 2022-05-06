using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveDestroyObjects : Objective
    {
        [Tooltip("Visible transform that will be destroyed once the objective is completed")]
        public Transform DestroyRoot;
        public int amtLeft;

        void Awake()
        {
            Title = "Find the hidden Bears";
            UpdateObjective("Bears remaining:", amtLeft.ToString() + "/6" , string.Empty);
            if (DestroyRoot == null)
                DestroyRoot = transform;
        }
        void Update()
        {
            
        }

        public void TriggerObjective()
        {
            if (IsCompleted) return;
            CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);
            Destroy(DestroyRoot.gameObject);
        }

        public void SetAmountOfBears(int amountLeft)
        {
            amtLeft = amountLeft;

            string notificationText = amtLeft.ToString() + " bears left";
            Title = amtLeft.ToString() + " bears left";
            UpdateObjective("Bears remaining:", amtLeft.ToString() + "/6" , notificationText);
            

            if(amtLeft <= 0)
            {
                TriggerObjective();
            }
        }
    }
}