using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GizmoTypes { None, Collider, Position }
public enum DisplayModes { Always, OnlyWhenSelected }
public enum PositionModes { Point, Cube, WireCube, Sphere, WireSphere }
public enum ColliderRenderTypes { Full, Wire }

public class GizmoDisplay : MonoBehaviour
{
    public GizmoTypes gizmoType;
    public DisplayModes displayMode;
    public PositionModes positionMode;
    public ColliderRenderTypes colliderRenderType;
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 1.0f;

    private void OnDrawGizmos()
    {
        if (displayMode == DisplayModes.Always)
        {
            DrawGizmo();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (displayMode == DisplayModes.OnlyWhenSelected)
        {
            DrawGizmo();
        }
    }

    private void DrawGizmo()
    {
        Gizmos.color = gizmoColor;

        switch (gizmoType)
        {
            case GizmoTypes.None:
                break;
            case GizmoTypes.Collider:
                DrawColliderGizmo();
                break;
            case GizmoTypes.Position:
                DrawPositionGizmo();
                break;
        }
    }

    private void DrawColliderGizmo()
    {
        Collider collider = GetComponent<Collider>();
        if (collider == null) return;

        if (colliderRenderType == ColliderRenderTypes.Full)
        {
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
        else if (colliderRenderType == ColliderRenderTypes.Wire)
        {
            Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
        }
    }

    private void DrawPositionGizmo()
    {
        switch (positionMode)
        {
            case PositionModes.Point:
                Gizmos.DrawSphere(transform.position, gizmoSize);
                break;
            case PositionModes.Cube:
                Gizmos.DrawCube(transform.position, Vector3.one * gizmoSize);
                break;
            case PositionModes.WireCube:
                Gizmos.DrawWireCube(transform.position, Vector3.one * gizmoSize);
                break;
            case PositionModes.Sphere:
                Gizmos.DrawSphere(transform.position, gizmoSize);
                break;
            case PositionModes.WireSphere:
                Gizmos.DrawWireSphere(transform.position, gizmoSize);
                break;
        }
    }
}
