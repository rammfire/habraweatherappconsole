﻿using System;

namespace habraweatherappconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Загружаю пользовательский API
            UserApiManager.ReadUserApiToLocalStorage();
            
            DataRepo.ReadListOfCitymonitoring();

            // Печатаю меню
            MainMenu.PrintMainMenu();
        }
    }
}
