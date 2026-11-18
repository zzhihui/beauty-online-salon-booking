using System;
using System.Diagnostics;
using App.BusinessEngine.Exception;
using App.Service.Contract.Entities;
using App.Service.Exception;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Utility.Extension;
using FluentValidation;
using log4net;

namespace App.Service.Services
{
    public interface IService : IHandler
    {
        void Validate<T>(T request, string ruleSet) where T : WsEntity;
    }


    public class ServiceBase : IService
    {
        protected readonly IValidatorFactory ValidatorFactory;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ITransformer Transformer;
        protected readonly ILog Logger;

        public ServiceBase(IValidatorFactory validatorFactory, 
                           IUnitOfWork unitOfWork, 
                           ITransformer transformer)
        {
            ValidatorFactory = validatorFactory;
            UnitOfWork = unitOfWork;
            Transformer = transformer;
            Logger = LogManager.GetLogger(GetType());
        }

        public void Validate<T>(T request, string ruleSet = "") where T : WsEntity
        {
            var validator = ValidatorFactory.GetValidator(request.GetType());
            validator.ValidateAndThrow(request, ruleSet);
        }
        
        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                Logger.DebugFormat("Perform Action");
                var response = action();
                Logger.DebugFormat("Commit Unit of Work");
                UnitOfWork.Commit();
                return response;
            }
            catch (BusinessEngineException ex)
            {
                Trace.TraceError($"ServiceHandler:[BusinessEngineException] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("ServiceHandler:[BusinessEngineException] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }
            catch (ValidationException ex)
            {
                Trace.TraceError($"ServiceHandler:[ValidationException] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("ServiceHandler:[ValidationException] {0}-{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex, ServiceExceptionCode.SEx002.ToString());
            }
            catch (TransformerException ex)
            {
                Trace.TraceError($"ServiceHandler:[TransformerException] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("ServiceHandler:[TransformerException] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }
            catch (System.Exception ex)
            {
                Trace.TraceError($"ServiceHandler:[Exception] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("ServiceHandler:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        
        public TResponse Query<TResponse>(Func<TResponse> action)
        {
            try
            {
                Logger.DebugFormat("Perform Action");
                var response = action();
                return response;
            }
            catch (BusinessEngineException exB)
            {
                Logger.ErrorFormat("BusinessEngineException Error:{0}--{1}", exB.Message, exB.StackTrace);
                throw;
            }
            catch (ValidationException exVal)
            {
                Logger.ErrorFormat("ValidationException Error:{0}--{1}", exVal.Message, exVal.StackTrace);
                throw new ServiceException(exVal, ServiceExceptionCode.SEx002.ToString());
            }
            catch (TransformerException exTr)
            {
                Logger.ErrorFormat("TransformerException Error:{0}--{1}", exTr.Message, exTr.StackTrace);
                throw;
            }
            catch (System.Exception ex)
            {
                Logger.ErrorFormat("Exception Error:{0}--{1}", ex.Message, ex.StackTrace);
                throw new ServiceException(ex);
            }
        }
    }
}