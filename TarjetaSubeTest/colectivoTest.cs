using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class ColectivoTests
    {
        private Colectivo colectivo;
        private Tarjeta tarjeta;
        private DateTime fecha;

        [SetUp]
        public void Setup()
        {
            colectivo = new Colectivo("133");
            tarjeta = new Tarjeta();
            fecha = new DateTime(2024, 11, 20, 10, 0, 0);
        }

        [Test]
        public void ObtenerLinea_DeberiaDevolverLineaCorrecta()
        {
            Assert.AreEqual("133", colectivo.ObtenerLinea());
        }

        [Test]
        public void PagarCon_TarjetaConSaldo_DeberiaGenerarBoleto()
        {
            tarjeta.CargarSaldo(5000);
            Boleto boleto = colectivo.PagarCon(tarjeta, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual("133", boleto.LineaColectivo);
            Assert.AreEqual(1580m, boleto.ImportePagado);
        }

        [Test]
        public void PagarCon_TarjetaSinSaldo_DeberiaDevolverNull()
        {
            Boleto boleto = colectivo.PagarCon(tarjeta, fecha);
            Assert.IsNull(boleto);
        }

        [Test]
        public void PagarCon_DeberiaDescontarSaldoCorrectamente()
        {
            tarjeta.CargarSaldo(5000);
            decimal saldoInicial = tarjeta.ObtenerSaldo();

            colectivo.PagarCon(tarjeta, fecha);

            Assert.AreEqual(saldoInicial - 1580m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void PagarCon_TarjetaMedioBoleto_DeberiaCobrarMitad()
        {
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);

            Boleto boleto = colectivo.PagarCon(medio, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(790m, boleto.ImportePagado);
        }

        [Test]
        public void PagarCon_TarjetaBoletoGratuito_DeberiaSerGratis()
        {
            var gratuito = new TarjetaBoletoGratuito();
            gratuito.CargarSaldo(5000);

            Boleto boleto = colectivo.PagarCon(gratuito, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void PagarCon_TarjetaFranquiciaCompleta_DeberiaSerGratis()
        {
            var franquicia = new TarjetaFranquiciaCompleta();

            Boleto boleto = colectivo.PagarCon(franquicia, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void PagarCon_ConSaldoNegativoPermitido_DeberiaGenerarBoleto()
        {
            tarjeta.CargarSaldo(2000);
            colectivo.PagarCon(tarjeta, fecha);
            Boleto boleto2 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));

            Assert.IsNotNull(boleto2);
            Assert.IsTrue(tarjeta.ObtenerSaldo() < 0);
        }

        [Test]
        public void PagarCon_ExcedeSaldoNegativo_DeberiaDevolverNull()
        {
            tarjeta.CargarSaldo(2000);
            colectivo.PagarCon(tarjeta, fecha);
            colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));
            Boleto boleto3 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsNull(boleto3);
        }
    }
}