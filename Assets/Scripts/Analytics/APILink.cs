namespace Analytics
{
    public class APILink
    {
        private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
        private string editorApi = "editorBetav2";
        private string deploymentAPI = "friendsBetav2";
        
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