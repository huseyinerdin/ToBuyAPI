using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyAPI.Application.DTOs.Category;

namespace ToBuyAPI.Application.DTOs.Product
{
	public class CreateProduct
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IFormFileCollection ProductImageFiles { get; set; }
		public List<string> CategoryIds { get; set; }
	}
}
