using UnityEngine;
using System.Collections;
using System;
using System.Threading;


namespace Pathfinding
{
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
	public class AIDestinationSetter : VersionedMonoBehaviour
	{
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		private Transform min = null;
		public float hitboxmob;
		public float hitboxplayer; // on doit le supprimer plus tard (il faut recuperer le hit box du joueur il est le meme pour tt les personnes)
		bool end = false;

		IAstarAI ai;


		void OnEnable()
		{
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable()
		{
			if (ai != null) ai.onSearchPath -= Update;
		}
		void Start()
		{
			Seeker.attacks = true;
			if (Seeker.listPlayer.Count!= 0)
            {
				min = Seeker.listPlayer[0];
			}
			
			ai.canMove = true;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update()
		{
			while (min == null)
            {
				min = Seeker.listPlayer[0];
			}
			int nbjoueur = Seeker.listPlayer.Count;

			bool f = ai.reachedEndOfPath;

			for (int i = 0; i < nbjoueur; i++)
			{

				if (cvt(this.gameObject, Seeker.listPlayer[i]) < cvt(this.gameObject, min))
				{

					min = Seeker.listPlayer[i];
				}

			}

			ai.destination = min.position;

			if (ai.reachedEndOfPath)
			{
				StartCoroutine(Attacks());
			}


		}

		IEnumerator Attacks()
		{
	
			ai.isStopped = true;
			yield return new WaitForSeconds(1f);
			ai.isStopped = false;


		}

		public float cvt(GameObject ia, Transform mob)
		{
			return Math.Abs(mob.position.x - ia.transform.position.x) + Math.Abs(mob.position.y - ia.transform.position.y);
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, hitboxmob);
		}
	}
}

