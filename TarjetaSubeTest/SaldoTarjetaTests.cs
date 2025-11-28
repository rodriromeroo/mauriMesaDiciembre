using NUnit.Framework;
using TarjetaSube;
using System;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class SaldoTarjetaTests
    {
        private Tarjeta tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta();
            colectivo = new Colectivo("133");
        }

        [Test]
        public void CargarSaldo_SuperaLimite56000_AcreditaHastaLimiteYGuardaExcedente()
        {
            tarjeta.CargarSaldo(30000);
            tarjeta.CargarSaldo(30000);

            Assert.AreEqual(56000, tarjeta.ObtenerSaldo());
            Assert.AreEqual(4000, tarjeta.ObtenerSaldoPendiente());
        }

        [Test]
        public void AcreditarCarga_DespuesDeViaje_RecargaSaldo()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            tarjeta.CargarSaldo(30000);
            tarjeta.CargarSaldo(30000);
            decimal pendienteInicial = tarjeta.ObtenerSaldoPendiente();

            colectivo.PagarCon(tarjeta, fecha);

            Assert.Less(tarjeta.ObtenerSaldoPendiente(), pendienteInicial);
        }

        [Test]
        public void TarjetaMedioBoleto_TambienRespetaLimite56000()
        {
            TarjetaMedioBoleto medioBoleto = new TarjetaMedioBoleto();

            medioBoleto.CargarSaldo(30000);
            medioBoleto.CargarSaldo(30000);

            Assert.AreEqual(56000, medioBoleto.ObtenerSaldo());
        }

        [Test]
        public void TarjetaBoletoGratuito_TambienRespetaLimite56000()
        {
            TarjetaBoletoGratuito gratuito = new TarjetaBoletoGratuito();

            gratuito.CargarSaldo(30000);
            gratuito.CargarSaldo(30000);

            Assert.AreEqual(56000, gratuito.ObtenerSaldo());
        }
    }
}