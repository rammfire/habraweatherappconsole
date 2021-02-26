using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using static System.Console;

namespace habraweatherappconsole
{
    public static class GettingWeatherData
    {
        /// <summary>
        /// Метод реализует возможность получать погодную информацию выбранного города
        /// </summary>
        public static void GettingWeatherDataFromServices()
        {
            WebClient webClient = new WebClient();
            string pattern = "=====\n" + "Номер в списке: {0}\n" + "Название в оригинале: {1}\n"
            + "В переводе:  {2} \n" + "Страна: {3}\n" + "Административный округ: {4}\n"
            + "Тип: {5}\n" + "====\n";
            int numberInList = 0;

            foreach (var item in DataRepo.listOfCityForMonitorWeather)
            {
                WriteLine(pattern, numberInList.ToString(),
                item.EnglishName, item.LocalizedName, item.Country.LocalizedName,
                item.AdministrativeArea.LocalizedName, item.AdministrativeArea.LocalizedType);
                numberInList++;
            }
            
            bool ifNotExists = false;
            string cityKey = null;
            int num = 0;
            do
            {
                ifNotExists = false;
                Write("Номер города для просмотра погоды: ");
                num = Convert.ToInt32(Console.ReadLine());
                
                if (num < 0 || num > DataRepo.listOfCityForMonitorWeather.Count)
                {
                    WriteLine("Такого номера нет. Попробуйте ещё раз.");
                    ifNotExists = true;
                }
            } while(ifNotExists);
            
            cityKey = DataRepo.listOfCityForMonitorWeather[num].Key;
            // Получаю ApiKey из списка
            string apiKey = UserApiManager.userApiList[0].UserApiProperty;
            
            string jsonUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/5day/{cityKey}?apikey={apiKey}&language=ru&metric=true";

            jsonUrl = webClient.DownloadString(jsonUrl);

            RootWeather weatherData = JsonSerializer.Deserialize<RootWeather>(jsonUrl);

            string patternWeather = "=====\n" + "Дата: {0}\n" + "Температура минимальная: {1}\n"
            +"Температура максимальная: {2}\n" + "Примечание на день: {3}\n" + "Примечание на ночь: {4}\n" + "====\n";

            foreach (var item in weatherData.DailyForecasts)
            {
                WriteLine(patternWeather, item.Date, item.Temperature.Minimum.Value,
                item.Temperature.Maximum.Value, item.Day.IconPhrase, item.Night.IconPhrase);
            }
        }
    }
}