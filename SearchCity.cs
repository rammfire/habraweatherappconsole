using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using static System.Console;

namespace habraweatherappconsole
{
    /// <summary>
    /// Класс описывает возможность получения поиска
    /// и добавления городов для их последующего мониторинга.
    /// </summary>
    public static class SearchCity
    {
        /// <summary>
        /// Метод реализует возможность получения списка городов.
        /// В качестве формального параметра принимается название города
        /// которое должно быть указано в классе MainMenu.
        /// </summary>
        /// <param name="formalCityName"></param>
        public static void GettingListOfCitiesOnRequest(string formalCityName)
        {
            // Получаю ApiKey из списка
            string apiKey = UserApiManager.userApiList[0].UserApiProperty;
            try
            {
                string jsonOnWeb = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={formalCityName}";

                WebClient webClient = new WebClient();
                string prepareString = webClient.DownloadString(jsonOnWeb);

                ObservableCollection<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);

                DataRepo.PrintКeceivedСities(rbci);
            }
            catch (Exception ex)
            {
                WriteLine("Неполучилось отобразить запрашиваемый город."
                + "Возможные причины: \n" + 
                "* Неправильно указано название города\n"
                + "* Нет доступа к интернету\n"
                + "Подробнее ниже: \n"
                + ex.Message);
            }

        }
    }
}