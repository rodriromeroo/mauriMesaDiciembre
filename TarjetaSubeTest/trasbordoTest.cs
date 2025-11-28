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

        [Test]
        public void PagarCon_Trasbordo_NoCobraNada()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(5000);
            var colectivo1 = new Colectivo("120");
            var colectivo2 = new Colectivo("144");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            decimal saldoInicial = tarjeta.ObtenerSaldo();
            colectivo1.PagarCon(tarjeta, fecha);
            decimal saldoDespuesPrimero = tarjeta.ObtenerSaldo();
            var boleto2 = colectivo2.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsNotNull(boleto2);
            Assert.AreEqual(0m, boleto2.ImportePagado);
            Assert.IsTrue(boleto2.EsTransbordo);
            Assert.AreEqual(saldoDespuesPrimero, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void PagarCon_TrasbordoMismaLinea_CobraNormal()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(5000);
            var colectivo1 = new Colectivo("120");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            colectivo1.PagarCon(tarjeta, fecha);
            decimal saldoDespuesPrimero = tarjeta.ObtenerSaldo();
            var boleto2 = colectivo1.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsNotNull(boleto2);
            Assert.AreEqual(1580m, boleto2.ImportePagado);
            Assert.IsFalse(boleto2.EsTransbordo);
            Assert.AreEqual(saldoDespuesPrimero - 1580m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void PagarCon_TrasbordoFueraDeHorario_CobraNormal()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(5000);
            var colectivo1 = new Colectivo("120");
            var colectivo2 = new Colectivo("144");
            var fecha = new DateTime(2024, 11, 20, 22, 30, 0);

            colectivo1.PagarCon(tarjeta, fecha);
            decimal saldoDespuesPrimero = tarjeta.ObtenerSaldo();
            var boleto2 = colectivo2.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsNotNull(boleto2);
            Assert.AreEqual(1580m, boleto2.ImportePagado);
            Assert.IsFalse(boleto2.EsTransbordo);
        }

        [Test]
        public void PagarCon_VariosTrasbordos_DentroDe60Minutos()
        {
            var tarjeta = new Tarjeta();
            tarjeta.CargarSaldo(5000);
            var colectivo1 = new Colectivo("120");
            var colectivo2 = new Colectivo("144");
            var colectivo3 = new Colectivo("133");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var b1 = colectivo1.PagarCon(tarjeta, fecha);
            var b2 = colectivo2.PagarCon(tarjeta, fecha.AddMinutes(20));
            var b3 = colectivo3.PagarCon(tarjeta, fecha.AddMinutes(40));

            Assert.AreEqual(1580m, b1.ImportePagado);
            Assert.AreEqual(0m, b2.ImportePagado);
            Assert.AreEqual(0m, b3.ImportePagado);
            Assert.IsTrue(b2.EsTransbordo);
            Assert.IsTrue(b3.EsTransbordo);
        }

        [Test]
        public void PagarCon_TrasbordoConFranquicias_FuncionaCorrectamente()
        {
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);
            var colectivo1 = new Colectivo("120");
            var colectivo2 = new Colectivo("144");
            var fecha = new DateTime(2024, 11, 20, 10, 0, 0);

            var b1 = colectivo1.PagarCon(medio, fecha);
            var b2 = colectivo2.PagarCon(medio, fecha.AddMinutes(20));

            Assert.AreEqual(790m, b1.ImportePagado);
            Assert.AreEqual(0m, b2.ImportePagado);
            Assert.IsTrue(b2.EsTransbordo);
        }

        [Test]
        public void EsTrasbordoValido_Hora7AM_DeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var hora7 = new DateTime(2024, 11, 20, 7, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", hora7.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", hora7);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void EsTrasbordoValido_Hora21_59_DeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var hora21_59 = new DateTime(2024, 11, 20, 21, 59, 0);

            tarjeta.RegistrarUltimoViaje("120", hora21_59.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", hora21_59);

            Assert.IsTrue(resultado);
        }

        [Test]
        public void EsTrasbordoValido_Hora22_NoDeberiaSerValido()
        {
            var tarjeta = new Tarjeta();
            var hora22 = new DateTime(2024, 11, 20, 22, 0, 0);

            tarjeta.RegistrarUltimoViaje("120", hora22.AddMinutes(-30));
            bool resultado = tarjeta.EsTrasbordoValido("144", hora22);

            Assert.IsFalse(resultado);
        }
    }
}