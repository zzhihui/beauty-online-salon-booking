using System;
using System.Diagnostics;
using App.Service.Exception;
using AutoMapper;
using Core.Common.Core;
using log4net;

namespace App.Service.Transformers
{

    public interface ITransformer : IHandler
    {
        TDestination Transform<TSource, TDestination>(TSource source) where TDestination : class, new();
    }

   
    public class TransformerBase : ITransformer
    {

        protected readonly ILog Logger;

        public TransformerBase()
        {
            Logger = LogManager.GetLogger(this.GetType());
        }


        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(string.Format("Transformer:[Exception] {0}-{1}", ex.Message, ex.StackTrace));
                throw new TransformerException(ex);
            }
        }


        public virtual TDestination Transform<TSource, TDestination>(TSource source) where TDestination : class, new()
        {
            var destination = new TDestination();
            return Execute(() =>
            {
                Mapper.Map(source, destination);
                return destination;
            });
        }
    }
}