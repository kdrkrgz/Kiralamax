using System;
using System.Collections.Generic;
using System.Text;
using CarRental.Core.Utilities;
using CarRental.Entities.Entities;

namespace CarRental.Business.Abstract
{
    public interface IPhotoService
    {
        IResult AddCarPhoto(Photo carPhoto);
        IResult UpdateCarPhoto(Photo carPhoto);
        IResult DeleteCarPhoto(Photo carPhoto);
        IResult DeleteCarPhotoById(int photoId);

        IResult AddCarPhotos(List<Photo> carPhotos);
        IResult DeleteCarPhotos(List<Photo> carPhotos);
    }
}
