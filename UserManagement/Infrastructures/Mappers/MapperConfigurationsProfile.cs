using Application.ViewModels.AccountDTOs;

using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Mappers
{
	public class MapperConfigurationsProfile : Profile
	{
		public MapperConfigurationsProfile()
		{
			//Account
			CreateMap<Account, AccountDTO>().ReverseMap();
			CreateMap<Account, CreateAccountDTO>().ReverseMap();
			CreateMap<Account, UpdateAccountDTO>().ReverseMap();
			CreateMap<Account, RegisterDTO>().ReverseMap();


        }
	}
}