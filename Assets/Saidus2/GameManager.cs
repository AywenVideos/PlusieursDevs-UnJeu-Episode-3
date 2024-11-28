using LevelGenerator.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Saidus2
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { return instance; } }
        bool isLevelGenerated = false;
        public bool IsLevelGenerated { get => isLevelGenerated; 
            set { 
                isLevelGenerated = value; 
                } 
        }
        public List<NavMeshManager> Statues = new List<NavMeshManager>();
        public NavMeshSurface[] surfaces;
        private void LaunchGame()
        { surfaces = GameObject.FindObjectsOfType<NavMeshSurface>();
            foreach (NavMeshSurface surface in surfaces)
            {
                surface.BuildNavMesh();
            }
            foreach(NavMeshManager manager in Statues)
            {
                manager.gameObject.SetActive(true);
            }

        }

        int followingStatue = 0;
        public float timeSanityDescent = 0.3f;

        //public PlayerControls playerControls;
        public event Action OnDeath;
        
        public bool hasKey = false;

        public int levelNumber = 1;
        public LevelGenerator.Scripts.LevelGenerator levelGenerator;

        //public List<NavMeshManager> statues = new List<NavMeshManager>();

        [SerializeField]
        float sanity, sanityMax, vignetteOffset = -0.03f;
        public Vignette vignette;
        public void Awake()
        {
            if (instance == null) instance = this;
            else { Destroy(this.gameObject); }
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += ChangeLevelGeneratorReference;
        }

        private void ChangeLevelGeneratorReference(Scene arg0, LoadSceneMode arg1)
        {

           levelGenerator = FindAnyObjectByType<LevelGenerator.Scripts.LevelGenerator>();
        }

        private void Start()
        {

            //playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>(); 
            OnDeath += Dead;
            sanity = sanityMax;
            GetComponentInChildren<Volume>().profile.TryGet<Vignette>(out vignette);
            StartCoroutine(SanityDown());
        }
        float sanityNormalized;
        public GameObject gameOverScreen;

        public float SanityNormalized { get => sanityNormalized; }
        private void OnDestroy()
        {
            OnDeath -= Dead;
        }

        private void Update()
        {
            if (levelGenerator == null) return;
                if(!IsLevelGenerated && levelGenerator.LevelSize <= 0)
                {
                    //try                             // oui c'est tres tres moche, mais pour des raisons de debug j'avais besoin de catch une exception de chargement de scene quand elle n'tait pas charg�e

                        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("LoadingScene"));


                    IsLevelGenerated = true;
                    LaunchGame();
                }

            sanityNormalized = 1 - sanity / sanityMax;
            AddEffect();
            if(sanity <= 0) OnDeath?.Invoke();

        }
      
        public void AddEffect()
        {
            vignette.intensity.value = SanityNormalized+vignetteOffset;
        }

        public void SanityPillGrabbed(int value)
        {
            sanity += value;
            sanity = Mathf.Clamp(sanity,0, sanityMax);
        }

        public void AddFollowingStatue ()
        {
            followingStatue++;
        }

        public void AddDamages(float damages)
        {
            sanity -= damages;
        }

        private IEnumerator SanityDown()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeSanityDescent);
                print("followingStatues : " + followingStatue);
                sanity -= followingStatue;
                if(sanity <= 0)
                OnDeath?.Invoke();
                break;
            }

        }

        public void SubstractFollowingStatue()
        {
            followingStatue--;
            followingStatue = Mathf.Clamp(followingStatue,0,int.MaxValue);
        }

        internal void LoadLevel()
        {
            levelNumber++;
            followingStatue = 0;
            Statues.Clear();
            SceneManager.LoadSceneAsync("LevelScene",LoadSceneMode.Single);
            SceneManager.LoadSceneAsync("LoadingScene",LoadSceneMode.Additive);
        }
        internal void Dead()
        {
            GameObject.Instantiate(gameOverScreen);
            OnDeath -= Dead;
        }
    }
}
