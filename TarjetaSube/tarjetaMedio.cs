using System;

namespace TarjetaSube
{
    public class TarjetaMedioBoleto : Tarjeta
    {
        private DateTime ultimoViaje = DateTime.MinValue;
        private int viajesConDescuentoHoy = 0;
        private DateTime fechaUltimoDescuento = DateTime.Today;

        public override decimal ObtenerMontoAPagar(decimal tarifaBase)
        {
            DateTime ahora = DateTime.Now;

            if (ahora.Date != fechaUltimoDescuento.Date)
            {
                viajesConDescuentoHoy = 0;
                fechaUltimoDescuento = ahora.Date;
            }

            if (ultimoViaje != DateTime.MinValue && (ahora - ultimoViaje).TotalMinutes < 5)
            {
                ultimoViaje = ahora;
                return tarifaBase; 
            }

            if (viajesConDescuentoHoy < 2)
            {
                viajesConDescuentoHoy++;
                ultimoViaje = ahora;
                return tarifaBase / 2;
            }
            else
            {
                ultimoViaje = ahora;
                return tarifaBase; 
            }
        }

        public override bool PuedeUsarseAhora()
        {
            DateTime ahora = DateTime.Now;
            if (ahora.DayOfWeek == DayOfWeek.Saturday || ahora.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (ahora.Hour < 6 || ahora.Hour >= 22)
                return false;
            return true;
        }
    }
}