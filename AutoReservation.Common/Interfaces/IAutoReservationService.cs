using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        bool isAvailable(AutoDto auto);

        [OperationContract] IEnumerable<AutoDto> ReadAllAuto();
        [OperationContract] IEnumerable<KundeDto> ReadAllKunde();
        [OperationContract] IEnumerable<ReservationDto> ReadAllReservation();

        [OperationContract] AutoDto FindAutoById(int id);
        [OperationContract] KundeDto FindKundeById(int id);
        [OperationContract] ReservationDto FindReservationByNr(int id);

        [OperationContract] AutoDto InsertAuto(AutoDto auto);
        [OperationContract] KundeDto InsertKunde(KundeDto kunde);
        [OperationContract] ReservationDto InsertReservation(ReservationDto reservation);

        [OperationContract] AutoDto UpdateAuto(AutoDto auto);
        [OperationContract] KundeDto UpdateKunde(KundeDto kunde);
        [OperationContract] ReservationDto UpdateReservation(ReservationDto reservation);

        [OperationContract] AutoDto DeleteAuto(AutoDto auto);
        [OperationContract] KundeDto DeleteKunde(KundeDto kunde);
        [OperationContract] ReservationDto DeleteReservation(ReservationDto reservation);
    }
}
