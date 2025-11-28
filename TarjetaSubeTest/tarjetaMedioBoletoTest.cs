using NUnit.Framework;
using TarjetaSube;
using System;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class TarjetaMedioBoletoTests
    {
        private TarjetaMedioBoleto tarjeta;
        private Colectivo colectivo;
        private DateTime fecha;

        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaMedioBoleto();
            colectivo = new Colectivo("144");
            tarjeta.CargarSaldo(10000);
            fecha = new DateTime(2024, 11, 20, 10, 0, 0);
        }

        [Test]
        public void ObtenerMontoAPagar_PrimerViaje_DevuelveMitad()
        {
            decimal monto = tarjeta.ObtenerMontoAPagar(1580m, fecha);
            Assert.AreEqual(790m, monto);
        }

        [Test]
        public void ObtenerMontoAPagar_SegundoViaje_DevuelveMitad()
        {
            tarjeta.ObtenerMontoAPagar(1580m, fecha);
            decimal monto2 = tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(10));

            Assert.AreEqual(790m, monto2);
        }

        [Test]
        public void ObtenerMontoAPagar_TercerViaje_DevuelveCompleto()
        {
            tarjeta.ObtenerMontoAPagar(1580m, fecha);
            tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(10));
            decimal monto3 = tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(20));

            Assert.AreEqual(1580m, monto3);
        }

        [Test]
        public void ObtenerMontoAPagar_AntesDe5Minutos_DevuelveMenosUno()
        {
            tarjeta.ObtenerMontoAPagar(1580m, fecha);
            decimal monto2 = tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(4));

            Assert.AreEqual(-1m, monto2);
        }

        [Test]
        public void ObtenerMontoAPagar_NuevoDia_ReiniciaContador()
        {
            tarjeta.ObtenerMontoAPagar(1580m, fecha);
            tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(10));
            tarjeta.ObtenerMontoAPagar(1580m, fecha.AddMinutes(20));

            var dia2 = new DateTime(2024, 11, 21, 10, 0, 0);
            decimal montoDia2 = tarjeta.ObtenerMontoAPagar(1580m, dia2);

            Assert.AreEqual(790m, montoDia2);
        }

        [Test]
        public void AplicarDescuentoUsoFrecuente_NoDeberiaAplicarDescuento()
        {
            decimal monto = 1580m;
            decimal resultado = tarjeta.AplicarDescuentoUsoFrecuente(monto, fecha);
            Assert.AreEqual(monto, resultado);
        }

        [Test]
        public void DescontarSaldo_ConSaldoSuficiente_DeberiaFuncionar()
        {
            decimal saldoInicial = tarjeta.ObtenerSaldo();
            bool resultado = tarjeta.DescontarSaldo(790m);

            Assert.IsTrue(resultado);
            Assert.AreEqual(saldoInicial - 790m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void PagarCon_ConSaldoSuficiente_DeberiaGenerarBoleto()
        {
            var boleto = colectivo.PagarCon(tarjeta, fecha);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(790m, boleto.ImportePagado);
        }

        [Test]
        public void PuedeUsarseAhora_DentroDeHorario_DeberiaSerTrue()
        {
            var fechaValida = new DateTime(2024, 11, 20, 15, 0, 0);
            bool resultado = tarjeta.PuedeUsarseAhora(fechaValida);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void PuedeUsarseAhora_FueraDeHorario_DeberiaSerFalse()
        {
            var fueraHorario = new DateTime(2024, 11, 20, 23, 0, 0);
            bool resultado = tarjeta.PuedeUsarseAhora(fueraHorario);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void PuedeUsarseAhora_FinDeSemana_DeberiaSerFalse()
        {
            var sabado = new DateTime(2024, 11, 23, 10, 0, 0);
            bool resultado = tarjeta.PuedeUsarseAhora(sabado);

            Assert.IsFalse(resultado);
        }
    }
}