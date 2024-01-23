using Photon.Pun;
using UnityEngine;

namespace MonoSandbox.Behaviours
{
    /// <summary>
    /// A base used for placing objects with MonoSandbox
    /// </summary>
    public class PlacementHandling : MonoBehaviourPunCallbacks
    {
        public float Offset = 4f;
        public bool IsEditing, IsActivated, Placed;
        public GameObject Cursor, SandboxContainer;
        public string _Name;

        public virtual GameObject CursorRef { get; }
        [PunRPC]
        public virtual void Activated(Vector3 normal, Vector3 point, string name)
        {
            photonView.RPC("Spawn" + _Name, RpcTarget.AllViaServer, normal, point, this.ToString());
            HapticManager.Haptic(HapticManager.HapticType.Create);
        }

        public virtual void DrawCursor(RaycastHit hitInfo)
        {

        }

        private void Start()
        {
            SandboxContainer = RefCache.SandboxContainer;
        }

        private void Update()
        {
            if (!SandboxContainer)
            {
                SandboxContainer = RefCache.SandboxContainer;
            }

            if (IsEditing && !Cursor)
            {
                Cursor = CursorRef;
                Cursor.GetComponent<Renderer>().material = new Material(RefCache.Selection);
            }
            else if (!IsEditing && Cursor)
            {
                Destroy(Cursor);
            }

            RaycastHit hitInfo = RefCache.Hit;
            if (IsEditing && Cursor)
            {
                if (Cursor.activeSelf != RefCache.HitExists) Cursor.SetActive(RefCache.HitExists);
                Cursor.GetComponent<Renderer>().material.color = new Color(0.392f, 0.722f, 0.820f, 0.4509804f);

                if (RefCache.HitExists)
                    DrawCursor(hitInfo);

                IsActivated = InputHandling.RightPrimary;
                if (IsActivated && !Placed)
                {
                    Placed = true;
                    Debug.Log("Spawned onject from: " + this.ToString());
                    Activated(hitInfo.normal, hitInfo.normal, _Name);
                }
                else if (!IsActivated && Placed)
                {
                    Placed = false;
                }
            }
        }
    }
}
