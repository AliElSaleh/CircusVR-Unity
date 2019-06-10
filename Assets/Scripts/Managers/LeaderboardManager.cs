using System;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;

namespace Assets.Scripts.Managers
{
	public class LeaderboardManager : MonoBehaviour
	{
		public LeaderboardSlot LeaderboardSlot = null;

		private GameObject Entries;

		private const int MaxSlots = 5;
		private float Offset;
		private const float Spacing = 1.0f;

		[UsedImplicitly]
		private void Start()
		{
			Entries = transform.GetChild(1).gameObject;
		}

		public void AddEntry(int Score)
		{
			if (Entries.transform.childCount == MaxSlots)
			{
				for (int i = 0; i < MaxSlots; i++)
				{
					if (UpdateLeaderboard(Score, i))
						break;
				}

				return;
			}

			LeaderboardSlot Slot = Instantiate(LeaderboardSlot, Entries.transform);

			Slot.transform.position = Entries.transform.position;
			Slot.transform.position += new Vector3(0.0f, Offset, 0.0f);
			Slot.GetComponent<TextMeshPro>().text = Score.ToString();

			for (int i = 0; i < Entries.transform.childCount; i++)
			{
				var ScoreFromOtherSlot = int.Parse(GetEntry(i).GetComponent<TextMeshPro>().text);

				if (ScoreFromOtherSlot < Score)
				{
					if (UpdateLeaderboard(Score, i))
						break;
				}
			}

			Offset -= Spacing;
		}

		private bool UpdateLeaderboard(int Score, int i)
		{
			var ScoreFromOtherSlot = int.Parse(GetEntry(i).GetComponent<TextMeshPro>().text);

			if (ScoreFromOtherSlot < Score)
			{
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
				Entries.transform.GetChild(Index).GetComponent<TextMeshPro>().text = Score.ToString();
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
	}
}
