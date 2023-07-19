﻿namespace Analytics
{
    public class APILink
    {
        private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
        private string editorApi = "EditorGoldV1";
        private string deploymentAPI = "GoldV1";
        
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