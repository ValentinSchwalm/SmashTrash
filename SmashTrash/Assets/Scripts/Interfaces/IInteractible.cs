using UnityEngine;

public interface IInteractible
{
    /// <summary>
    /// Is called when the player interacts with the object.
    /// </summary>
    void OnInteract(Vector3 pointOfImpact);
}
