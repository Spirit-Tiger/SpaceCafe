using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;
    public TMP_InputField NameInput;

    public TextMeshProUGUI LeaderboardSlot1;
    public TextMeshProUGUI LeaderboardSlot2;
    public TextMeshProUGUI LeaderboardSlot3;

    public TextMeshProUGUI LeaderboardSlot1_1;
    public TextMeshProUGUI LeaderboardSlot2_1;
    public TextMeshProUGUI LeaderboardSlot3_1;

    public TextMeshProUGUI Warning1;
    public TextMeshProUGUI Warning2;

    public string Slot1Text;
    public string Slot2Text;
    public string Slot3Text;

    public float Slot1 = 0;
    public float Slot2 = 0;
    public float Slot3 = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetLeaderts(string name, float score)
    {
        if (Slot1 < score)
        {
            if (Slot2 > 0)
            {
                Slot3 = Slot2;
                Slot2 = Slot1;

                LeaderboardSlot3.text = Slot2Text;
                LeaderboardSlot3_1.text = Slot2Text;

                Slot3Text = LeaderboardSlot3.text;

                LeaderboardSlot2.text = Slot1Text;
                LeaderboardSlot2_1.text = Slot1Text;

                Slot2Text = LeaderboardSlot2.text;
             
               
            }

            else if (Slot1 > 0)
            {

                Slot2 = Slot1;
                LeaderboardSlot2.text = Slot1Text;
                LeaderboardSlot2_1.text = Slot1Text;

                Slot2Text = LeaderboardSlot2.text;
            }

            Slot1 = score;
            LeaderboardSlot1.text = name + " : " + score.ToString();
            LeaderboardSlot1_1.text = name + " : " + score.ToString();

            Slot1Text = LeaderboardSlot1.text; 



        }
        else if (Slot2 < score)
        {
            if (Slot1 == score)
            {
                return;
            }

            if (Slot2 > 0)
            {
                Slot3 = Slot2;
                LeaderboardSlot3.text = Slot2Text;
                LeaderboardSlot3_1.text = Slot2Text;
            }

            Slot2 = score;
            LeaderboardSlot2.text = name + " : " + score.ToString();
            LeaderboardSlot2_1.text = name + " : " + score.ToString();

            Slot2Text = LeaderboardSlot2.text;

        }
        else if (Slot3 < score)
        {
            if (Slot2 == score)
            {
                return;
            }

            Slot3 = score;
            LeaderboardSlot3.text = name + " : " + score.ToString();
            LeaderboardSlot3_1.text = name + " : " + score.ToString();


            Slot3Text = LeaderboardSlot3.text;
        }
        else
        {
            Warning1.gameObject.SetActive(true);
            Warning2.gameObject.SetActive(true);
        }
    }
}
