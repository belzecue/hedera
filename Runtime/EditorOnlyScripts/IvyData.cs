using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hedera
{
	[System.Serializable]
    public class IvyProfile {
		#if UNITY_EDITOR

		public bool showAdvanced, showGrowthFoldout=true, showAIFoldout=true, showMeshFoldout=true;

		public LayerMask collisionMask = -5;
        public Material branchMaterial, leafMaterial;

	    public float ivyStepDistance = 0.1f;
	    public float ivyLeafSize = 0.15f;
	    public float ivyBranchSize = 0.05f;

        /** maximum length of an ivy branch segment that is freely floating [0..1] */
	    public float maxFloatLength = 1;

	    /** maximum distance for adhesion of scene object [0..1] */
	    public float maxAdhesionDistance = 1f;

		// force the ivy root to grow to at least this length
		public float minLength = 0.5f, maxLength = 5f;
		// public int maxBranchesPerRoot = 2;
		public int maxBranchesTotal = 64;

	    public float primaryWeight = 0.5f;
	    public float randomWeight = 1f;
	    public float gravityWeight = 3;
	    public float adhesionWeight = 1f;

	    public float branchingProbability = 0.25f;
	    public float leafProbability = 0.5f;
		public float leafSunlightBonus = 1f;
		public float branchOptimize = 0.5f;
		public int branchSmooth = 2;

		public float branchTaper = 1f;

		public string namePrefix = "Ivy[{0}]{1}";
		public bool markMeshAsStatic = true;
		public bool useLightmapping = false;

		public bool useVertexColors = true;
		public Gradient leafVertexColors = new Gradient();

		public IvyProfile() {
			ResetSettings();
		}

	    public void ResetSettings()
        {
	        primaryWeight = 0.5f;
	        randomWeight = 1f;
	        gravityWeight = 3f;
	        adhesionWeight = 1f;

	        branchingProbability = 0.25f;

	        ivyStepDistance = 0.1f;

	        ivyLeafSize = 0.15f;
	        ivyBranchSize = 0.05f;
			leafProbability = 0.5f;
			leafSunlightBonus = 1f;
	
			branchOptimize = 0.5f;
			branchSmooth = 2;
			branchTaper = 1f;

	        maxFloatLength = 1f;
	        maxAdhesionDistance = 1f;
			// maxBranchesPerRoot = 2;
			maxBranchesTotal = 64;

			minLength = 1f;
			maxLength = 3f;

			namePrefix = "Ivy[{0}]{1}";
			markMeshAsStatic = true;
			useLightmapping = false;
			collisionMask = Physics.DefaultRaycastLayers;

			useVertexColors = true;
			leafVertexColors = new Gradient();
			leafVertexColors.SetKeys( new GradientColorKey[] { 
				new GradientColorKey(Color.white, 0f),
				new GradientColorKey(Color.green, 0.68f),
				new GradientColorKey(Color.yellow, 1f )
			}, 
			new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f)} );

			//branchMaterial = null;
			//leafMaterial = null;
        }

		#endif
    }

	[System.Serializable]
    public class IvyNode
    {
		
		#if UNITY_EDITOR
	    public Vector3 localPos;			

	    /** primary grow direction, a weighted sum of the previous directions */
	    public Vector3 primaryGrowDir;

	    public Vector3 adhesionVector;

	    /** a smoothed adhesion vector computed and used during the birth phase,
	       since the ivy leaves are align by the adhesion vector, this smoothed vector
	       allows for smooth transitions of leaf alignment */
	    public Vector3 smoothAdhesionVector;

	    public float length;
		public float lengthCumulative;

	    /** length at the last node that was climbing */
	    public float floatingLength;

	    public bool isClimbing;

		#endif
    }

	[System.Serializable]
    public class IvyRoot
    {
		#if UNITY_EDITOR

	    public List<IvyNode> nodes = new List<IvyNode>();
	    public bool isAlive;

	    /** number of parents, represents the level in the root hierarchy */
	    public int parents;
		
		// there's a big flaw in the old algorithm or port, where roots will keep getting generated infinitely... childCount helps us kill off roots with too many children
		public int childCount;

		public float forceMinLength = -1f;

		public int meshSegments;
		public List<Vector3> leafPoints = new List<Vector3>(64);
		public bool useCachedBranchData = false, useCachedLeafData = false;

		public List<Vector3> vertices = new List<Vector3>(128);
	    public List<Vector2> texCoords = new List<Vector2>(128);
	    public List<int> triangles = new List<int>(1024);

		public List<Vector3> leafVertices = new List<Vector3>(128);
		public List<Vector2> leafUVs = new List<Vector2>(128);
		public List<int> leafTriangles = new List<int>(512);
		public List<Color> leafVertexColors = new List<Color>(128);

		#endif
    }

	[System.Serializable]
    public class IvyGraph
    {
		#if UNITY_EDITOR

		public bool isGrowing = false;
		public bool isVisible = true;
		public bool dirtyUV2s = false;
		public Vector3 seedPos;
		public bool generateMeshDuringGrowth = false;
		
		public List<Vector3> debugLineSegmentsList = new List<Vector3>(512);
		public Vector3[] debugLineSegmentsArray;

		 // ivy roots
	    [HideInInspector] public List<IvyRoot> roots = new List<IvyRoot>(8);	

		// ivy mesh data
		// public List<Vector3> vertices = new List<Vector3>();
	    // public List<Vector2> texCoords = new List<Vector2>();
	    // public List<int> triangles = new List<int>();

		// public List<Vector3> leafVertices = new List<Vector3>();
		// public List<Vector2> leafUVs = new List<Vector2>();
		// public List<int> leafTriangles = new List<int>();

		public Mesh branchMesh, leafMesh;
		// { get { 
		// 	if ( string.IsNullOrEmpty(branchGUID) ) {
		// 		return null;
		// 	}
		// 	var mesh = 
		// 	}}
		public string branchMeshID, leafMeshID;
		public Transform rootBehavior;
		public GameObject rootGO;
		public MeshFilter branchMF, leafMF;
		public Renderer branchR, leafR;

		public void ResetMeshData()
        {
	        // vertices.Clear();
            // texCoords.Clear();
            // triangles.Clear();
			// leafVertices.Clear();
			// leafUVs.Clear();
			// leafTriangles.Clear();
			dirtyUV2s = false;
			branchMesh = null;
			leafMesh = null;
        }

		#endif
    }


}