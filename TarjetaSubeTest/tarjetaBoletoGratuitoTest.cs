using NUnit.Framework;
using TarjetaSube;
using System;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class TarjetaBoletoGratuitoTests
    {
        private TarjetaBoletoGratuito tarjeta;
        private Colectivo colectivo;

        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaBoletoGratuito();
            colectivo = new Colectivo("102");
            tarjeta.CargarSaldo(10000);
        }

        [Test]
        public void ObtenerMontoAPagar_PrimerosDosViajes_DeberiaSerCero()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            var b1 = colectivo.PagarCon(tarjeta, fecha);
            var b2 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));

            Assert.AreEqual(0m, b1.ImportePagado);
            Assert.AreEqual(0m, b2.ImportePagado);
        }

        [Test]
        public void ObtenerMontoAPagar_TercerViajeEnDia_DeberiaCobrarCompleto()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            colectivo.PagarCon(tarjeta, fecha);
            colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));
            var b3 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.AreEqual(1580m, b3.ImportePagado);
        }

        [Test]
        public void AplicarDescuentoUsoFrecuente_NoDeberiaAplicarDescuento()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            decimal monto = 1580m;
            decimal resultado = tarjeta.AplicarDescuentoUsoFrecuente(monto, fecha);

            Assert.AreEqual(monto, resultado);
        }
    } */
}