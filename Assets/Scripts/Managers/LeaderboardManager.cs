using System;
using UnityEngine;
using JetBrains.Annotations;
using TMPro;

namespace Assets.Scripts.Managers
{
	public class LeaderboardManager : MonoBehaviour
	{
		public LeaderboardSlot LeaderboardSlot = null;

        [SerializeField][HideInInspector]
		private GameObject Entries;

		[Range(1, 10)]
		public int MaxSlots = 5;

		private float Offset;
		private const float Spacing = 1.0f;

		[UsedImplicitly]
		private void Start()
		{
			Entries = transform.GetChild(1).gameObject;
		}

		public void AddEntry(int Score)
		{
			if (Score == 0)
				return;

			// Check for duplicate scores
			for (int i = 0; i < Entries.transform.childCount; i++)
			{
				var ScoreFromOtherSlot = GetEntry(i).Score;
				if (Score == ScoreFromOtherSlot)
					return;
			}
			
			// Update the leaderboard
			for (int i = 0; i < Entries.transform.childCount; i++)
			{
				if (UpdateLeaderboard(Score, i))
				{
					ReOrder();
					
					return;
				}
			}

			// Create a new slot in the leaderboard
			if (Entries.transform.childCount < MaxSlots)
				CreateSlot(Score);

			ReOrder();
		}

		private void CreateSlot(int Score)
		{
			LeaderboardSlot Slot = Instantiate(LeaderboardSlot, Entries.transform);

			Slot.name = "Slot_" + Entries.transform.childCount;
			Slot.transform.position = Entries.transform.position;
			Slot.transform.position += new Vector3(0.0f, Offset, 0.0f);
			Slot.GetComponent<TextMeshPro>().text = Score.ToString();
			Slot.Score = Score;


			Offset -= Spacing;
		}

		private bool UpdateLeaderboard(int Score, int i)
		{
			// Get the score from the other slot
			var ScoreFromOtherSlot = GetEntry(i).Score;

			if (ScoreFromOtherSlot < Score)
			{
				// Push down the previous score
				if (Entries.transform.childCount < MaxSlots)
					CreateSlot(ScoreFromOtherSlot);

				// Set the new score
				UpdateEntry(i, Score);

				return true;
			}

			return false;
		}

		public LeaderboardSlot GetEntry(int Index)
		{
			return Entries.transform.GetChild(Index).GetComponent<LeaderboardSlot>();
		}

		public void UpdateEntry(int Index, int Score)
		{
			if (Entries.transform.childCount == 0)
				return;

			if (Index < Entries.transform.childCount)
			{
				Entries.transform.GetChild(Index).GetComponent<LeaderboardSlot>().Score = Score;
				Entries.transform.GetChild(Index).GetComponent<TextMeshPro>().text = Score.ToString();
			}
			else
			{
				Debug.Log("Index out of bounds: " + Index);
			}
		}

		public void DeleteEntry(int Index)
		{
			if (Entries.transform.childCount == 0)
				return;

			if (Index < Entries.transform.childCount)
			{
				Destroy(Entries.transform.GetChild(Index));
			}
		}

		private void ReOrder()
		{
			int[] ScoresFromOtherSlot = new int[Entries.transform.childCount];

			// Get all scores
			for (int i = 0; i < Entries.transform.childCount; i++)
			{
				ScoresFromOtherSlot[i] = GetEntry(i).Score;
			}

			// Sort and reverse
			Array.Sort(ScoresFromOtherSlot);
			Array.Reverse(ScoresFromOtherSlot);

			// Update each entry
			for (int i = 0; i < Entries.transform.childCount; i++)
			{
				UpdateEntry(i, ScoresFromOtherSlot[i]);
			}
		}
	}
}
