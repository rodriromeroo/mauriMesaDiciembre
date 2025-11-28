using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class BoletoTests
    {
        [Test]
        public void Constructor_DeberiaInicializarTodosLosCampos()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(5000);
            var colectivo = new Colectivo("120");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = colectivo.PagarCon(tarjeta, fecha);

            Assert.AreEqual("120", boleto.LineaColectivo);
            Assert.AreEqual(1580m, boleto.ImportePagado);
            Assert.AreEqual(3420m, boleto.SaldoRestante);
            Assert.AreEqual(tarjeta.ObtenerId(), boleto.TarjetaId);
            Assert.AreEqual("Tarjeta Normal", boleto.TipoTarjeta);
            Assert.IsFalse(boleto.EsTransbordo);
        }

        [Test]
        public void Boleto_ConMedioBoleto_DeberiaIndicarTipoCorrecto()
        {
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);
            var colectivo = new Colectivo("120");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = colectivo.PagarCon(medio, fecha);

            Assert.AreEqual("Medio Boleto Estudiantil", boleto.TipoTarjeta);
            Assert.AreEqual(790m, boleto.ImportePagado);
        }

        [Test]
        public void Boleto_ConBoletoGratuito_DeberiaIndicarTipoCorrecto()
        {
            var gratuito = new TarjetaBoletoGratuito();
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("120");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = colectivo.PagarCon(gratuito, fecha);

            Assert.AreEqual("Boleto Educativo Gratuito", boleto.TipoTarjeta);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void Boleto_ConFranquiciaCompleta_DeberiaIndicarTipoCorrecto()
        {
            var franquicia = new TarjetaFranquiciaCompleta();
            var colectivo = new Colectivo("120");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = colectivo.PagarCon(franquicia, fecha);

            Assert.AreEqual("Franquicia Completa", boleto.TipoTarjeta);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void Boleto_Transbordo_DeberiaMarcarComoTransbordo()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(5000);
            var colectivo1 = new Colectivo("120");
            var colectivo2 = new Colectivo("144");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            colectivo1.PagarCon(tarjeta, fecha);
            var boleto = colectivo2.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsTrue(boleto.EsTransbordo);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void Boleto_ConSaldoNegativo_DeberiaMostrarImporteCompleto()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(2000);
            var colectivo = new Colectivo("120");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            colectivo.PagarCon(tarjeta, fecha);
            var boleto = colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));

            Assert.AreEqual(1580m, boleto.ImportePagado);
            Assert.IsTrue(tarjeta.ObtenerSaldo() < 0);
        }
    }
}