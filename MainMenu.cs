using static System.Console;

namespace habraweatherappconsole
{
    /// <summary>
    /// Класс описывает главное меню для взаимодействия
    /// с программой.
    /// </summary>
    public static class MainMenu
    {

        public static void PrintMainMenu()
        {
            bool canExit = true;
            string answer = null;
            while (canExit)
            {
                WriteLine("==================================");
                WriteLine("|Добро пожаловать в Погоду       |");
                WriteLine("|Доступные действия:             |\n" +
                          "==================================");
                WriteLine("|1 - Ввести API                  |\n" +
                          "|2 - Добавить город              |\n" +
                          "|3 - Посмотреть погоду           |\n" +
                          "|q - Выйти из программы          |");
                WriteLine("===============================\n");

                Write("Ваш выбор: ");
                answer = ReadLine();

                switch (answer)
                {
                    case "1":
                    {
                        string api = ReadLine();
                        UserApiManager.WriteUserApiToLocalStorage(api);
                    }
                    break;

                    case "2":
                    {

                    }
                    break;

                    case "3":
                    {

                    }
                    break;

                    case "q":
                    case "Q":
                    case "й":
                    case "Й":
                    {
                        canExit = false;
                    }
                    break;

                    default:
                    {
                        WriteLine("Что-то пошло не так");
                    }
                    break;
                }
            }
        }
    }
}