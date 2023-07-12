namespace Analytics
{
    public class APILink
    {
        private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
        private string editorApi = "EditorBetaV1";
        private string deploymentAPI = "BetaV1";
        
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