using NUnit.Framework;
using TarjetaSube;
using System;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class TarjetaFranquiciaCompletaTests
    {
        private TarjetaFranquiciaCompleta tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaFranquiciaCompleta();
            colectivo = new Colectivo("102");
        }

        [Test]
        public void ObtenerMontoAPagar_SiempreDeberiaSerCero()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            decimal monto = tarjeta.ObtenerMontoAPagar(1580, fecha);
            Assert.AreEqual(0m, monto);
        }

        [Test]
        public void PagarCon_DeberiaGenerarBoletoGratis()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            var boleto = colectivo.PagarCon(tarjeta, fecha);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void AplicarDescuentoUsoFrecuente_NoDeberiaAplicarDescuento()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            decimal monto = 1580m;
            decimal resultado = tarjeta.AplicarDescuentoUsoFrecuente(monto, fecha);
            Assert.AreEqual(monto, resultado);
        }

        [Test]
        public void DescontarSaldo_NoDeberiaDescontarCuandoMontoEsCero()
        {
            tarjeta.CargarSaldo(5000);
            decimal saldoInicial = tarjeta.ObtenerSaldo();

            bool resultado = tarjeta.DescontarSaldo(0m);

            Assert.IsTrue(resultado);
            Assert.AreEqual(saldoInicial, tarjeta.ObtenerSaldo());
        }
    } */
}