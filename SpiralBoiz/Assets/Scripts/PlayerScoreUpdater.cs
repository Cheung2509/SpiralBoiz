using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerScoreUpdater : MonoBehaviour {

    public List<int> player_score;


    List<int> current_players_score = new List<int>();

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            current_players_score.Add(0);
        }
    }

    public void updatePlayerScoreAmount(int current_player)
    {
        current_players_score[current_player] += 1;
    }

    public void saveNewScoreStats()
    {
        List<int> saved_vaules = getPreviousScores();

        StreamWriter writer = new StreamWriter("PlayerStat.txt", false);

        for (int i = 0; i <= 3; i++)
        {
            // Add new values to old values
            player_score[i] = saved_vaules[i] + current_players_score[i];
            writer.WriteLine(player_score[i]);
        }

        writer.Close();

    }

    List<int> getPreviousScores()
    {
        List<int> saved_vaules = new List<int>();
        List<string> string_text = new List<string>();
        StreamReader reader = new StreamReader("PlayerStat.txt");
        string whole_text_as_string = reader.ReadToEnd();
        string_text.AddRange(whole_text_as_string.Split("\n"[0]));

        for (int i = 0; i < string_text.Count; i++)
        {
            int candidate;
            int.TryParse(string_text[i], out candidate);
            saved_vaules.Add(candidate);
        }

        reader.Dispose();

        return saved_vaules;
    }
}
