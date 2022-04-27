using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UI;
namespace Unity.FPS.Game
{
    public class ObjectHealthBar : MonoBehaviour
    {
        public ObjectHealth objectHealth;

        [Tooltip("Image component displaying health left")]
        public Image HealthBarImage;

        [Tooltip("The floating healthbar pivot transform")]
        public Transform HealthBarPivot;

        [Tooltip("Whether the health bar is visible when at full health or not")]
        public bool HideFullHealthBar = true;

        private void Start()
        {
            if (objectHealth == null)
                objectHealth = GetComponent<ObjectHealth>();
        }

        void Update()
        {
            HealthBarImage.fillAmount = objectHealth.CurrentHealth / objectHealth.MaxHealth;

            // rotate health bar to face the camera/player
            HealthBarPivot.LookAt(Camera.main.transform.position);

            // hide health bar if needed
            if (HideFullHealthBar)
                HealthBarPivot.gameObject.SetActive(HealthBarImage.fillAmount != 1);
        }
    }
}

