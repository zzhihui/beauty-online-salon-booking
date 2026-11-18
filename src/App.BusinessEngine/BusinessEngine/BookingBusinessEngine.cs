using App.Data.Repositories;
using Core.Common.Utility;

namespace App.BusinessEngine.BusinessEngine
{
    public interface IBookingBusinessEngine {
    }

    public class BookingBusinessEngine: BusinessEngineBase, IBookingBusinessEngine
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEncryptionHelper _encryptionHelper;

        public BookingBusinessEngine(IBookingRepository bookingRepository, IEncryptionHelper encryptionHelper)
        {
            _bookingRepository = bookingRepository;
            _encryptionHelper = encryptionHelper;
        }
    }
}
