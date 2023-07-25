namespace Analytics
{
    public class APILink
    {
        private string baseURL = "https://nature-morph-v2-default-rtdb.firebaseio.com/";
        private string editorApi = "EditorGoldV5";
        private string deploymentAPI = "Final";
        
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