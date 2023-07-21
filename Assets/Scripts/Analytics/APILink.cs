namespace Analytics
{
    public class APILink
    {
        private string baseURL = "https://nature-morph-v2-default-rtdb.firebaseio.com/";
        private string editorApi = "EditorGoldV3";
        private string deploymentAPI = "GoldV3";
        
        public APILink(){}

        public string getEditorAPI()
        {
            return $"{baseURL}/{editorApi}";
        }
        
        public string getDeploymentAPI()
        {
            return $"{baseURL}/{deploymentAPI}";
        }
    }
}