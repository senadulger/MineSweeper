using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper_220229053
{
    public class Skorboard
    {
        private List<PlayerScore> scores = new List<PlayerScore>();

        private static Skorboard instance;

        public Skorboard() { }

        public static Skorboard GetInstance()
        {
            if (instance == null)
            {
                instance = new Skorboard();
            }
            return instance;
        }

        public void AddScore(string userName, int correctFlags, int passedTime)
        {
            int score = 0;

            if (passedTime > 0)
            {
                score = (correctFlags * 1000) / passedTime;
            } 
            
            PlayerScore newScore = new PlayerScore(userName, score);
            scores.Add(newScore);
            scores = scores.OrderByDescending(s => s.Score).Take(10).ToList();

            if (scores.Count > 10)
            {
                scores.RemoveAt(0);
            }
        }

        public List<PlayerScore> GetScores()
        {
            return scores;
        }
    }

    public class PlayerScore
    {
        public string UserName { get; set; }
        public int Score { get; set; }

        public PlayerScore(string userName, int score)
        {
            UserName = userName;
            Score = score;
        }
    }
}

