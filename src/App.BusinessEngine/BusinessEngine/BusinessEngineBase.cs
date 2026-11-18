using System;
using System.Diagnostics;
using App.BusinessEngine.Exception;
using App.Data.Exception;
using Core.Common.Core;
using FluentValidation;
using log4net;

namespace App.BusinessEngine.BusinessEngine
{

    public interface IBusinessEngine : IHandler
    {

    }

    public interface IBusinessEngine<T> : IBusinessEngine
    {

    }

    public class BusinessEngineBase : IBusinessEngine
    {
        protected readonly ILog Logger;

        public BusinessEngineBase()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (ValidationException ex)
            {
                Trace.TraceError($"BusinessEngine:[ValidationException] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("BusinessEngine:[ValidationException] {0}-{1}", ex.Message, ex.StackTrace);

                throw;
            }
            catch (DataQueryException ex)
            {
                Trace.TraceError($"BusinessEngine:[DataQueryException] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("BusinessEngine:[DataQueryException] {0}-{1}", ex.Message, ex.StackTrace);

                throw;
            }
            catch (DataRepositoryException ex)
            {
                Trace.TraceError($"BusinessEngine:[DataRepositoryException] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("BusinessEngine:[DataRepositoryException] {0}-{1}", ex.Message, ex.StackTrace);
                throw;
            }
            catch (System.Exception ex)
            {
                Trace.TraceError($"BusinessEngine:[Exception] {ex.Message}-{ex.StackTrace}");
                Logger.ErrorFormat("BusinessEngine:[Exception] {0}-{1}", ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        
        public void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (DataQueryException ex)
            {
                Trace.TraceError($"BusinessEngine:[DataQueryException] {ex.Message}-{ex.StackTrace}");
                throw;
            }
            catch (DataRepositoryException ex)
            {
                Trace.TraceError($"BusinessEngine:[DataRepositoryException] {ex.Message}-{ex.StackTrace}");
                throw;
            }
            catch (System.Exception ex)
            {
                Trace.TraceError($"BusinessEngine:[Exception] {ex.Message}-{ex.StackTrace}");
                throw new BusinessEngineException(ex);
            }
        }
    }
}