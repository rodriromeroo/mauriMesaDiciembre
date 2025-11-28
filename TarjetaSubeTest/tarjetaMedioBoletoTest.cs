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

        [SetUp]
        public void Setup()
        {
            tarjeta = new TarjetaMedioBoleto();
            colectivo = new Colectivo("144");
            tarjeta.CargarSaldo(10000);
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
        public void DescontarSaldo_ConSaldoSuficiente_DeberiaFuncionar()
        {
            bool resultado = tarjeta.DescontarSaldo(790m);
            Assert.IsTrue(resultado);
        }

        [Test]
        public void PagarCon_ConSaldoSuficiente_DeberiaGenerarBoleto()
        {
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);
            var boleto = colectivo.PagarCon(tarjeta, fecha);
            Assert.IsNotNull(boleto);
        }
    }
}