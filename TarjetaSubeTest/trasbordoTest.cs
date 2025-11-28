using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class TrasbordoTests
    {
        [Test]
        public void EsTrasbordoValido_EnCondicionesCorrectas_DeberiaSerTrue()
        {
            var tarjeta = new Tarjeta();
            var ahora = new DateTime(2024, 11, 20, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", ahora.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", ahora);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void EsTrasbordoValido_MismaLinea_NoDeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var ahora = new DateTime(2024, 11, 20, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", ahora.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("120", ahora);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void EsTrasbordoValido_MasDe60Minutos_NoDeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var ahora = new DateTime(2024, 11, 20, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", ahora.AddMinutes(-61));
            bool resultado = tarjeta.EsTrasbordoValido("144", ahora);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void EsTrasbordoValido_Domingo_NoDeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var domingo = new DateTime(2024, 11, 24, 10, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", domingo.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", domingo);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void EsTrasbordoValido_FueraDeHorario_NoDeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var noche = new DateTime(2024, 11, 20, 22, 30, 0);

            tarjeta.RegistrarUltimoViaje("120", noche.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", noche);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void EsTrasbordoValido_SabadoDentroDeHorario_DeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var sabado = new DateTime(2024, 11, 23, 15, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", sabado.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", sabado);

            Assert.IsTrue(resultado);
        }
    }
}