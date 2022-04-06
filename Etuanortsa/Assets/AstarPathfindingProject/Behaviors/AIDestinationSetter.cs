using UnityEngine;
using System.Collections;
<<<<<<< Updated upstream
using System;
=======
>>>>>>> Stashed changes

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
<<<<<<< Updated upstream
		IAstarAI ai;
		public GameObject[] PlayerList;// a suprimer plus tard pour mettre la vraie liste
		public GameObject min;
		public float hitboxmob;
		public float hitboxplayer; // on doit le supprimer plus tard (il faut recuperer le hit box du joueur il est le meme pour tt les personnes)
		public float test= 0;


		void Start()
		{
			min = PlayerList[1];

		}
		
		
=======
		public Transform target2;
		public Transform target3;
		public Transform target4;
		public int distance1;
		public int distance2;
		public int distance3;
		public int distance4;
		IAstarAI ai;

>>>>>>> Stashed changes
		void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}
<<<<<<< Updated upstream
		
		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			int nbjoueur = PlayerList.Length;
			bool f = ai.reachedEndOfPath;

			for (int i = 0; i< nbjoueur; i++)
            {
				if (cvt(this.gameObject, PlayerList[i]) < cvt(this.gameObject, min))
                {
					
					min = PlayerList[i];
				}
				
			}
			ai.destination = min.transform.position;

			if (cvt(this.gameObject,min)<= hitboxmob + hitboxplayer)
            {
				// mob attaque
				// animation de l attaque
				ai.canMove = false;
			
			}

			if (cvt(this.gameObject, min) > hitboxmob + hitboxplayer)
			{
				ai.canMove = true;
			}






			//float distance1 = Vector3.Distance(target.position, mob.position);
			//float distance2 = Vector3.Distance(target2.position, mob.position);
			//float distance3 = Vector3.Distance(target3.position, mob.position);
			//float distance4 = Vector3.Distance(target4.position, mob.position);

			//if (target != null && ai != null && distance1<= distance2 && distance1<= distance3 && distance1<= distance4)
			//{ 
			//	ai.destination = target.position; 
			//}
			//if (target2 != null && ai != null && distance2 < distance1 && distance2 <= distance3 && distance2 <= distance4)
			//{ 	
			//	ai.destination = target2.position;
			//}
			//if (target3 != null && ai != null && distance3 < distance1 && distance3 < distance2 && distance3 <= distance4)
			//{
			//	ai.destination = target3.position;
			//}
			//if (target4 != null && ai != null && distance4 < distance1 && distance4 < distance2 && distance4 < distance3)
			//{
			//	ai.destination = target4.position;
			//}
		}
		public float cvt(GameObject ia, GameObject mob)
		{
			return Math.Abs (mob.transform.position.x - ia.transform.position.x) + Math.Abs(mob.transform.position.y - ia.transform.position.y);
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, hitboxmob);
		}

=======

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			
			if (target != null && ai != null && distance1<= distance2 && distance1<= distance3 && distance1<= distance4)
			{ 
				ai.destination = target.position; 
			}
			if (target2 != null && ai != null && distance2 < distance1 && distance2 <= distance3 && distance2 <= distance4)
            { 	
				ai.destination = target2.position;
			}
			if (target3 != null && ai != null && distance3 < distance1 && distance3 < distance2 && distance3 <= distance4)
			{
				ai.destination = target3.position;
			}
			if (target4 != null && ai != null && distance4 < distance1 && distance4 < distance2 && distance4 < distance3)
			{
				ai.destination = target4.position;
			}
		}
>>>>>>> Stashed changes
	}
}
