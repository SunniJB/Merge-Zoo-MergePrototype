using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayLeaderboard : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardEntryPrefab;
    private List<GameObject> leaderboardEntries = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(GetScores());
    }

    private IEnumerator GetScores()
    {
        yield return new WaitForSeconds(2f);

        Leaderboard.GetScores();
        StartCoroutine(PopulateLeaderboard());
    }

    private IEnumerator PopulateLeaderboard()
    {
        yield return new WaitForSeconds(2f);

        // Wait for the leaderboard data to be fetched
        yield return new WaitUntil(() => Leaderboard.Scores != null);

        // Clear the existing leaderboard entries
        foreach (var entry in leaderboardEntries)
        {
            Destroy(entry);
        }
        leaderboardEntries.Clear();

        // Create a new leaderboard entry for each player
        foreach (var score in Leaderboard.Scores)
        {
            var entry = Instantiate(leaderboardEntryPrefab, transform);

            // Split the score string into rank, player name, and score
            var scoreParts = score.Split(',');
            var playerName = scoreParts[1].Split(':')[1].Trim();
            var playerScore = scoreParts[2].Split(':')[1].Trim();

            // Remove the hashtag and any characters following it in the username
            playerName = playerName.Split('#')[0];

            // Find the TMP_Text components in the leaderboardEntryPrefab
            var usernameText = entry.transform.Find("user_username").GetComponent<TMP_Text>();
            var levelText = entry.transform.Find("user_Level").GetComponent<TMP_Text>();

            // Set the text of the TMP_Text components
            usernameText.text = playerName;
            levelText.text = "Lvl. " + playerScore;


            leaderboardEntries.Add(entry);
        }
    }
}