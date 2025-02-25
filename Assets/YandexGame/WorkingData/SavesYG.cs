
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        //BaseGameData
        public int CurrentResult;
        public int BestResult;
        public bool IsEducationEnd = false;
        public bool IsCatSceneView = true;

        //Settings
        public float VolumeSounds = 1;
        public int DelayAfterDieDoodle = 1000;

        //Achivements
        public int Astronaut4CurrentCount;
        public int Astronaut8CurrentCount;
        public int Astronom50CurrentCount;
        public int Astronom100CurrentCount;
        public int EpicFail50CurrentCount;
        public int EpicFail100CurrentCount;
        public int KillMonsters10CurrentCount;
        public int KillMonsters20CurrentCount;
        public int Rotate10CurrentCount;
        public int Rotate20CurrentCount;
        public int Runner1000CurrentCount;
        public int Runner2000CurrentCount;

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
