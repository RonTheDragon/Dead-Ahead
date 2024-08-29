[System.Serializable]
public class KillInfo
{
    public enum KillType { Crash, Killed, Runover, Boss}
    public KillType KilledBy;
    public string DeathMsg;
    public int Reward;
}
