using System.Collections;
using Blocks_Folder;
using Game_Folder.UIScripts;
using Map_Folder;
using UnityEngine;
using UnityEngine.UI;
using Utils.EventCenter;

namespace Game_Folder
{
    public class GameMgr : MonoBehaviour
    {
        private static GameMgr _instance;

        public RectTransform basePoint;

        [SerializeField] private int originStep;
        public int moveStep;
        public bool gameOn;

        public Vector2 startPoint;
    
        [SerializeField] private float midY;
        public float endY;

        [SerializeField] private float scrollSpeed;
    
        [SerializeField] private Animator treeAnim;
        [SerializeField] private GameObject leafObj;
        [SerializeField] private GameObject failPanel;
        [SerializeField] private AudioSource BGM;
        [SerializeField] private GameObject roll;
        [SerializeField] private AnimationCurve alphaCurve;
        
        private float originY;
        private int curLevel = 0;



        private void Start()
        {
            _instance = this;
            moveStep = originStep;
            originY = basePoint.anchoredPosition.y;
            EventCenter.GetInstance().AddEventListener(EventTypes.RootMove,OnRootMove);
            EventCenter.GetInstance().AddEventListener(EventTypes.LevelFinish,OnLevelFinish);
            leafObj.SetActive(false);
            failPanel.SetActive(false);
        }

        private void Update()
        {
            if(gameOn)
            {
                SlideMap();
            }

        }

        public static GameMgr GetInstance()
        {
            return _instance;
        }

        public void SetFinalandStartPoint()
        {
            var matrix = MapCreator.GetInstance()._mapMatrix;
            matrix[(int)startPoint.x][(int)startPoint.y].GetComponent<BlockBase>().info.moveState = 0;

        }
    
        void OnRootMove()
        {
            moveStep--;
            if (moveStep < 0)
            {
                StartCoroutine(OutOfMoveStep());
            }
        }

        void SlideMap()
        {
            var delta = Input.mouseScrollDelta * -50f;
            var pos = basePoint.anchoredPosition.y;
            var value = pos + delta.y;

            basePoint.anchoredPosition =
                new Vector2(basePoint.anchoredPosition.x, Mathf.Clamp(value, originY, endY));
        }

        void OnLevelFinish()
        {
            gameOn = false;
            switch (curLevel)
            {
                case 0:
                    StartCoroutine(Level0Finish());
                    break;
                case 1:
                    StartCoroutine(Level1Finish());
                    break;
                case 2:
                    StartCoroutine(Level2Finish());
                    break;
            }
        }
    

        IEnumerator OutOfMoveStep()
        {
            gameOn = false;
            failPanel.SetActive(true);
            while (basePoint.anchoredPosition.y > originY)
            {
                SlideUp();
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.1f);
            failPanel.SetActive(false);
            moveStep = originStep;
            leafObj.GetComponent<LeafScript>().textComp.text = moveStep < 0 ? 0.ToString() : moveStep.ToString();
            
            MapCreator.GetInstance().ReloadMap(curLevel);
            // while (basePoint.anchoredPosition.y < midY)
            // {
            //     SlideDown();
            //     yield return new WaitForSeconds(0.01f);
            // }
            gameOn = true;
        }

        IEnumerator Level0Enter()
        {
            while (basePoint.anchoredPosition.y < midY)
            {
                SlideDown();
                yield return new WaitForSeconds(0.01f);
            }
            //
            // roll.SetActive(true);
            // var blinkTime = 4;
            // for(int i = 0; i <= blinkTime; i++)
            // {
            //     Debug.LogError("666");
            //     var time = 3f;
            //     var timer = 0f;
            //     var rollImage = roll.GetComponent<Image>();
            //     var alpha = 0f;
            //     while (time > 0f)
            //     {
            //         time -= Time.deltaTime;
            //         alpha = alphaCurve.Evaluate(timer / time);
            //         yield return new WaitForSeconds(Time.deltaTime);
            //         rollImage.color = new Color(rollImage.color.r, rollImage.color.g, rollImage.color.b, alpha);
            //     }
            // }
            // roll.SetActive(false);
        }

        IEnumerator Level0Finish()
        {
            leafObj.SetActive(false);
            BGM.Pause();
            AudioMgr.GetInstance().PlayLevelPassAudio(curLevel);
            while (basePoint.anchoredPosition.y > originY)
            {
                basePoint.transform.Translate(Vector3.down * scrollSpeed);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.2f);
            PlayAnim(curLevel);
            yield return new WaitForSeconds(1f);
            MapCreator.GetInstance().ReloadMap(1);
            endY = 950f;
            moveStep = originStep;
            curLevel++;
            while (basePoint.anchoredPosition.y < midY)
            {
                SlideDown();
                yield return new WaitForSeconds(0.01f);
            }
            leafObj.GetComponent<LeafScript>().textComp.text = moveStep < 0 ? 0.ToString() : moveStep.ToString();
            leafObj.SetActive(true);
            BGM.Play();
            gameOn = true;
        }

        IEnumerator Level1Finish()
        {
            leafObj.SetActive(false);
            BGM.Pause();
            AudioMgr.GetInstance().PlayLevelPassAudio(curLevel);
            while (basePoint.anchoredPosition.y > originY)
            {
                basePoint.transform.Translate(Vector3.down * scrollSpeed);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.2f);
            PlayAnim(curLevel);
            yield return new WaitForSeconds(1f);
            MapCreator.GetInstance().ReloadMap(2);
            endY = 3095f;
            moveStep = originStep;
            curLevel++;
            while (basePoint.anchoredPosition.y < midY)
            {
                SlideDown();
                yield return new WaitForSeconds(0.01f);
            }
            leafObj.GetComponent<LeafScript>().textComp.text = moveStep < 0 ? 0.ToString() : moveStep.ToString();
            leafObj.SetActive(true);
            BGM.Play();
            gameOn = true;
        }

        IEnumerator Level2Finish()
        {
            BGM.Pause();
            leafObj.SetActive(false);
            AudioMgr.GetInstance().PlayLevelPassAudio(curLevel);
            while (basePoint.anchoredPosition.y > originY)
            {
                basePoint.transform.Translate(Vector3.down * scrollSpeed);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.2f);
            PlayAnim(curLevel);
            yield return new WaitForSeconds(1f);
        }
    
        public void OnStartClick()
        {
            var mapCreator = MapCreator.GetInstance();
            mapCreator.LoadMapMatrix(Resources.Load<TextAsset>("LevelMaptxt/level_0"));
            mapCreator.CreateMap();
            gameOn = true;
            endY = 950f;
            StartCoroutine(Level0Enter());
            var leaf = leafObj;
            leaf.SetActive(true);
            leaf.GetComponent<LeafScript>().textComp.text = moveStep < 0 ? 0.ToString() : moveStep.ToString();
            
        }
    
        void OnQuitClick()
        {
            
        }

        void SlideDown()
        {
            basePoint.transform.Translate(Vector3.up * scrollSpeed);
        }

        void SlideUp()
        {
            basePoint.transform.Translate(Vector3.down * scrollSpeed);
        }

        void PlayAnim(int level)
        {
            switch (level)
            {
                case 0:
                    treeAnim.Play("grow");
                    break;
                case 1:
                    treeAnim.Play("grow2");
                    break;
                case 2:
                    treeAnim.Play("grow3");
                    break;
            }
        }
    }
}
