using System;
using System.Collections.Generic;

namespace TarjetaSube
{
    public class EstacionBicicleta
    {
        private const decimal TARIFA_DIA = 1777.50m;
        private const decimal MULTA_POR_EXCESO = 1000m;
        private const int TIEMPO_MAXIMO_MINUTOS = 60;

        private List<Retiro> retirosActivos;
        private List<Multa> multasPendientes;

        public EstacionBicicleta()
        {
            retirosActivos = new List<Retiro>();
            multasPendientes = new List<Multa>();
        }

        public bool RetirarBicicleta(Tarjeta tarjeta, DateTime? fechaHora = null)
        {
            DateTime ahora = fechaHora ?? DateTime.Now;
            long tarjetaId = tarjeta.ObtenerId();

            if (TieneBicicletaRetirada(tarjeta))
            {
                Console.WriteLine("Esta tarjeta ya tiene una bicicleta retirada.");
                return false;
            }

            LimpiarMultasAntiguasDelDia(ahora);
            int cantidadMultas = ContarMultasDeLaTarjeta(tarjetaId, ahora);
            
            decimal montoTotal = TARIFA_DIA + (MULTA_POR_EXCESO * cantidadMultas);

            if (cantidadMultas > 0)
                Console.WriteLine($"Se aplicaron {cantidadMultas} multa(s) por exceso de tiempo.");

            bool pagoExitoso = tarjeta.DescontarSaldo(montoTotal);
            
            if (!pagoExitoso)
            {
                Console.WriteLine("Saldo insuficiente. No se puede retirar la bicicleta.");
                return false;
            }

            EliminarMultasDeLaTarjeta(tarjetaId, ahora);
            retirosActivos.Add(new Retiro(tarjetaId, ahora));
            Console.WriteLine($"Bicicleta retirada exitosamente. Monto cobrado: ${montoTotal}");
            return true;
        }

        public bool DevolverBicicleta(Tarjeta tarjeta, DateTime? fechaHora = null)
        {
            DateTime ahora = fechaHora ?? DateTime.Now;
            long tarjetaId = tarjeta.ObtenerId();

            Retiro retiro = BuscarRetiroActivo(tarjetaId);
            
            if (retiro == null)
            {
                Console.WriteLine("Esta tarjeta no tiene ninguna bicicleta retirada.");
                return false;
            }

            double minutosUsados = (ahora - retiro.FechaRetiro).TotalMinutes;

            if (minutosUsados > TIEMPO_MAXIMO_MINUTOS)
            {
                multasPendientes.Add(new Multa(tarjetaId, ahora));
                Console.WriteLine($"Bicicleta devuelta con exceso de tiempo ({minutosUsados:F0} minutos).");
                Console.WriteLine($"Se aplicará una multa de ${MULTA_POR_EXCESO} en el próximo retiro.");
            }
            else
            {
                Console.WriteLine($"Bicicleta devuelta correctamente ({minutosUsados:F0} minutos de uso).");
            }

            retirosActivos.Remove(retiro);
            return true;
        }

        public bool TieneBicicletaRetirada(Tarjeta tarjeta)
        {
            return BuscarRetiroActivo(tarjeta.ObtenerId()) != null;
        }

        public int ObtenerCantidadMultasPendientes(Tarjeta tarjeta, DateTime? fechaHora = null)
        {
            DateTime ahora = fechaHora ?? DateTime.Now;
            LimpiarMultasAntiguasDelDia(ahora);
            return ContarMultasDeLaTarjeta(tarjeta.ObtenerId(), ahora);
        }

        private Retiro BuscarRetiroActivo(long tarjetaId)
        {
            foreach (Retiro retiro in retirosActivos)
            {
                if (retiro.TarjetaId == tarjetaId)
                    return retiro;
            }
            return null;
        }

        private int ContarMultasDeLaTarjeta(long tarjetaId, DateTime fecha)
        {
            int contador = 0;
            foreach (Multa multa in multasPendientes)
            {
                if (multa.TarjetaId == tarjetaId && multa.Fecha.Date == fecha.Date)
                    contador++;
            }
            return contador;
        }

        private void EliminarMultasDeLaTarjeta(long tarjetaId, DateTime fecha)
        {
            multasPendientes.RemoveAll(m => m.TarjetaId == tarjetaId && m.Fecha.Date == fecha.Date);
        }

        private void LimpiarMultasAntiguasDelDia(DateTime fechaActual)
        {
            multasPendientes.RemoveAll(m => m.Fecha.Date != fechaActual.Date);
        }

        private class Retiro
        {
            public long TarjetaId { get; private set; }
            public DateTime FechaRetiro { get; private set; }

            public Retiro(long id, DateTime fecha)
            {
                TarjetaId = id;
                FechaRetiro = fecha;
            }
        }

        private class Multa
        {
            public long TarjetaId { get; private set; }
            public DateTime Fecha { get; private set; }

            public Multa(long id, DateTime fecha)
            {
                TarjetaId = id;
                Fecha = fecha;
            }
        }
    }
}