using Kneat.SWApi.Infrastructure.Interfaces;
using System;
using System.Net;

namespace Kneat.SWApi.Infrastructure.External
{
    public class SWApiInfrastructure : ISWApiInfrastructure
    {
        private readonly string _urlBaseConnection = "https://swapi.co/api";


        public string RequestStarShipAPI(int page = 1)
        {
            try
            {
                var response = (new WebClient()).DownloadString($"{_urlBaseConnection}/starships?page={page}");
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
