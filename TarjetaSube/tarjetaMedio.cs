using System;

namespace TarjetaSube
{
    public class TarjetaMedioBoleto : Tarjeta
    {
        private DateTime ultimoViaje = DateTime.MinValue;
        private int viajesConDescuentoHoy = 0;
        private DateTime fechaUltimoDescuento = DateTime.Today;

        public override decimal ObtenerMontoAPagar(decimal tarifaBase, DateTime fechaHora)
        {
            if (fechaHora.Date != fechaUltimoDescuento.Date)
            {
                viajesConDescuentoHoy = 0;
                fechaUltimoDescuento = fechaHora.Date;
            }

            if (ultimoViaje != DateTime.MinValue && (fechaHora - ultimoViaje).TotalMinutes < 5)
                return -1m;

            if (viajesConDescuentoHoy < 2)
            {
                viajesConDescuentoHoy++;
                ultimoViaje = fechaHora;
                return tarifaBase / 2;
            }
            else
            {
                ultimoViaje = fechaHora;
                return tarifaBase;
            }
        }

        public override bool PuedeUsarseAhora(DateTime fechaHora)
        {
            if (fechaHora.DayOfWeek == DayOfWeek.Saturday || fechaHora.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (fechaHora.Hour < 6 || fechaHora.Hour >= 22)
                return false;
            return true;
        }

        public override decimal AplicarDescuentoUsoFrecuente(decimal monto, DateTime fechaHora) => monto;
    }
}