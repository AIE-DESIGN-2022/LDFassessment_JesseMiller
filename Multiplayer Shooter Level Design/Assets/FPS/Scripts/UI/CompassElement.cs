using Unity.FPS.Game;
using UnityEngine;
using Unity.FPS.AI;

namespace Unity.FPS.UI
{
    public class CompassElement : MonoBehaviour
    {
        [Tooltip("The marker on the compass for this element")]
        public CompassMarker CompassMarkerPrefab;

        [Tooltip("Text override for the marker, if it's a direction")]
        public string TextDirection;

        Compass m_Compass;
        Objective objective;
        bool objectiveRegistered = false;
        public Transform player;
        float distanceToPlayer = 100;
        public bool markerOn;
        bool isEnemy;

        void Awake()
        {
            //Objective.OnObjectiveActivated += ObjectiveRegistration;
            if(GetComponent<EnemyController>())
            {
                isEnemy = true;
            }

            m_Compass = FindObjectOfType<Compass>();
            DebugUtility.HandleErrorIfNullFindObject<Compass, CompassElement>(m_Compass, this);
            player = GameObject.FindWithTag("Player").transform;
            objective = GetComponent<Objective>();
            if (objective != null) return;
            float dist = Vector3.Distance(this.transform.position, player.transform.position);
            if ( dist < distanceToPlayer && isEnemy)
            {
                RegistrationLogic();
            }
            else if(!isEnemy)
            {
                RegistrationLogic();
            }
        }

        private void Update()
        {
            float dist = Vector3.Distance(this.transform.position, player.transform.position);
        

            if (objective != null)
            {
                if (objective.IsActivated() && !objectiveRegistered)
                {
                    
                    RegistrationLogic();
                    objectiveRegistered = true;
                }
            }
            if(dist < distanceToPlayer && !markerOn && isEnemy)
            {
                markerOn = true;
                RegistrationLogic();
            }
            else if(dist > distanceToPlayer && markerOn && isEnemy)
            {
                markerOn = false;
                OnDestroy();
            }
           

        }

        private void RegistrationLogic()
        {
            var markerInstance = Instantiate(CompassMarkerPrefab);

            markerInstance.Initialize(this, TextDirection);
            m_Compass.RegisterCompassElement(transform, markerInstance);
        }

        private void ObjectiveRegistration(Objective an_objective)
        {
            RegistrationLogic();
        }

        void OnDestroy()
        {
            m_Compass.UnregisterCompassElement(transform);
            //Objective.OnObjectiveActivated -= ObjectiveRegistration;
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}