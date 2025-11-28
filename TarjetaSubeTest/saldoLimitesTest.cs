using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class SaldoLimitesTests
    {
        [Test]
        public void CargarSaldo_ConMontosValidos_DeberiaAceptarTodos()
        {
            var tarjeta = new Tarjeta();
            decimal[] montosValidos = { 2000m, 3000m, 4000m, 5000m, 8000m, 10000m, 15000m, 20000m, 25000m, 30000m };

            foreach (var monto in montosValidos)
            {
                Assert.IsTrue(tarjeta.CargarSaldo(monto), $"Monto {monto} debería ser válido");
            }
        }

        [Test]
        public void CargarSaldo_ConMontoInvalido_DeberiaRechazar()
        {
            var tarjeta = new Tarjeta();
            decimal[] montosInvalidos = { 1000m, 1234m, 6000m, 12000m, 35000m };

            foreach (var monto in montosInvalidos)
            {
                Assert.IsFalse(tarjeta.CargarSaldo(monto), $"Monto {monto} debería ser inválido");
            }
        }

        [Test]
        public void TodasLasTarjetas_DeberianRespetarLimite56000()
        {
            var normal = new Tarjeta();
            var medio = new TarjetaMedioBoleto();
            var gratuito = new TarjetaBoletoGratuito();
            var franquicia = new TarjetaFranquiciaCompleta();

            normal.CargarSaldo(30000);
            normal.CargarSaldo(30000);

            medio.CargarSaldo(30000);
            medio.CargarSaldo(30000);

            gratuito.CargarSaldo(30000);
            gratuito.CargarSaldo(30000);

            franquicia.CargarSaldo(30000);
            franquicia.CargarSaldo(30000);

            Assert.AreEqual(56000m, normal.ObtenerSaldo());
            Assert.AreEqual(56000m, medio.ObtenerSaldo());
            Assert.AreEqual(56000m, gratuito.ObtenerSaldo());
            Assert.AreEqual(56000m, franquicia.ObtenerSaldo());
        }
    }
}