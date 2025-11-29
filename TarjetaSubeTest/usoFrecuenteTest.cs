using NUnit.Framework;
using TarjetaSube;
using System;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class UsoFrecuenteTests
    {
        [Test]
        public void CalcularDescuentoUsoFrecuente_Primeros29Viajes_SinDescuento()
        {
            var tarjeta = new Tarjeta();
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            for (int i = 0; i < 29; i++)
            {
                decimal monto = tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
                Assert.AreEqual(1580m, monto);
            }
        }

        [Test]
        public void CalcularDescuentoUsoFrecuente_Viajes30a59_20PorcientoDescuento()
        {
            var tarjeta = new Tarjeta();
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            for (int i = 0; i < 29; i++)
            {
                tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
            }

            decimal monto = tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
            Assert.AreEqual(1580m * 0.80m, monto);
        }

        [Test]
        public void CalcularDescuentoUsoFrecuente_Viajes60a80_25PorcientoDescuento()
        {
            var tarjeta = new Tarjeta();
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            for (int i = 0; i < 59; i++)
            {
                tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
            }

            decimal monto = tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
            Assert.AreEqual(1580m * 0.75m, monto);
        }

        [Test]
        public void CalcularDescuentoUsoFrecuente_DespuesViaje80_SinDescuento()
        {
            var tarjeta = new Tarjeta();
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            for (int i = 0; i < 80; i++)
            {
                tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
            }

            decimal monto = tarjeta.CalcularDescuentoUsoFrecuente(1580, fecha);
            Assert.AreEqual(1580m, monto);
        }
    } */
}