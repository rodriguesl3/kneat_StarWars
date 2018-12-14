using AutoMapper;
using Kneat.SWApi.Application.Implementation;
using Kneat.SWApi.Application.Interface;
using Kneat.SWApi.Domain;
using Kneat.SWApi.Infrastructure.External;
using Kneat.SWApi.Infrastructure.Interfaces;
using Kneat.SWApi.Presentation.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Kneat.SWApi.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
          .AddSingleton<ISWApiInfrastructure, SWApiInfrastructure>()
          .AddScoped<IStarShipInformation, StarShipInformation>()
          .BuildServiceProvider();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<StarShip, StarShipInformationViewModel>()
                                                           .ForMember(dest => dest.StopsNecessaries, opt => opt.MapFrom(source => source.Stops))
                                                           .ForMember(dest => dest.StarShipName, opt => opt.MapFrom(source => source.name))
                                                           .ForMember(dest => dest.Starship_class, opt => opt.MapFrom(source => source.starship_class))
                                                           .ForMember(dest => dest.MGLT, opt => opt.MapFrom(source => source.MGLT))
                                                           .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(source => source.manufacturer))
                                                           .ForMember(dest => dest.Consumables, opt => opt.MapFrom(source => source.consumables))
                                                           .ReverseMap()
            );
                                                        
            var mapper = config.CreateMapper();


            var starShipInformation = serviceProvider.GetService<IStarShipInformation>();
            RunApp(starShipInformation, mapper);
        }

        internal static void Automapper()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<StarShipInformationViewModel, StarShip>());
        }




        internal static void RunApp(IStarShipInformation _starShipInformation, IMapper mapper)
        {
            Console.WriteLine("Please inform, the distance: ");
            var typeDistance = Console.ReadLine();
            int distance = 0;

            if (!Int32.TryParse(typeDistance, out distance))
            {
                Console.WriteLine("Distance informed is invalid");
                return;
            }

            
            var starShipInformationList = _starShipInformation.GetStartShipsInformation(distance);

            var target = mapper.Map<IEnumerable<StarShip>, IEnumerable<StarShipInformationViewModel>>(starShipInformationList);
            Console.WriteLine("StarShip Name    ---     Stops");
            target.ToList().ForEach(x => Console.WriteLine($"Star Ship: {x.StarShipName} -> Stops: {x.StopsNecessaries} -> MGLT: {x.MGLT}  --> Consumables: {x.Consumables} \n\n"));

            Console.ReadKey();
        }
    }
}
