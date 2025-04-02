public interface IRayEventReceiver
{

    bool CanReceiveRays();

    void OnRaycastEnter();
    void OnRaycastExit();

    void Activate();

    void DeActivate();
}