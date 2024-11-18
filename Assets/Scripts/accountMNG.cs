using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ACCOUNT_DATA
{
    public long userID; //will be used later for login functionality
    public string username = "testUser";
    public float totalKM;

    public int level = 1;
    public int XP = 0;

    public Dictionary<string, int> stats = new Dictionary<string, int>()
    {
        { "ATK", 15 },
        { "DEF", 10 },
        { "SPD", 10 },
        { "MATK", 12 },
        { "MDEF", 9 },
    };

    private bool addXP(int xp)
    {
        XP += xp;

        bool didLevelUp = false;
        while (requiredXP() <= XP) {
            XP -= requiredXP();
            levelUp();
            didLevelUp = true;
        }

        return didLevelUp;
    }

    public int maxHealthFetch() {
        return level * 25;
    }

    private void levelUp()
    {
        level += 1;
        foreach (var keys_ in stats.Keys) {
            stats[keys_] += Random.Range(1, 3);
        }
        
        accountMNG._instance.updateText();
    }

    private int requiredXP()
    {
        return ((level * 50) ^ 2) / 10;
    }
    
    public ACCOUNT_DATA() {
        // Load account data if there is any (database will be created at the very end)
    }
}

public class accountMNG : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> guiReplaceText;

    public static accountMNG _instance;
    private ACCOUNT_DATA data;

    private void Start()
    {
        if (_instance != null) return;
        data = new ACCOUNT_DATA(); // load should be done in the constructor later.
        _instance = this; // Save as static reference for easier use later -Lila
        
        updateText();
    } 

    public void updateText()
    {
        foreach (var _text in guiReplaceText) {
            foreach (var key_ in data.stats.Keys) {
                _text.text = _text.text.Replace("{" + key_ + "}", data.stats[key_].ToString());
            }

            _text.text = 
                _text.text.Replace("{USERNAME}", data.username)
                    .Replace("{TOTAL}", data.totalKM.ToString(CultureInfo.InvariantCulture))
                    .Replace("{LEVEL}", data.level.ToString());
        }
    }
}
