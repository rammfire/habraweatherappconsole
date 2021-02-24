using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Xml.Serialization;


using static System.Console;
using System;

namespace habraweatherappconsole
{
    /// <summary>
    /// Класс описывает возможность получать информацию о города
    /// и предоставляет коллекцию для хранения
    /// </summary>
    public static class DataRepo
    {
        /// <summary>
        /// Коллекция временно хранит найденные города
        /// </summary>
        /// <typeparam name="RootBasicCityInfo"></typeparam>
        /// <returns></returns>
        static ObservableCollection<RootBasicCityInfo> findCityesCollection = new ObservableCollection<RootBasicCityInfo>();

        /// <summary>
        /// Список хранит информацию об добаленных городах
        /// </summary>
        /// <typeparam name="RootBasicCityInfo"></typeparam>
        /// <returns></returns>
        static ObservableCollection<RootBasicCityInfo> listOfCityForMonitorWeather = new ObservableCollection<RootBasicCityInfo>();

        /// <summary>
        /// Список хранит информацию о погоде
        /// </summary>
        /// <typeparam name="RootWeather"></typeparam>
        /// <returns></returns>
        static ObservableCollection<RootWeather> weatherInformation = new ObservableCollection<RootWeather>();

        /// <summary>
        /// Метод реализует возможность чтения файла с предопределённым количеством
        /// (в виде массива объектов json) городов.
        /// Фаил SimpleData.json должен находиться радом с исполняемым файлом
        /// </summary>
        public static void ReadDataFromLocalStorage()
        {
            string prepareString = null;
            ObservableCollection<RootBasicCityInfo> rbci;
            int numberInList = 0;
            string pattern = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале: {1}\n"
            + "В переводе:  {2} \n" + "Страна: {3}\n" + "Административный округ: {4}\n"
            + "Тип: {5}\n" + "====\n";
            using (StreamReader sr = new StreamReader("SimpleData.json"))
            {
                prepareString = sr.ReadToEnd();
            }

            rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);
            foreach (var item in rbci)
            {
                WriteLine(pattern, numberInList.ToString(),
                item.EnglishName, item.LocalizedName, item.Country.LocalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                numberInList++;
            }
        }

        public static void ReadWeatherData()
        {
            string prepareString = null;
            RootWeather weatherData;

            using (StreamReader sr = new StreamReader("WeatherExample.json"))
            {
                prepareString = sr.ReadToEnd();
            }

            weatherData = JsonSerializer.Deserialize<RootWeather>(prepareString);

            foreach (var item in weatherData.DailyForecasts)
            {
                WriteLine(item.Temperature.Maximum.Value + " " + item.Temperature.Minimum.Value);
            }
        }

        private static void WriteListOfCityMonitoring()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<RootBasicCityInfo>));

            using (StreamWriter sw = new StreamWriter("RootBasicCityInfo.xml"))
            {
                xmlSerializer.Serialize(sw, listOfCityForMonitorWeather);
            }
        }
        public static void PrintКeceivedСities (ObservableCollection<RootBasicCityInfo> formalListOfCityes)
        {
            string pattern = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале: {1}\n"
            + "В переводе:  {2} \n" + "Страна: {3}\n" + "Административный округ: {4}\n"
            + "Тип: {5}\n" + "====\n";
            int numberInList = 0;

            foreach (var item in formalListOfCityes)
            {
                WriteLine(pattern, numberInList.ToString(),
                item.EnglishName, item.LocalizedName, item.Country.LocalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                numberInList++;
            }

            Write ("Номер какого города добавить в мониторинг: ");
            int num = Convert.ToInt32(Console.ReadLine());

            try
            {
                listOfCityForMonitorWeather.Add(formalListOfCityes[num]);
            }

            catch (Exception ex)
            {
                WriteLine("Похоже, вы ошиблись цифрой.\n");
                WriteLine(ex.Message);
            }
            WriteListOfCityMonitoring();
        }
    }
}