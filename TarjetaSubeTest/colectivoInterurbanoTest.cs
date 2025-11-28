using NUnit.Framework;
using TarjetaSube;
using System;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class ColectivoInterurbanoTests
    {
        [Test]
        public void Constructor_DeberiaHeredarDeColectivo()
        {
            var interurbano = new ColectivoInterurbano("500");
            Assert.IsInstanceOf<Colectivo>(interurbano);
        }

        [Test]
        public void ObtenerLinea_DeberiaDevolverLineaCorrecta()
        {
            var interurbano = new ColectivoInterurbano("500");
            Assert.AreEqual("500", interurbano.ObtenerLinea());
        }

        [Test]
        public void PagarCon_TarjetaNormal_DeberiaCobrarTarifaCompleta()
        {
            var interurbano = new ColectivoInterurbano("500");
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(10000);
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = interurbano.PagarCon(tarjeta, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(3000m, boleto.ImportePagado);
        }

        [Test]
        public void PagarCon_MedioBoleto_DeberiaCobrarMitad()
        {
            var interurbano = new ColectivoInterurbano("500");
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(10000);
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = interurbano.PagarCon(medio, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(1500m, boleto.ImportePagado);
        }

        [Test]
        public void PagarCon_BoletoGratuito_DeberiaSerGratis()
        {
            var interurbano = new ColectivoInterurbano("500");
            var gratuito = new TarjetaBoletoGratuito();
            gratuito.CargarSaldo(10000);
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = interurbano.PagarCon(gratuito, fecha);

            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }
    }
}