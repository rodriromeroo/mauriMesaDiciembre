using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class EstacionBicicletaTest
    {
        private EstacionBicicleta estacion;
        private Tarjeta tarjeta;

        [SetUp]
        public void Setup()
        {
            estacion = new EstacionBicicleta();
            tarjeta = new Tarjeta();
        }

        [Test]
        public void RetirarBicicletaExitosamenteSinMultas()
        {
            tarjeta.CargarSaldo(5000);
            decimal saldoInicial = tarjeta.ObtenerSaldo();

            bool resultado = estacion.RetirarBicicleta(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(saldoInicial - 1777.50m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void DevolverBicicletaDentroDelTiempo()
        {
            tarjeta.CargarSaldo(5000);
            DateTime retiro = new DateTime(2024, 11, 15, 10, 0, 0);
            DateTime devolucion = new DateTime(2024, 11, 15, 10, 45, 0);

            estacion.RetirarBicicleta(tarjeta, retiro);
            bool resultado = estacion.DevolverBicicleta(tarjeta, devolucion);

            Assert.IsTrue(resultado);
            Assert.AreEqual(0, estacion.ObtenerCantidadMultasPendientes(tarjeta, devolucion));
        }

        [Test]
        public void DevolverBicicletaConExcesoDeTiempo()
        {
            tarjeta.CargarSaldo(5000);
            DateTime retiro = new DateTime(2024, 11, 15, 10, 0, 0);
            DateTime devolucion = new DateTime(2024, 11, 15, 11, 30, 0); 

            estacion.RetirarBicicleta(tarjeta, retiro);
            estacion.DevolverBicicleta(tarjeta, devolucion);

            Assert.AreEqual(1, estacion.ObtenerCantidadMultasPendientes(tarjeta, devolucion));
        }

        [Test]
        public void NoRetirarBicicletaPorSaldoInsuficiente()
        {
            tarjeta.CargarSaldo(2000);
            tarjeta.DescontarSaldo(1500);

            bool resultado = estacion.RetirarBicicleta(tarjeta);

            Assert.IsFalse(resultado);
        }

        [Test]
        public void RetirarBicicletaConMultasPendientes()
        {
            tarjeta.CargarSaldo(10000);
            DateTime retiro1 = new DateTime(2024, 11, 15, 9, 0, 0);
            DateTime devol1 = new DateTime(2024, 11, 15, 10, 30, 0); 
            DateTime retiro2 = new DateTime(2024, 11, 15, 11, 0, 0);

            estacion.RetirarBicicleta(tarjeta, retiro1);
            estacion.DevolverBicicleta(tarjeta, devol1);

            decimal saldoAntesSegundo = tarjeta.ObtenerSaldo();
            bool resultado = estacion.RetirarBicicleta(tarjeta, retiro2);

            Assert.IsTrue(resultado);
            Assert.AreEqual(saldoAntesSegundo - 1777.50m - 1000m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void RetirarConVariasMultasDelMismoDia()
        {
            tarjeta.CargarSaldo(15000);
            DateTime hora = new DateTime(2024, 11, 15, 8, 0, 0);

            estacion.RetirarBicicleta(tarjeta, hora);
            estacion.DevolverBicicleta(tarjeta, hora.AddMinutes(90));

            decimal despuesPrimerCiclo = tarjeta.ObtenerSaldo();

            estacion.RetirarBicicleta(tarjeta, hora.AddMinutes(100));
            estacion.DevolverBicicleta(tarjeta, hora.AddMinutes(200));

            decimal saldoAntes = tarjeta.ObtenerSaldo();
            estacion.RetirarBicicleta(tarjeta, hora.AddMinutes(210));

            Assert.AreEqual(saldoAntes - 1777.50m - 1000m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void NoRetirarDosBicicletasSimultaneas()
        {
            tarjeta.CargarSaldo(10000);

            estacion.RetirarBicicleta(tarjeta);
            bool segundoRetiro = estacion.RetirarBicicleta(tarjeta);

            Assert.IsFalse(segundoRetiro);
        }

        [Test]
        public void MultasNoSeAcumulanEntreDias()
        {
            tarjeta.CargarSaldo(10000);
            DateTime dia1 = new DateTime(2024, 11, 15, 10, 0, 0);
            DateTime dia2 = new DateTime(2024, 11, 16, 10, 0, 0);

            estacion.RetirarBicicleta(tarjeta, dia1);
            estacion.DevolverBicicleta(tarjeta, dia1.AddMinutes(90));

            decimal saldoAntes = tarjeta.ObtenerSaldo();
            estacion.RetirarBicicleta(tarjeta, dia2);

            Assert.AreEqual(saldoAntes - 1777.50m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void RetirarConSaldoNegativoPermitido()
        {
            tarjeta.CargarSaldo(2000);

            bool resultado = estacion.RetirarBicicleta(tarjeta);

            Assert.IsTrue(resultado);
            Assert.AreEqual(222.50m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void NoRetirarSiSuperaLimiteNegativoConMultas()
        {
            tarjeta.CargarSaldo(3000);
            DateTime hora = new DateTime(2024, 11, 15, 10, 0, 0);

            estacion.RetirarBicicleta(tarjeta, hora);
            estacion.DevolverBicicleta(tarjeta, hora.AddMinutes(90));

            decimal saldoActual = tarjeta.ObtenerSaldo();
            tarjeta.DescontarSaldo(saldoActual - 500);

            bool resultado = estacion.RetirarBicicleta(tarjeta, hora.AddMinutes(100));

            Assert.IsFalse(resultado);
        }

        [Test]
        public void TarjetaMedioBoletoTambienPuedeRetirarBici()
        {
            TarjetaMedioBoleto medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);

            bool resultado = estacion.RetirarBicicleta(medio);

            Assert.IsTrue(resultado);
            Assert.AreEqual(5000 - 1777.50m, medio.ObtenerSaldo());
        }
    }
}