using UnityEngine;
using JetBrains.Annotations;
using TMPro;

namespace Assets.Scripts.Duck
{
    public class Duck : MonoBehaviour
    {
        protected Rigidbody Rigidbody;

        [Range(1, 1000)] [HideInInspector] public float LaunchForce = 300.0f;

        [Range(1, 200)] [HideInInspector] public float ThrowForce = 30.0f;

        public GameObject ParticleHitPrefab;
        public GameObject ScoreHitPrefab;

        protected TextMeshProUGUI TextComponent;
        protected float FontSize = 100.0f;

        [UsedImplicitly]
        protected void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.AddForce(Vector3.up * LaunchForce);

            TextComponent = ScoreHitPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            FontSize = TextComponent.fontSize;
        }

        public void SetPhysicalMaterial(PhysicMaterial NewPhysicMaterial)
        {
            GetComponent<SphereCollider>().material = NewPhysicMaterial;
        }

        [UsedImplicitly]
        private void OnCollisionEnter(Collision Collision)
        {
            switch (Collision.transform.gameObject.tag)
            {
                case "Target":
                    ParticleHitPrefab = Instantiate(ParticleHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                    ParticleHitPrefab.GetComponent<ParticleSystem>().Play();

                    if (Timer.Finished)
                        return;

                    float Distance = Vector3.Distance(Collision.gameObject.transform.position,
                        Collision.contacts[0].point);

                    if (Distance < 0.3f)
                    {
                        int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier3;
                        ScoreManager.Add(ScoreToAdd);

                        // Spawn the score hit UI element (for feedback)
                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        TextComponent.color = Color.yellow;
                        TextComponent.text = "+" + ScoreToAdd;
                        ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }
                    else if (Distance > 0.3f && Distance < 0.5f)
                    {
                        int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier2;
                        ScoreManager.Add(ScoreToAdd);

                        // Spawn the score hit UI element (for feedback)
                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        TextComponent.color = Color.yellow;
                        TextComponent.text = "+" + ScoreToAdd;
                        ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }
                    else
                    {
                        int ScoreToAdd = Collision.gameObject.GetComponent<Target>().Tier1;

                        ScoreManager.Add(ScoreToAdd);

                        // Spawn the score hit UI element (for feedback)
                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                        TextComponent.color = Color.yellow;
                        TextComponent.text = "+" + ScoreToAdd;
                        ScoreHitPrefab.transform.rotation = Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) * Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }

                    break;

                case "Water":
                    Destroy(gameObject); // Destroy duck
                    break;
            }
        }
    }
}
