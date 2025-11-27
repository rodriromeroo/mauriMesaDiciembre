using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        private string numeroLinea;
        private decimal valorPasaje;
        private bool esInterurbano;

        public Colectivo(string linea)
        {
            numeroLinea = linea;
            valorPasaje = 1580m;
            esInterurbano = false;
        }

        public Colectivo(string linea, bool interurbano)
        {
            numeroLinea = linea;
            esInterurbano = interurbano;
            valorPasaje = interurbano ? 3000m : 1580m;
        }

        public string ObtenerLinea() => numeroLinea;
        public decimal ObtenerValorPasaje() => valorPasaje;

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            DateTime ahora = DateTime.Now;

            if (!tarjeta.PuedeUsarseAhora())
                return null;

            bool esTrasbordo = tarjeta.EsTrasbordoValido(numeroLinea, ahora);
            decimal montoBase = esTrasbordo ? 0m : tarjeta.ObtenerMontoAPagar(valorPasaje);

            if (!esTrasbordo && tarjeta.GetType() == typeof(Tarjeta))
            {
                montoBase = tarjeta.CalcularDescuentoUsoFrecuente(montoBase);
            }

            bool pagoExitoso = (montoBase == 0) || tarjeta.DescontarSaldo(montoBase);
            if (!pagoExitoso)
                return null;

            tarjeta.RegistrarUltimoViaje(numeroLinea, ahora);
            tarjeta.RegistrarViaje();

            return new Boleto(numeroLinea, montoBase, tarjeta, esTrasbordo);
        }
    }
}