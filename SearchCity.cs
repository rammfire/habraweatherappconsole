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
        public static void GettingListOfCitiesOnRequest(string formalCityName)
        {
            // Получаю ApiKey из списка
            string apiKey = UserApiManager.userApiList[0].UserApiProperty;
            string jsonOnWeb = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={formalCityName}&language=ru";

            WebClient webClient = new WebClient();
            string prepareString = webClient.DownloadString(jsonOnWeb);

            ObservableCollection<RootBasicCityInfo> rbci = JsonSerializer.Deserialize<ObservableCollection<RootBasicCityInfo>>(prepareString);

            DataRepo.PrintКeceivedСities(rbci);

        }
    }
}