using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using CarRental.Business.Abstract;
using CarRental.Business.BusinessAspects.Autofac;
using CarRental.Business.Constants;
using CarRental.Core.Aspects.Autofac.Caching;
using CarRental.Core.Aspects.Autofac.Logging;
using CarRental.Core.Aspects.Autofac.Transaction;
using CarRental.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using CarRental.Core.Utilities;
using CarRental.Core.Utilities.Results;
using CarRental.DataAccess.Abstract;
using CarRental.Entities.Entities;

namespace CarRental.Business.Concrete
{
    public class PhotoManager: IPhotoService
    {
        private readonly IPhotoDal _photoDal;
        public PhotoManager(IPhotoDal photoDal)
        {
            _photoDal = photoDal;
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult AddCarPhoto(Photo carPhoto)
        {
            _photoDal.Add(carPhoto);
            return new SuccessResult(Message.GeneralSuccessfull);

        }
        
        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult UpdateCarPhoto(Photo carPhoto)
        {
            _photoDal.Update(carPhoto);
            return new SuccessResult(Message.GeneralSuccessfull);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult DeleteCarPhoto(Photo carPhoto)
        {
            _photoDal.Delete(carPhoto);
            return new SuccessResult(Message.GeneralSuccessfull);
        }
        
        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult AddCarPhotos(List<Photo> carPhotos)
        {
            _photoDal.AddRange(carPhotos);
            return new SuccessResult(Message.GeneralSuccessfull);
        }

        [TransactionScopeAspect]
        [SecuredOperation("admin,system.admin")]
        [LogAspect(typeof(FileLogger))]
        public IResult DeleteCarPhotos(List<Photo> carPhotos)
        {
            _photoDal.RemoveRange(carPhotos);
            return new SuccessResult(Message.GeneralSuccessfull);

        }
    }
}
