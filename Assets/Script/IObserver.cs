public interface IObserver
{
    public void OnNotify(PlayerAction action);
    public void OnNotify(BossAction action);
}
