using System;

namespace TarjetaSube
{
    public class Colectivo
    {
        protected string numeroLinea;
        protected decimal valorPasaje;

        public Colectivo(string linea) : this(linea, false) { }

        protected Colectivo(string linea, bool interurbano)
        {
            numeroLinea = linea;
            valorPasaje = interurbano ? 3000m : 1580m;
        }

        public string ObtenerLinea() => numeroLinea;

        public virtual Boleto PagarCon(Tarjeta tarjeta, DateTime? fechaHora = null)
        {
            DateTime ahora = fechaHora ?? DateTime.Now;

            if (!tarjeta.PuedeUsarseAhora(ahora))
                return null;

            bool esTrasbordo = tarjeta.EsTrasbordoValido(numeroLinea, ahora);
            decimal montoBase = esTrasbordo ? 0m : tarjeta.ObtenerMontoAPagar(valorPasaje, ahora);

            if (montoBase == -1m) return null;

            if (!esTrasbordo)
                montoBase = tarjeta.AplicarDescuentoUsoFrecuente(montoBase, ahora);

            bool pagoExitoso = montoBase == 0 || tarjeta.DescontarSaldo(montoBase);
            if (!pagoExitoso) return null;

            tarjeta.RegistrarUltimoViaje(numeroLinea, ahora);
            tarjeta.RegistrarViaje(ahora);

            return new Boleto(numeroLinea, montoBase, tarjeta, esTrasbordo);
        }
    }
}