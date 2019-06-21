using UnityEngine;
using Assets.Scripts.Player;
using JetBrains.Annotations;
using TMPro;

namespace Assets.Scripts.Duck
{
    public class InfectedDuck : Duck
    {
        [Range(10, 100)] public int ScoreLoss = 100;

        public GameObject ParticlePrefab;

        private float DestroyDelay;

        [UsedImplicitly]
        private void Update()
        {
            if (Timer.Finished)
                return;

            if (transform.parent == Events.Parent)
            {
                Events.DisableInput();
                Pointer.HideLine();
                Pointer.HideReticule();

                DestroyDelay += Time.deltaTime;

                if (DestroyDelay > 1.0f)
                {
                    if (ScoreManager.TextMeshProUGUIComponent)
                        ScoreManager.Subtract(ScoreLoss);

                    Pointer.bHeld = false;
                    Events.EnableInput();
                    Pointer.ShowLine();
                    Pointer.ShowReticule();

                    Destroy(gameObject);
                }
                else
                {
                    if (!ParticlePrefab.GetComponent<ParticleSystem>().isPlaying)
                    {
                        ParticlePrefab = Instantiate(ParticlePrefab, transform);
                        ParticlePrefab.GetComponent<ParticleSystem>().Play();

                        ScoreHitPrefab = Instantiate(ScoreHitPrefab, transform);

                        TextComponent = ScoreHitPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                        TextComponent.color = Color.black;
                        TextComponent.text = "-" + ScoreLoss;
                        ScoreHitPrefab.transform.rotation =
                            Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) *
                            Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));
                    }
                }
            }
        }

        [UsedImplicitly]
        private void OnCollisionEnter(Collision Collision)
        {
            switch (Collision.transform.gameObject.tag)
            {
                case "Target":
                    ParticleHitPrefab =
                        Instantiate(ParticleHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                    ParticleHitPrefab.GetComponent<ParticleSystem>().Play();

                    if (Timer.Finished)
                        return;

                    int ScoreToSubtract = Collision.gameObject.GetComponent<Target>().Tier3;
                    ScoreManager.Subtract(ScoreToSubtract);

                    // Spawn the score hit UI element (for feedback)
                    ScoreHitPrefab = Instantiate(ScoreHitPrefab, Collision.contacts[0].point, Quaternion.identity);
                    TextComponent = ScoreHitPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                    TextComponent.color = Color.black;
                    TextComponent.text = "-" + ScoreToSubtract;
                    ScoreHitPrefab.transform.rotation =
                        Quaternion.LookRotation(Vector3.zero - ScoreHitPrefab.transform.position) *
                        Quaternion.Inverse(new Quaternion(0.0f, 180.0f, 0.0f, 1.0f));

                    break;

                case "Water":
                    Destroy(gameObject);
                    break;
            }
        }
    }
}