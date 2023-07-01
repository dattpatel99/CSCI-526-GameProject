/**
 * This is the information that will be stored about a checkpoint
 */
namespace Analytics {
    public class CheckPointAnalytics
    {
        public string checkpointID;
        public float crossTime;
        public int curHeart;
        public int curTimeBank;
        public int curPlayerAge;
        public bool repeat;
        public CheckPointAnalytics(string checkPointName, PlayerController playerController, TimeBank bank, bool repeated)
        {
            this.repeat = repeated;
            this.checkpointID = checkPointName;
            this.curHeart = playerController.getHP().GetHP();
            this.curPlayerAge = playerController.getAge();
            this.curTimeBank = bank.GetTimeStore();
        }
        public void UpdateTime(float timer)
        {
            this.crossTime = timer;
        }
    }
}