using CarRental.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Helpers.FileUpload;
using CarRental.Entities.Dtos;
using CarRental.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CarRental.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController: ControllerBase
    {
        private readonly ICarService _carManager;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carManager = carService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var result = _carManager.GetAllCarsWithDetails();
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromForm]CarAddDto carAddDto)
        {
            List<string> imagePaths = new List<string>();

            if (Request.Form.Files.Count > 0)
            {
                var carImages = Request.Form.Files;
                foreach (var file in carImages)
                {
                    // TODO: Filename edit
                    // upload request form files to under "wwwroot/Images/CarImages" folder and return saving db paths
                    var dbPath = FileUploadHelper.UploadFile(file,"wwwroot", "Images\\CarImages");
                    imagePaths.Add(dbPath);
                }
            }
            var car = _mapper.Map<Car>(carAddDto);


            var result = _carManager.AddCar(car, imagePaths);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            //imagePaths.ForEach(FileUploadHelper.RemoveFile);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromForm]CarAddDto carAddDto)
        {
            List<string> imagePaths = new List<string>();

            if (Request.Form.Files.Count > 0)
            {
                var carImages = Request.Form.Files;
                foreach (var file in carImages)
                {
                    // TODO: Filename edit
                    // upload request form files to under "wwwroot/Images/CarImages" folder and return saving db paths
                    var dbPath = FileUploadHelper.UploadFile(file,"wwwroot", "Images\\CarImages");
                    imagePaths.Add(dbPath);
                }
            }
            var car = _mapper.Map<Car>(carAddDto);
            //if (result.IsSuccess)
            //{
            //    return Ok(result);
            //}
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _carManager.DeleteByCarId(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
            
        }

        [HttpDelete("deletecarphoto/{id}")]
        public IActionResult DeleteCarPhoto(int id)
        {
            var result = _carManager.DeleteCarPhoto(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
