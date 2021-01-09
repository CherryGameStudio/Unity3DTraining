using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UGUITraining
{
#if UNITY_EDITOR
	public class RaycastTargetGizmos : MonoBehaviour
	{
		[SerializeField]
		private bool IsDrawLine;

		private static Vector3[] corners = new Vector3[4];

		private void OnDrawGizmos()
		{
			foreach (MaskableGraphic g in GameObject.FindObjectsOfType<MaskableGraphic>())
			{
				if (g.raycastTarget && IsDrawLine)
				{
					RectTransform rectTransform = g.transform as RectTransform;
					rectTransform.GetWorldCorners(corners);
					Gizmos.color = Color.blue;
					for (int i = 0; i < corners.Length; i++)
					{
						Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
					}
				}
			}
		}
	}
#endif
}
