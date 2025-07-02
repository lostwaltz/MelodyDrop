using UnityEditor;
using UnityEngine;

namespace Engine
{
    public class JsonParser : MonoBehaviour
    {
        [Header("경로 설정")] [Tooltip("엑셀 파일들이 있는 폴더 경로 (프로젝트 기준 상대 경로)")]
        public string excelFolder = "../MiniMash/ExternalResource";

        [Tooltip("생성된 JSON 파일을 저장할 폴더 경로 (Assets 기준)")]
        public string jsonSaveFolder = "Assets/Resources/Json";
    }
}