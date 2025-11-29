using NUnit.Framework;
using System;
using TarjetaSube;

namespace TarjetaSube.Tests
{ /*
    [TestFixture]
    public class FranquiciasHorarioTests
    {
        [Test]
        public void TarjetaNormal_FuncionaCualquierDiaYHorario()
        {
            var normal = new Tarjeta();
            normal.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var domingo = new DateTime(2024, 11, 24, 23, 0, 0);

            var boleto = colectivo.PagarCon(normal, domingo);
            Assert.IsNotNull(boleto);
        }

        [Test]
        public void FranquiciaCompleta_DentroDeHorario_PermitePagar()
        {
            var franquicia = new TarjetaFranquiciaCompleta();
            var colectivo = new Colectivo("102");
            var fechaValida = new DateTime(2024, 11, 20, 10, 0, 0);

            var boleto = colectivo.PagarCon(franquicia, fechaValida);
            Assert.IsNotNull(boleto);
            Assert.AreEqual(0m, boleto.ImportePagado);
        }

        [Test]
        public void FranquiciaCompleta_FueraDeHorario_NoPermitePagar()
        {
            var franquicia = new TarjetaFranquiciaCompleta();
            var colectivo = new Colectivo("102");
            var fueraHorario = new DateTime(2024, 11, 20, 23, 0, 0);

            var boleto = colectivo.PagarCon(franquicia, fueraHorario);
            Assert.IsNull(boleto);
        }

        [Test]
        public void FranquiciaCompleta_Sabado_NoPermitePagar()
        {
            var franquicia = new TarjetaFranquiciaCompleta();
            var colectivo = new Colectivo("102");
            var sabado = new DateTime(2024, 11, 23, 10, 0, 0);

            var boleto = colectivo.PagarCon(franquicia, sabado);
            Assert.IsNull(boleto);
        }

        [Test]
        public void FranquiciaCompleta_Domingo_NoPermitePagar()
        {
            var franquicia = new TarjetaFranquiciaCompleta();
            var colectivo = new Colectivo("102");
            var domingo = new DateTime(2024, 11, 24, 10, 0, 0);

            var boleto = colectivo.PagarCon(franquicia, domingo);
            Assert.IsNull(boleto);
        }

        [Test]
        public void MedioBoleto_DentroDeHorario_PermitePagar()
        {
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var fechaValida = new DateTime(2024, 11, 20, 15, 0, 0);

            var boleto = colectivo.PagarCon(medio, fechaValida);
            Assert.IsNotNull(boleto);
        }

        [Test]
        public void MedioBoleto_FueraDeHorario_NoPermitePagar()
        {
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var fueraHorario = new DateTime(2024, 11, 20, 5, 0, 0);

            var boleto = colectivo.PagarCon(medio, fueraHorario);
            Assert.IsNull(boleto);
        }

        [Test]
        public void MedioBoleto_FinDeSemana_NoPermitePagar()
        {
            var medio = new TarjetaMedioBoleto();
            medio.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var sabado = new DateTime(2024, 11, 23, 10, 0, 0);

            var boleto = colectivo.PagarCon(medio, sabado);
            Assert.IsNull(boleto);
        }

        [Test]
        public void BoletoGratuito_DentroDeHorario_PermitePagar()
        {
            var gratuito = new TarjetaBoletoGratuito();
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var fechaValida = new DateTime(2024, 11, 20, 12, 0, 0);

            var boleto = colectivo.PagarCon(gratuito, fechaValida);
            Assert.IsNotNull(boleto);
        }

        [Test]
        public void BoletoGratuito_AntesHorario_NoPermitePagar()
        {
            var gratuito = new TarjetaBoletoGratuito();
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var antesHorario = new DateTime(2024, 11, 20, 5, 30, 0);

            var boleto = colectivo.PagarCon(gratuito, antesHorario);
            Assert.IsNull(boleto);
        }

        [Test]
        public void BoletoGratuito_DespuesHorario_NoPermitePagar()
        {
            var gratuito = new TarjetaBoletoGratuito();
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var despuesHorario = new DateTime(2024, 11, 20, 22, 30, 0);

            var boleto = colectivo.PagarCon(gratuito, despuesHorario);
            Assert.IsNull(boleto);
        }

        [Test]
        public void TodasLasFranquicias_Hora6AM_PermitePagar()
        {
            var medio = new TarjetaMedioBoleto();
            var gratuito = new TarjetaBoletoGratuito();
            var franquicia = new TarjetaFranquiciaCompleta();
            medio.CargarSaldo(5000);
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var hora6 = new DateTime(2024, 11, 20, 6, 0, 0);

            var b1 = colectivo.PagarCon(medio, hora6);
            var b2 = colectivo.PagarCon(gratuito, hora6);
            var b3 = colectivo.PagarCon(franquicia, hora6);

            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);
            Assert.IsNotNull(b3);
        }

        [Test]
        public void TodasLasFranquicias_Hora21_59_PermitePagar()
        {
            var medio = new TarjetaMedioBoleto();
            var gratuito = new TarjetaBoletoGratuito();
            var franquicia = new TarjetaFranquiciaCompleta();
            medio.CargarSaldo(5000);
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var hora21_59 = new DateTime(2024, 11, 20, 21, 59, 0);

            var b1 = colectivo.PagarCon(medio, hora21_59);
            var b2 = colectivo.PagarCon(gratuito, hora21_59);
            var b3 = colectivo.PagarCon(franquicia, hora21_59);

            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);
            Assert.IsNotNull(b3);
        }

        [Test]
        public void TodasLasFranquicias_Hora22_NoPermitePagar()
        {
            var medio = new TarjetaMedioBoleto();
            var gratuito = new TarjetaBoletoGratuito();
            var franquicia = new TarjetaFranquiciaCompleta();
            medio.CargarSaldo(5000);
            gratuito.CargarSaldo(5000);
            var colectivo = new Colectivo("102");
            var hora22 = new DateTime(2024, 11, 20, 22, 0, 0);

            var b1 = colectivo.PagarCon(medio, hora22);
            var b2 = colectivo.PagarCon(gratuito, hora22);
            var b3 = colectivo.PagarCon(franquicia, hora22);

            Assert.IsNull(b1);
            Assert.IsNull(b2);
            Assert.IsNull(b3);
        }
    } */
}