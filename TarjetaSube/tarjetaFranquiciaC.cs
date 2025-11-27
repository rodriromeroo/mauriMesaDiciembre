using System;

namespace TarjetaSube
{
    public class TarjetaFranquiciaCompleta : Tarjeta
    {
        public override decimal ObtenerMontoAPagar(decimal monto) => 0m;

        public override bool PuedeUsarseAhora()
        {
            DateTime ahora = DateTime.Now;
            if (ahora.DayOfWeek == DayOfWeek.Saturday || ahora.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (ahora.Hour < 6 || ahora.Hour >= 22)
                return false;
            return true;
        }

        public override bool DescontarSaldo(decimal monto)
        {
            if (ObtenerSaldoPendiente() > 0)
                AcreditarCarga();
            return true;
        }
    }
}