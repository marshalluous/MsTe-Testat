using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public class ServiceTestLocal : ServiceTestBase
    {
        private IAutoReservationService target;
        protected override IAutoReservationService Target => target ?? (target = new AutoReservationService());
        
    }
}