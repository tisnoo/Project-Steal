using Unity.Netcode;

public class PlayersManager {
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame{
        get {
            return playersInGame.Value;
        }
    }
    
    private void Start()
    {
        // Keep player connected info.
    }


}