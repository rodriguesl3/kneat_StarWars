using Kneat.SWApi.Application.Interface;
using Kneat.SWApi.Domain;
using Kneat.SWApi.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kneat.SWApi.Application.Implementation
{
    public class StarShipInformation : IStarShipInformation
    {
        private readonly ISWApiInfrastructure _starShipInfrastructure;

        public StarShipInformation(ISWApiInfrastructure starShipInfrastructure)
        {
            _starShipInfrastructure = starShipInfrastructure;
        }


        public IEnumerable<StarShip> GetStartShipsInformation(int distance)
        {
            var resultList = new List<StarShipHeadInformation>();
            int page = 1;
            bool hasFinished = false;

            while (!hasFinished)
            {
                var responseDeserialize = JsonConvert.DeserializeObject<StarShipHeadInformation>(_starShipInfrastructure.RequestStarShipAPI(page));

                responseDeserialize.results = CalculateStops(responseDeserialize.results, distance);


                resultList.Add(responseDeserialize);

                if (String.IsNullOrEmpty(responseDeserialize.next))
                {
                    hasFinished = true;
                }
                page++;
            }

            return resultList.SelectMany(x => x.results);
        }


        public StarShip[] CalculateStops(StarShip[] starShipList, int distance)
        {
            foreach (var starShip in starShipList)
            {
                var typeTime = starShip.consumables;
                var megaLight = starShip.MGLT;
                starShip.Stops = (typeTime == "unknown" || megaLight == "unknown") ? -1 : (distance / Convert.ToInt32(megaLight)) / convertTimeStringInIntegerValue(typeTime);
            }

            return starShipList;
        }

        public int convertTimeStringInIntegerValue(string period)
        {
            int totalHrs = 0;
            var shortType = period.Split(' ')[1][0].ToString();
            var time = Convert.ToInt32(period.Split(' ')[0]);
            switch (shortType)
            {
                case "y":
                    totalHrs = time * 24 * 365;
                    break;
                case "m":
                    totalHrs = time * 24 * 30;
                    break;
                case "w":
                    totalHrs = time * 24 * 7;
                    break;
                case "d":
                    totalHrs = time * 24;
                    break;
                case "h":
                    totalHrs = time;
                    break;
                default:
                    totalHrs = 0;
                    break;

            }

            return totalHrs;
        }


    }
}
