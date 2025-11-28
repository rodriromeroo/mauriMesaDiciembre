using System;
using System.Collections.Generic;

namespace TarjetaSube
{
    public class Tarjeta
    {
        protected decimal saldo;
        private decimal saldoPendiente;
        private const decimal LIMITE_NEGATIVO = -1200m;
        private const decimal LIMITE_MAXIMO = 56000m;
        private readonly List<decimal> montosPermitidos = new()
        {
            2000,
            3000,
            4000,
            5000,
            8000,
            10000,
            15000,
            20000,
            25000,
            30000
        };

        private readonly long id;
        private static long contadorId = 1000000;
        public long Id => id;

        protected int viajesDelMes;
        protected DateTime ultimoMesRegistrado;

        protected DateTime ultimoViajeFechaHora = DateTime.MinValue;
        protected string ultimaLineaViajada = "";

        public Tarjeta()
        {
            id = ++contadorId;
            saldo = 0;
            saldoPendiente = 0;
            viajesDelMes = 0;
            ultimoMesRegistrado = DateTime.Now;
        }

        public decimal ObtenerSaldo() => saldo;
        public decimal ObtenerSaldoPendiente() => saldoPendiente;
        public long ObtenerId() => id;

        public decimal CalcularDescuentoUsoFrecuente(decimal montoBase, DateTime fechaHora)
        {
            if (fechaHora.Month != ultimoMesRegistrado.Month || fechaHora.Year != ultimoMesRegistrado.Year)
            {
                viajesDelMes = 0;
                ultimoMesRegistrado = fechaHora;
            }

            viajesDelMes++;

            if (viajesDelMes <= 29) return montoBase;
            if (viajesDelMes <= 59) return montoBase * 0.80m;
            if (viajesDelMes <= 80) return montoBase * 0.75m;
            return montoBase;
        }

        public virtual decimal AplicarDescuentoUsoFrecuente(decimal monto, DateTime fechaHora) => CalcularDescuentoUsoFrecuente(monto, fechaHora);

        public virtual decimal ObtenerMontoAPagar(decimal tarifaBase, DateTime fechaHora) => tarifaBase;
        public virtual bool PuedeUsarseAhora(DateTime fechaHora) => true;
        public virtual void RegistrarViaje(DateTime fechaHora) { }

        public bool CargarSaldo(decimal monto)
        {
            if (!montosPermitidos.Contains(monto)) return false;
            decimal nuevoSaldo = saldo + monto;
            if (nuevoSaldo > LIMITE_MAXIMO)
            {
                decimal excedente = nuevoSaldo - LIMITE_MAXIMO;
                saldo = LIMITE_MAXIMO;
                saldoPendiente += excedente;
                return true;
            }
            saldo = nuevoSaldo;
            return true;
        }

        public void AcreditarCarga()
        {
            if (saldoPendiente <= 0) return;
            decimal espacio = LIMITE_MAXIMO - saldo;
            if (espacio <= 0) return;
            decimal aAcreditar = Math.Min(saldoPendiente, espacio);
            saldo += aAcreditar;
            saldoPendiente -= aAcreditar;
        }

        public virtual bool DescontarSaldo(decimal monto)
        {
            if (saldo - monto < LIMITE_NEGATIVO) return false;
            saldo -= monto;
            if (saldoPendiente > 0) AcreditarCarga();
            return true;
        }

        public void RegistrarUltimoViaje(string linea, DateTime cuando)
        {
            ultimaLineaViajada = linea;
            ultimoViajeFechaHora = cuando;
        }

        public virtual bool EsTrasbordoValido(string nuevaLinea, DateTime ahora)
        {
            if (ultimoViajeFechaHora == DateTime.MinValue) return false;
            if (string.Equals(ultimaLineaViajada.Trim(), nuevaLinea.Trim(), StringComparison.OrdinalIgnoreCase)) return false;
            if ((ahora - ultimoViajeFechaHora).TotalMinutes > 60) return false;
            if (ahora.DayOfWeek == DayOfWeek.Sunday) return false;
            if (ahora.Hour < 7 || ahora.Hour >= 22) return false;

            return true;
        }
    }
}