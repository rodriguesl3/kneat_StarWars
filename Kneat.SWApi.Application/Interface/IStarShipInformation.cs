using Kneat.SWApi.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kneat.SWApi.Application.Interface
{
    public interface IStarShipInformation
    {
        IEnumerable<StarShip> GetStartShipsInformation(int distance);



    }
}
