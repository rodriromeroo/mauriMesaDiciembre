using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{
    [TestFixture]
    public class MedioBoletoTests
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
        public void MedioBoleto_PrimerViaje_CobraMitad()
        {
            var boleto = colectivo.PagarCon(tarjeta, fecha);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(790m, boleto.ImportePagado);
        }

        [Test]
        public void MedioBoleto_SegundoViaje_CobraMitad()
        {
            colectivo.PagarCon(tarjeta, fecha);
            var boleto2 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));

            Assert.IsNotNull(boleto2);
            Assert.AreEqual(790m, boleto2.ImportePagado);
        }

        [Test]
        public void MedioBoleto_TercerViaje_CobraCompleto()
        {
            colectivo.PagarCon(tarjeta, fecha);
            colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));
            var boleto3 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(20));

            Assert.IsNotNull(boleto3);
            Assert.AreEqual(1580m, boleto3.ImportePagado);
        }

        [Test]
        public void MedioBoleto_NoPermitePagarAntesDe5Minutos()
        {
            colectivo.PagarCon(tarjeta, fecha);
            var boleto2 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(4));

            Assert.IsNull(boleto2);
        }

        [Test]
        public void MedioBoleto_PermitePagarDespuesDe5Minutos()
        {
            colectivo.PagarCon(tarjeta, fecha);
            var boleto2 = colectivo.PagarCon(tarjeta, fecha.AddMinutes(5));

            Assert.IsNotNull(boleto2);
        }

        [Test]
        public void MedioBoleto_NuevoDia_ReiniciaContador()
        {
            colectivo.PagarCon(tarjeta, fecha);
            colectivo.PagarCon(tarjeta, fecha.AddMinutes(10));
            colectivo.PagarCon(tarjeta, fecha.AddMinutes(20));

            var dia2 = new DateTime(2024, 11, 21, 10, 0, 0);
            var boletoNuevoDia = colectivo.PagarCon(tarjeta, dia2);

            Assert.IsNotNull(boletoNuevoDia);
            Assert.AreEqual(790m, boletoNuevoDia.ImportePagado);
        }

        [Test]
        public void MedioBoleto_AplicarDescuentoUsoFrecuente_NoAplicaDescuento()
        {
            decimal monto = 1580m;
            decimal resultado = tarjeta.AplicarDescuentoUsoFrecuente(monto, fecha);
            Assert.AreEqual(monto, resultado);
        }

        [Test]
        public void MedioBoleto_DescontarSaldo_FuncionaCorrectamente()
        {
            decimal saldoInicial = tarjeta.ObtenerSaldo();
            bool resultado = tarjeta.DescontarSaldo(790m);

            Assert.IsTrue(resultado);
            Assert.AreEqual(saldoInicial - 790m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void MedioBoleto_FueraDeHorario_NoPermitePagar()
        {
            var fueraHorario = new DateTime(2024, 11, 20, 23, 0, 0);
            var boleto = colectivo.PagarCon(tarjeta, fueraHorario);

            Assert.IsNull(boleto);
        }

        [Test]
        public void MedioBoleto_FinDeSemana_NoPermitePagar()
        {
            var sabado = new DateTime(2024, 11, 23, 10, 0, 0);
            var boleto = colectivo.PagarCon(tarjeta, sabado);

            Assert.IsNull(boleto);
        }
    }
}