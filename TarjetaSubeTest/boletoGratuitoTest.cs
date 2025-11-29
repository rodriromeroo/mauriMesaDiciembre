using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class BoletoGratuitoTests
    {
        [Test]
        public void BoletoGratuito_SoloDosViajesGratisPorDia()
        {
            var tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            var colectivo = new Colectivo("102");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var b1 = colectivo.PagarCon(tarjeta, fecha);
            var b2 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));
            var b3 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);
            Assert.IsNotNull(b3);
            Assert.AreEqual(0m, b1.ImportePagado);
            Assert.AreEqual(0m, b2.ImportePagado);
            Assert.AreEqual(1580m, b3.ImportePagado);
        }

        [Test]
        public void BoletoGratuito_AplicarDescuentoUsoFrecuente_NoAplicaDescuento()
        {
            var tarjeta = new TarjetaBoletoGratuito();
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            decimal monto = 1580m;
            decimal resultado = tarjeta.AplicarDescuentoUsoFrecuente(monto, fecha);

            Assert.AreEqual(monto, resultado);
        }

        [Test]
        public void BoletoGratuito_ObtenerMontoAPagar_PrimerosDosViajesCero()
        {
            var tarjeta = new TarjetaBoletoGratuito();
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            decimal monto1 = tarjeta.ObtenerMontoAPagar(1580m, fecha);
            tarjeta.RegistrarViaje(fecha);

            decimal monto2 = tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(10));
            tarjeta.RegistrarViaje(fecha.AddMinutes(10));

            decimal monto3 = tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(20));

            Assert.AreEqual(0m, monto1);
            Assert.AreEqual(0m, monto2);
            Assert.AreEqual(1580m, monto3);
        }

        [Test]
        public void BoletoGratuito_NuevoDia_ReiniciaContador()
        {
            var tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            var colectivo = new Colectivo("102");
            var dia1 = new DateTime(2024, 11, 20, 10, 0, 0);
            var dia2 = new DateTime(2024, 11, 21, 10, 0, 0);

            colectivo.PagarCon(tarjeta, dia1);
            colectivo.PagarCon(tarjeta, dia1.AddMinutes(10));
            colectivo.PagarCon(tarjeta, dia1.AddMinutes(20));

            var b1Dia2 = colectivo.PagarCon(tarjeta, dia2);

            Assert.IsNotNull(b1Dia2);
            Assert.AreEqual(0m, b1Dia2.ImportePagado);
        }

        [Test]
        public void BoletoGratuito_FueraDeHorario_NoPermitePagar()
        {
            var tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            var colectivo = new Colectivo("102");
            var fueraHorario = new DateTime(2024, 11, 20, 23, 0, 0);

            var boleto = colectivo.PagarCon(tarjeta, fueraHorario);

            Assert.IsNull(boleto);
        }

        [Test]
        public void BoletoGratuito_FinDeSemana_NoPermitePagar()
        {
            var tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            var colectivo = new Colectivo("102");
            var sabado = new DateTime(2024, 11, 23, 10, 0, 0);

            var boleto = colectivo.PagarCon(tarjeta, sabado);

            Assert.IsNull(boleto);
        }
    } */ 
}