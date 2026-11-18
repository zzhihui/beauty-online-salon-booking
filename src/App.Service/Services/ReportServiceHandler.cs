using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Reports;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using FluentValidation;



namespace App.Service.Services
{
    public interface IReportServiceHandler
    {
        WsReport GetReport(WsReport report, int shopId);
    }

    [ServiceHandler]
    public class ReportServiceHandler : ServiceBase, IReportServiceHandler
    {
        public ReportServiceHandler(IValidatorFactory validatorFactory, IUnitOfWork unitOfWork, ITransformer transformer) : base(validatorFactory, unitOfWork, transformer)
        {
        }

        public WsReport GetReport(WsReport report, int shopId)
        {
            throw new System.NotImplementedException();
        }
    }
}