using UnityEngine;
using Rewired;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] MainMenu mainMenu;
    // Rewired Stuff
    [SerializeField] int playerId = 0;
    private Player player;
    private void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
    }
    void Update()
    {
        if (player.GetAnyButtonDown() && gameObject.activeInHierarchy)
        {
            mainMenu.ActivateMainMenu();
        }
    }
}
