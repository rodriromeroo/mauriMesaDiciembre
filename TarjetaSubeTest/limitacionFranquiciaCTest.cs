using System;
using NUnit.Framework;
using TarjetaSube;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class LimitacionFranquiciaCompletaTest
    {
        [Test]
        public void BoletoGratuito_PrimerosDosViajesGratis_TerceroCobra()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(5000);
            Colectivo colectivo = new Colectivo("102");
            DateTime horarioPermitido = new DateTime(2024, 11, 20, 10, 0, 0);

            Boleto primerViaje = colectivo.PagarCon(tarjeta, horarioPermitido);
            Boleto segundoViaje = colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(10));
            Boleto tercerViaje = colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(20));

            Assert.IsNotNull(primerViaje);
            Assert.IsNotNull(segundoViaje);
            Assert.IsNotNull(tercerViaje);
            Assert.AreEqual(0m, primerViaje.ImportePagado);
            Assert.AreEqual(0m, segundoViaje.ImportePagado);
            Assert.AreEqual(1580m, tercerViaje.ImportePagado);
        }

        [Test]
        public void BoletoGratuito_PrimerosDosViajes_NoDescuentanSaldo()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("K");
            DateTime horarioPermitido = new DateTime(2024, 11, 20, 10, 0, 0);
            decimal saldoInicial = tarjeta.ObtenerSaldo();

            colectivo.PagarCon(tarjeta, horarioPermitido);
            colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(10));

            decimal saldoFinal = tarjeta.ObtenerSaldo();

            Assert.AreEqual(saldoInicial, saldoFinal);
        }

        [Test]
        public void BoletoGratuito_TercerViajeYPosteriores_DescuentanSaldo()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("144");
            DateTime horarioPermitido = new DateTime(2024, 11, 20, 10, 0, 0);

            colectivo.PagarCon(tarjeta, horarioPermitido);
            colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(10));
            decimal saldoAntesTercer = tarjeta.ObtenerSaldo();

            colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(20));

            Assert.AreEqual(saldoAntesTercer - 1580m, tarjeta.ObtenerSaldo());
        }

        [Test]
        public void BoletoGratuito_SinSaldo_PrimerosDosViajesFuncionan()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            Colectivo colectivo = new Colectivo("27");
            DateTime horarioPermitido = new DateTime(2024, 11, 20, 10, 0, 0);

            Boleto primerViaje = colectivo.PagarCon(tarjeta, horarioPermitido);
            Boleto segundoViaje = colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(10));
            Boleto tercerViaje = colectivo.PagarCon(tarjeta, horarioPermitido.AddMinutes(20));

            Assert.IsNotNull(primerViaje);
            Assert.IsNotNull(segundoViaje);
            Assert.IsNull(tercerViaje);
        }

        [Test]
        public void BoletoGratuito_NuevoDia_ReiniciaContadorViajesGratuitos()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("133");
            DateTime dia1 = new DateTime(2024, 11, 20, 10, 0, 0);
            DateTime dia2 = new DateTime(2024, 11, 21, 10, 0, 0);

            colectivo.PagarCon(tarjeta, dia1);
            colectivo.PagarCon(tarjeta, dia1.AddMinutes(10));
            colectivo.PagarCon(tarjeta, dia1.AddMinutes(20));

            Boleto primerViajeDia2 = colectivo.PagarCon(tarjeta, dia2);
            Boleto segundoViajeDia2 = colectivo.PagarCon(tarjeta, dia2.AddMinutes(10));

            Assert.AreEqual(0m, primerViajeDia2.ImportePagado);
            Assert.AreEqual(0m, segundoViajeDia2.ImportePagado);
        }

        [Test]
        public void BoletoGratuito_FueraDeHorario_NoPermitePagar()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("102");
            DateTime fueraHorario = new DateTime(2024, 11, 20, 23, 0, 0);

            Boleto boleto = colectivo.PagarCon(tarjeta, fueraHorario);

            Assert.IsNull(boleto);
        }

        [Test]
        public void BoletoGratuito_Sabado_NoPermitePagar()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("102");
            DateTime sabado = new DateTime(2024, 11, 23, 10, 0, 0);

            Boleto boleto = colectivo.PagarCon(tarjeta, sabado);

            Assert.IsNull(boleto);
        }

        [Test]
        public void BoletoGratuito_Domingo_NoPermitePagar()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("102");
            DateTime domingo = new DateTime(2024, 11, 24, 10, 0, 0);

            Boleto boleto = colectivo.PagarCon(tarjeta, domingo);

            Assert.IsNull(boleto);
        }

        [Test]
        public void BoletoGratuito_DentroDeHorarioYDiasHabiles_PermitePagar()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("102");
            DateTime miercoles = new DateTime(2024, 11, 20, 10, 0, 0);

            Boleto boleto = colectivo.PagarCon(tarjeta, miercoles);

            Assert.IsNotNull(boleto);
        }

        [Test]
        public void BoletoGratuito_ViajesConsecutivosMismoDia_FuncionaCorrectamente()
        {
            TarjetaBoletoGratuito tarjeta = new TarjetaBoletoGratuito();
            tarjeta.CargarSaldo(10000);
            Colectivo colectivo = new Colectivo("144");
            DateTime horario = new DateTime(2024, 11, 20, 10, 0, 0);

            Boleto b1 = colectivo.PagarCon(tarjeta, horario);
            Boleto b2 = colectivo.PagarCon(tarjeta, horario.AddMinutes(10));
            Boleto b3 = colectivo.PagarCon(tarjeta, horario.AddMinutes(20));
            Boleto b4 = colectivo.PagarCon(tarjeta, horario.AddMinutes(30));

            Assert.AreEqual(0m, b1.ImportePagado);
            Assert.AreEqual(0m, b2.ImportePagado);
            Assert.AreEqual(1580m, b3.ImportePagado);
            Assert.AreEqual(1580m, b4.ImportePagado);
        }
    } */
}