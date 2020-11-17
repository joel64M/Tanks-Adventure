using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace NameSpaceName {
    [CustomEditor(typeof(PatrolZone))]
    public class PatrolZoneEditor : Editor
    {

        #region Variables
        PatrolZone pz;
        #endregion

        #region Builtin Methods

        private void OnEnable()
        { 
            pz = target as PatrolZone;
        }

        private void OnSceneGUI()
        {
            Handles.color = Color.red;
            pz.point1 = HandlePoint(pz.point1,ref pz.point4,ref pz.point3,1);
        
            Handles.color = Color.green;
            pz.point2 = HandlePoint(pz.point2,ref pz.point3,ref pz.point4,2);
            Handles.color = Color.blue;
            pz.point3 = HandlePoint(pz.point3, ref pz.point2,ref pz.point1,3);
            pz.point4 = HandlePoint(pz.point4,ref pz.point1,ref pz.point2,4);
            Handles.color = Color.blue;
            Handles.DrawAAPolyLine(new Vector3[]{ pz.point1,pz.point3, pz.point2,pz.point4,pz.point1});
        }

       Vector3 HandlePoint(Vector3 point,ref Vector3 prevPoint,ref  Vector3 nextPoint,int i)
        {
            point = Handles.FreeMoveHandle(point, Quaternion.identity, 2f, Vector3.zero, Handles.CylinderHandleCap);
            EditorUtility.SetDirty(pz);
            Handles.Label(point, "point"+i.ToString());
            point.y = 0;
            prevPoint.x = point.x;
            nextPoint.z = point.z;
            return point;
        }

        #endregion

        #region Custom Methods

        #endregion

    }
}
