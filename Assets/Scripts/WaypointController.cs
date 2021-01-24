using UnityEngine;

public class WaypointController : MonoBehaviour
{
    #region Fields

    [SerializeField] public Vector3[] waypoints;

    #endregion Fields

    #region MonoBehaviour

    ///TODO: Create simple editor for waypoints
    private void OnDrawGizmos()
    {
        if(waypoints != null)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if(i == 0)
                {
                    Gizmos.color = Color.green;
                }

                if (i == waypoints.Length-1)
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawSphere(waypoints[i], 0.3f);

                if (i != waypoints.Length - 1)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
                }
            }
        }
    }

    #endregion MonoBehaviour
}
