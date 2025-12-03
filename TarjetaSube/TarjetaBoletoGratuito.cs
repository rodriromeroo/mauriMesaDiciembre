using System;

namespace TarjetaSube
{
    public class TarjetaBoletoGratuito : Tarjeta
    {
        private int viajesGratuitosHoy = 0;
        private DateTime ultimaFechaViaje = DateTime.MinValue;

        public override decimal ObtenerMontoAPagar(decimal monto, DateTime fechaHora)
        {
            if (ultimaFechaViaje.Date != fechaHora.Date)
                viajesGratuitosHoy = 0;

            if (viajesGratuitosHoy < 2)
                return 0m;
            else
                return monto;
        }

        public override bool PuedeUsarseAhora(DateTime fechaHora)
        {
            if (fechaHora.DayOfWeek == DayOfWeek.Saturday || fechaHora.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (fechaHora.Hour < 6 || fechaHora.Hour >= 22)
                return false;
            return true;
        }

        public override void RegistrarViaje(DateTime fechaHora)
        {
            if (ultimaFechaViaje.Date != fechaHora.Date)
                viajesGratuitosHoy = 0;
            viajesGratuitosHoy++;
            ultimaFechaViaje = fechaHora;
        }

        public override decimal AplicarDescuentoUsoFrecuente(decimal monto, DateTime fechaHora) => monto;
    }
}