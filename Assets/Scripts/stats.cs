using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text levelText;
    public Text attackText;
    public Text defenseText;

    private int level = 1;
    private int attack;
    private int defense;

    void Start()
    {
        // Initialize attack and defense based on the level
        UpdateStats();

        // Add a listener to the button
        // levelUpButton.onClick.AddListener(LevelUp);
    }

    void UpdateStats()
    {
        // Calculate stats based on level
        attack = level * 2;
        defense = level * 2;

        // Update the UI text
        levelText.text = "Level: " + level;
        // attackText.text = "Attack: " + attack;
        // defenseText.text = "Defense: " + defense;
    }

    void LevelUp()
    {
        // Increase the level by 1
        level += 1;
        UpdateStats();
    }
}
