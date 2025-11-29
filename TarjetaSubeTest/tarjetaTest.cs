using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class TarjetaTests
    {
        private Tarjeta tarjeta;

        [SetUp]
        public void Setup() => tarjeta = new Tarjeta();

        [Test]
        public void Constructor_DeberiaInicializarConSaldoCero()
        {
            Assert.AreEqual(0m, tarjeta.ObtenerSaldo());
            Assert.AreEqual(0m, tarjeta.ObtenerSaldoPendiente());
        }

        [Test]
        public void CargarSaldo_ConMontosValidos_DeberiaAceptarCarga()
        {
            Assert.IsTrue(tarjeta.CargarSaldo(5000));
            Assert.AreEqual(5000m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void CargarSaldo_ConMontosInvalidos_DeberiaRechazarCarga()
        {
            Assert.IsFalse(tarjeta.CargarSaldo(1234));
            Assert.AreEqual(0m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void CargarSaldo_SuperaLimiteMaximo_DeberiaGuardarExcedentePendiente()
        {
            tarjeta.CargarSaldo(30000);
            tarjeta.CargarSaldo(30000);

            Assert.AreEqual(56000m, tarjeta.ObtenerSaldo());
            Assert.AreEqual(4000m, tarjeta.ObtenerSaldoPendiente());
        }

        [Test]
        public void DescontarSaldo_ConSaldoSuficiente_DeberiaDescontarCorrectamente()
        {
            tarjeta.CargarSaldo(5000);
            bool resultado = tarjeta.DescontarSaldo(1580);

            Assert.IsTrue(resultado);
            Assert.AreEqual(3420m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void DescontarSaldo_ExcedeLimiteNegativo_DeberiaFallar()
        {
            tarjeta.CargarSaldo(2000);
            tarjeta.DescontarSaldo(1580);
            tarjeta.DescontarSaldo(1580);
            bool resultado = tarjeta.DescontarSaldo(1580);

            Assert.IsFalse(resultado);
            Assert.AreEqual(-1160m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void EsTrasbordoValido_EnCondicionesCorrectas_DeberiaSerValido()
        {
            var ahora = new DateTime(2024, 11, 20, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", ahora.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", ahora);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void EsTrasbordoValido_MismaLinea_NoDeberiaSerValido()
        {
            var ahora = new DateTime(2024, 11, 20, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", ahora.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("120", ahora);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void EsTrasbordoValido_MasDe60Minutos_NoDeberiaSerValido()
        {
            var ahora = new DateTime(2024, 11, 20, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", ahora.AddMinutes(-61));
            bool resultado = tarjeta.EsTrasbordoValido("144", ahora);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void EsTrasbordoValido_Domingo_NoDeberiaSerValido()
        {
            var domingo = new DateTime(2024, 11, 24, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", domingo.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", domingo);

            Assert.IsFalse(resultado);
        }
    } */
}