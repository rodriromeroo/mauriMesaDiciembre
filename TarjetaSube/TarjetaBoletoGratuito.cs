using System;

namespace TarjetaSube
{
    public class TarjetaBoletoGratuito : Tarjeta
    {
        private int viajesGratuitosHoy;
        private DateTime ultimaFechaViaje;

        public TarjetaBoletoGratuito() : base()
        {
            viajesGratuitosHoy = 0;
            ultimaFechaViaje = DateTime.MinValue;
        }

        public override decimal ObtenerMontoAPagar(decimal monto)
        {
            DateTime hoy = DateTime.Today;
            if (ultimaFechaViaje.Date != hoy)
                viajesGratuitosHoy = 0;

            if (viajesGratuitosHoy < 2)
                return 0m;
            else
                return monto;
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

        public override void RegistrarViaje()
        {
            DateTime hoy = DateTime.Today;
            if (ultimaFechaViaje.Date != hoy)
                viajesGratuitosHoy = 0;

            viajesGratuitosHoy++;
            ultimaFechaViaje = DateTime.Now;
        }
    }
}