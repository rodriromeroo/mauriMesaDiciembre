using System;

namespace TarjetaSube
{
    public class TarjetaFranquiciaCompleta : Tarjeta
    {
        public override decimal ObtenerMontoAPagar(decimal monto, DateTime fechaHora) => 0m;

        public override bool PuedeUsarseAhora(DateTime fechaHora)
        {
            if (fechaHora.DayOfWeek == DayOfWeek.Saturday || fechaHora.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (fechaHora.Hour < 6 || fechaHora.Hour >= 22)
                return false;
            return true;
        }

        public override bool EsTrasbordoValido(string nuevaLinea, DateTime ahora)
        {
            if (ultimoViajeFechaHora == DateTime.MinValue) return false;
            if (string.Equals(ultimaLineaViajada.Trim(), nuevaLinea.Trim(), StringComparison.OrdinalIgnoreCase)) return false;
            if ((ahora - ultimoViajeFechaHora).TotalMinutes > 60) return false;
            return PuedeUsarseAhora(ahora);
        }

        public override decimal AplicarDescuentoUsoFrecuente(decimal monto, DateTime fechaHora) => monto;
    }
}